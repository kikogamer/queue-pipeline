using Consumer.NotaFiscal.Configuration;

var builder = Host.CreateApplicationBuilder(args).ConfigureWorker();
var host = builder.Build();
host.Run();
