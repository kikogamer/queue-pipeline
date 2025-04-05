
using App.Adapters.Serialization;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace App.Adapters.Consumer
{
    public class AsyncQueueServiceWorker<TRequest, TResponse> : QueueServiceWorkerBase where TResponse : Task
    {
        private readonly IAmqpSerializer _serializer;
        protected readonly Func<TRequest?, TResponse> _dispatchFunc;

        public AsyncQueueServiceWorker(ILogger? logger, IConnection connection, IChannel channel, IAmqpSerializer serializer, Func<TRequest?, TResponse> dispatchFunc, 
            string queueName, ushort prefetchCount) 
            : base(logger, connection, channel, queueName, prefetchCount)
        {
            _serializer = serializer;
            _dispatchFunc = dispatchFunc;
        }

        protected override IAsyncBasicConsumer BuildConsumer()
        {
            AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(this._channel);

            consumer.ReceivedAsync += this.Receive;

            return consumer;
        }

        protected virtual async Task<PostConsumeAction> Dispatch(BasicDeliverEventArgs receivedItem, TRequest? request)
        {
            if (receivedItem is null) throw new ArgumentNullException(nameof(receivedItem));

            await this._dispatchFunc(request);

            return PostConsumeAction.Ack;
        }

        private async Task Receive(object sender, BasicDeliverEventArgs receivedItem)
        {
            PostConsumeAction postReceiveAction = this.TryDeserialize(receivedItem, out TRequest? request);

            if (postReceiveAction == PostConsumeAction.None)
            {
                try
                {
                    postReceiveAction = await this.Dispatch(receivedItem, request);
                }
                catch (Exception e)
                {
                    postReceiveAction = PostConsumeAction.Nack;
                    this?._logger?.LogWarning("Exception on processing message {queueName} {exception}", this.QueueName, e);
                }
            }

            switch (postReceiveAction)
            {
                case PostConsumeAction.None: throw new InvalidOperationException("None is unsupported");
                case PostConsumeAction.Ack: await this._channel.BasicAckAsync(receivedItem.DeliveryTag, false); break;
                case PostConsumeAction.Nack: await this._channel.BasicNackAsync(receivedItem.DeliveryTag, false, false); break;
                case PostConsumeAction.Reject: await this._channel.BasicRejectAsync(receivedItem.DeliveryTag, false); break;
            }
        }

        private PostConsumeAction TryDeserialize(BasicDeliverEventArgs receivedItem, out TRequest? request)
        {
            if (receivedItem is null) throw new ArgumentNullException(nameof(receivedItem));

            PostConsumeAction postReceiveAction = PostConsumeAction.None;

            request = default;
            try
            {
                request = _serializer.Deserialize<TRequest>(receivedItem);
            }
            catch (Exception exception)
            {
                postReceiveAction = PostConsumeAction.Reject;

                this?._logger?.LogWarning("Message rejected during desserialization {exception}", exception);
            }

            return postReceiveAction;
        }
    }
}
