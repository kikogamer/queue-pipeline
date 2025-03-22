import { Dialog, DialogBackdrop, DialogPanel, DialogTitle } from "@headlessui/react";
import { useCarrinho } from "../context/CarrinhoContext";
import { XMarkIcon } from "@heroicons/react/24/solid";

const Carrinho: React.FC = () => {

    const { carrinho, dispatch, isVisible } = useCarrinho();

    const totalCarrinho = carrinho.reduce((acc, item) => acc + item.valorTotal, 0);

    return (
        <Dialog open={isVisible} onClose={() => dispatch({ type: "TOGGLE_CART", payload: null })}>
            <DialogBackdrop
                transition
                className="fixed inset-0 bg-gray-500/75 transition-opacity duration-500 ease-in-out data-closed:opacity-0"
            />
            <div className="fixed inset-0 overflow-hidden">
                <div className="absolute inset-0 overflow-hidden">
                    <div className="pointer-events-none fixed inset-y-0 right-0 flex max-w-full pl-10">
                        <DialogPanel
                            transition
                            className="pointer-events-auto w-screen max-w-md transform transition duration-500 ease-in-out data-closed:translate-x-full sm:duration-700"
                        >
                            <div className="flex h-full flex-col overflow-y-scroll bg-white shadow-xl">
                                <div className="flex-1 overflow-y-auto px-4 py-6 sm:px-6">
                                    <div className="flex items-start justify-between">
                                        <DialogTitle className="text-lg font-medium text-gray-900">Meu Carrinho</DialogTitle>
                                        <div className="ml-3 flex h-7 items-center">
                                            <button
                                                type="button"
                                                onClick={() => dispatch({ type: "TOGGLE_CART", payload: null })}
                                                className="relative -m-2 p-2 text-gray-400 hover:text-gray-500"
                                            >
                                                <span className="absolute -inset-0.5" />
                                                <span className="sr-only">Fechar</span>
                                                <XMarkIcon aria-hidden="true" className="size-6" />
                                            </button>
                                        </div>
                                    </div>

                                    <div className="mt-8">
                                        <div className="flow-root">
                                            <ul role="list" className="-my-6 divide-y divide-gray-200">
                                                {carrinho.map((item) => (
                                                    <li key={item.produto.id} className="flex py-6">
                                                        
                                                        <div className="size-24 shrink-0 overflow-hidden rounded-md border border-gray-200">
                                                            <img alt={item.produto.nome} src={item.produto.imagemUrl} className="size-full object-cover" />
                                                        </div>
                                                        <div className="ml-4 flex flex-1 flex-col">
                                                            <div>
                                                                <div className="flex justify-between text-base font-medium text-gray-900">
                                                                    <h3>
                                                                        <a href={item.produto.imagemUrl}>{item.produto.nome}</a>
                                                                    </h3>
                                                                    <p className="ml-4">R$ {item.produto.preco.toFixed(2)}</p>
                                                                </div>
                                                            </div>
                                                            <div className="flex flex-1 items-end justify-between text-sm">
                                                                <p className="text-gray-500">Qty {item.quantidade}</p>
                                                                <div className="flex">
                                                                    <button onClick={() => dispatch({ type: "REMOVE_FROM_CART", payload: item })}
                                                                        type="button" 
                                                                        className="font-medium text-indigo-600 hover:text-indigo-500">
                                                                        Remover
                                                                    </button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
                                                ))}
                                            </ul>
                                        </div>
                                    </div>
                                </div>

                                <div className="border-t border-gray-200 px-4 py-6 sm:px-6">
                                    <div className="flex justify-between text-base font-medium text-gray-900">
                                        <p>Subtotal</p>
                                        <p>R$ {totalCarrinho.toFixed(2)}</p>
                                    </div>
                                    <p className="mt-0.5 text-sm text-gray-500">Frete e impostos calculados na finalização da compra.</p>
                                    <div className="mt-6">
                                        <a
                                            href="#"
                                            className="flex items-center justify-center rounded-md border border-transparent bg-indigo-600 px-6 py-3 text-base font-medium text-white shadow-xs hover:bg-indigo-700"
                                        >
                                            Checkout
                                        </a>
                                    </div>
                                    <div className="mt-6 flex justify-center text-center text-sm text-gray-500">
                                        <p>
                                            or{' '}
                                            <button
                                                type="button"
                                                onClick={() => dispatch({ type: "TOGGLE_CART", payload: null })}
                                                className="font-medium text-indigo-600 hover:text-indigo-500"
                                            >
                                                Continue Shopping
                                                <span aria-hidden="true"> &rarr;</span>
                                            </button>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </DialogPanel>
                    </div>
                </div>
            </div>
        </Dialog>
    );
};

export default Carrinho;