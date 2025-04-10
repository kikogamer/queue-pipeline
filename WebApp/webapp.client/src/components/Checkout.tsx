import React, { useState } from "react";
import { useCarrinho } from "../context/CarrinhoContext";
import { TrashIcon } from "@heroicons/react/24/solid";
import { CarrinhoItem } from "../models/carrinho-item";
import { useNavigate } from "react-router-dom";

const Checkout: React.FC = () => {
    const { carrinho, dispatch } = useCarrinho();

    const [pedido, setPedido] = useState({
        cliente: "",
        email: "",
        endereco: "",
        complemento: "",
        cidade: "",
        estado: "",
        pais: "",
        cep: "",
        telefone: "",
        frete: 50,
        imposto: 35.50,
        itens: [] as { produtoId: number; descricao: string; quantidade: number }[]
    });

    const handlePedidoChange = (event: { target: { name: any; value: any; }; }) => {
        const { name, value } = event.target;
        setPedido({ ...pedido, [name]: value });
    };
    const [errors, setErrors] = useState<{ api?: string; [key: string]: string | undefined }>({});
    const [loading, setLoading] = useState(false);

    const [totalCarrinho, setTotalCarrinho] = React.useState(carrinho.reduce((acc, item) => acc + item.valorTotal, 0));

    const totalPedido = totalCarrinho + pedido.frete + pedido.imposto;

    const deleteItem = (item: CarrinhoItem) => {
        dispatch({ type: "REMOVE_FROM_CART", payload: item })
        setTotalCarrinho(totalCarrinho - item.valorTotal);
    } 
    
    const updateQty = (item: CarrinhoItem, e: React.ChangeEvent<HTMLSelectElement>) => {
        dispatch({ type: "UPDATE_CART", payload: { produto: item.produto, quantidade: Number(e.target.value) } });
        
        const quantidade =  Number(e.target.value);
        const valorTotal = quantidade * item.produto.preco;

        const index = carrinho.findIndex((ci: CarrinhoItem) => ci.produto.id === item.produto.id);
        if (index !== -1) {
            const dif = valorTotal - carrinho[index].valorTotal;
            setTotalCarrinho(totalCarrinho + dif);
        }            
    }

    const navigate = useNavigate();

    const handleSubmit = async (event: { preventDefault: () => void; }) => {
        event.preventDefault();

        setLoading(true);
        setErrors({});

        try {
            pedido.itens = [];
            pedido.itens.push(...carrinho.map(i => ({ produtoId: i.produto.id, descricao: i.produto.nome, quantidade: i.quantidade })));
            
            const response = await fetch("api/v1/pedidos", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(pedido),
              });

            const data = await response.json(); // Captura a resposta JSON da API

            if (!response.ok) {
                const apiErrors: Record<string, string> = {};
                for (const field in data.errors) {
                    if (Array.isArray(data.errors[field])) {
                        apiErrors[field.toLowerCase()] = data.errors[field].join(" "); // Junta múltiplas mensagens em uma única string
                    }
                }
                setErrors(apiErrors);
                return;
            }

            dispatch({
                type: "CLEAR_CART",
                payload: undefined
            });
            alert("Pedido realizado com sucesso!");
            navigate("/");

        } catch (error) {
            setErrors({ api: "Erro inesperado. Tente novamente mais tarde." });
        } finally {
            setLoading(false);
        }
    }

    return (
        <form>
            {errors.api && <p className="text-red-500 text-sm mt-2">{errors.api}</p>}
            <div className="bg-gray-50 border-b border-gray-900/10 pb-12">
                <div className="flex flex-row">
                    <div className="text-2xl text-gray-900">
                        <div className="border-b border-gray-900/10 pb-12 p-5">
                            <h2 className="text-base/7 font-semibold text-gray-900">Dados para Contato</h2>
                            <p className="mt-1 text-sm/6 text-gray-600">Use um endereço permanente onde você possa receber correspondência.</p>
                            <div className="mt-2 grid grid-cols-1 gap-x-10 gap-y-8 sm:grid-cols-10">
                                <div className="sm:col-span-10">
                                    <label htmlFor="email" className="block text-sm/6 font-medium text-gray-900">
                                        Email
                                    </label>
                                    {errors.email && <p className="text-red-500 text-sm">{errors.email}</p>}
                                    <div className="mt-2">
                                        <input
                                        id="email"
                                        name="email"
                                        type="email"
                                        value={pedido.email || ''}
                                        onChange={handlePedidoChange}
                                        autoComplete="email"
                                        className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600"
                                        />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="border-b border-gray-900/10 pb-12 p-5">
                            <h2 className="text-base/7 font-semibold text-gray-900">Dados para Entrega</h2>
                            <div className="mt-2 grid grid-cols-1 gap-x-10 gap-y-8 sm:grid-cols-10">
                                <div className="sm:col-span-10">
                                    <label htmlFor="cliente" className="block text-sm/6 font-medium text-gray-900">
                                        Nome
                                    </label>
                                    {errors.cliente && <p className="text-red-500 text-sm">{errors.cliente}</p>}
                                    <div className="mt-2">
                                        <input
                                        id="cliente"
                                        name="cliente"
                                        type="text"
                                        value={pedido.cliente || ''}
                                        onChange={handlePedidoChange}
                                        autoComplete="cliente"
                                        className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600"
                                        />
                                    </div>
                                </div>
                                <div className="sm:col-span-10">
                                    <label htmlFor="endereco" className="block text-sm/6 font-medium text-gray-900">
                                        Endereço
                                    </label>
                                    {errors.endereco && <p className="text-red-500 text-sm">{errors.endereco}</p>}
                                    <div className="mt-2">
                                        <input
                                        id="endereco"
                                        name="endereco"
                                        type="text"
                                        value={pedido.endereco || ''}
                                        onChange={handlePedidoChange}
                                        autoComplete="endereco"
                                        className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600"
                                        />
                                    </div>
                                </div>
                                <div className="sm:col-span-10">
                                    <label htmlFor="complemento" className="block text-sm/6 font-medium text-gray-900">
                                        Complemento
                                    </label>
                                    {errors.complemento && <p className="text-red-500 text-sm">{errors.complemento}</p>}
                                    <div className="mt-2">
                                        <input
                                        id="complemento"
                                        name="complemento"
                                        type="text"
                                        value={pedido.complemento || ''}
                                        onChange={handlePedidoChange}
                                        autoComplete="complemento"
                                        className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600"
                                        />
                                    </div>
                                </div>
                                <div className="sm:col-span-10 flex flex-row">
                                    <div className="basis-2/4">
                                        <label htmlFor="cidade" className="block text-sm/6 font-medium text-gray-900">
                                            Cidade
                                        </label>
                                        {errors.cidade && <p className="text-red-500 text-sm">{errors.cidade}</p>}
                                        <div className="mt-2">
                                            <input
                                            id="cidade"
                                            name="cidade"
                                            type="text"
                                            value={pedido.cidade || ''}
                                            onChange={handlePedidoChange}
                                            autoComplete="cidade"
                                            className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600"
                                            />
                                        </div>
                                    </div>
                                    <div className="ml-4 basis-2/4">
                                        <label htmlFor="pais" className="block text-sm/6 font-medium text-gray-900">
                                            País
                                        </label>
                                        {errors.pais && <p className="text-red-500 text-sm">{errors.pais}</p>}
                                        <div className="mt-2">
                                            <input
                                            id="pais"
                                            name="pais"
                                            type="text"
                                            value={pedido.pais || ''}
                                            onChange={handlePedidoChange}
                                            autoComplete="pais"
                                            className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600"
                                            />
                                        </div>
                                    </div>
                                </div>
                                <div className="sm:col-span-6 flex flex-row">
                                    <div className="basis-2/4">
                                        <label htmlFor="estado" className="block text-sm/6 font-medium text-gray-900">
                                            Estado
                                        </label>
                                        {errors.estado && <p className="text-red-500 text-sm">{errors.estado}</p>}
                                        <div>
                                            <input 
                                            id="estado"
                                            name="estado"
                                            type="text"
                                            value={pedido.estado || ''}
                                            onChange={handlePedidoChange}
                                            autoComplete="estado"
                                            className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600"
                                            />
                                        </div>
                                    </div>
                                    <div className="ml-4 basis-2/4">
                                        <label htmlFor="cep" className="block text-sm/6 font-medium text-gray-900">
                                            Cep
                                        </label>
                                        {errors.cep && <p className="text-red-500 text-sm">{errors.cep}</p>}
                                        <div>
                                            <input
                                            id="cep"
                                            name="cep"
                                            type="text"
                                            value={pedido.cep || ''}
                                            onChange={handlePedidoChange}
                                            autoComplete="cep"
                                            className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600"
                                            />
                                        </div>
                                    </div>
                                </div>
                                <div className="sm:col-span-10">
                                    <label htmlFor="telefone" className="block text-sm/6 font-medium text-gray-900">
                                        Telefone
                                    </label>
                                    {errors.telefone && <p className="text-red-500 text-sm">{errors.telefone}</p>}
                                    <div className="mt-2">
                                        <input
                                        id="telefone"
                                        name="telefone"
                                        type="text"
                                        value={pedido.telefone || ''}
                                        onChange={handlePedidoChange}
                                        autoComplete="telefone"
                                        className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600"
                                        />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="text-2xl text-gray-900">
                        <div className="border-b border-gray-900/10 pb-12 p-5">
                            <h2 className="text-base/7 font-semibold text-gray-900">Resumo do Pedido</h2>
                            <div className="flex h-full flex-col overflow-y-scroll bg-white shadow-xl mt-6 w-96">
                                <div className="grid grid-cols-1">
                                    <ul role="list" className="divide-y divide-gray-200">
                                        {carrinho.map((item) => (
                                            <li key={item.produto.id} className="flex py-6">
                                                <div className="ml-6 size-24 shrink-0 overflow-hidden rounded-md border border-gray-200">
                                                    <img alt={item.produto.nome} src={item.produto.imagemUrl} className="size-full object-cover" />
                                                </div>
                                                <div className="ml-4 flex flex-1 flex-col">
                                                    <div>
                                                        <div className="flex justify-between text-base font-medium text-gray-900">
                                                            <h3>
                                                                <a href={`/produtos/detail/${item.produto.id}`}>{item.produto.nome}</a>
                                                            </h3>
                                                            <button onClick={() => deleteItem(item) }
                                                                type="button" 
                                                                className="font-medium text-indigo-600 hover:text-indigo-500">
                                                                <TrashIcon aria-hidden="true" className="size-6 text-gray-500" />
                                                            </button>
                                                        </div>
                                                    </div>
                                                    <div className="flex flex-1 items-end justify-between text-sm">
                                                        <p className="font-medium text-gray-900 font-bold">R$ {item.produto.preco.toFixed(2)}</p>
                                                        <div className="flex">
                                                            <select 
                                                                className="w-16 h-8 border border-gray-300 rounded-md text-center text-sm text-gray-900"
                                                                defaultValue={item.quantidade}
                                                                onChange={(e) => updateQty(item, e) } 
                                                            >
                                                                {[...Array(10).keys()].map((i) => (<option key={i} value={i + 1}>{i + 1}</option>))}
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </li>
                                        ))}
                                    </ul>
                                </div>
                                <div className="border-t border-gray-200 px-4 py-6 sm:px-6">
                                    <div className="flex flex-row justify-between text-base font-medium text-gray-500">
                                        <p>Subtotal</p>
                                        <p>R$ {totalCarrinho.toFixed(2)}</p>
                                    </div>
                                    <div className="flex flex-row justify-between text-base font-medium text-gray-500">
                                        <p>Frete</p>
                                        <p>R$ {pedido.frete.toFixed(2)}</p>
                                    </div>
                                    <div className="flex flex-row justify-between text-base font-medium text-gray-500">
                                        <p>Imposto</p>
                                        <p>R$ {pedido.imposto.toFixed(2)}</p>
                                    </div>
                                </div>  
                                <div className="border-t border-gray-200 px-4 py-6 sm:px-6">
                                    <div className="flex flex-row justify-between font-medium text-gray-900">
                                        <p>Total</p>
                                        <p>R$ {totalPedido.toFixed(2)}</p>
                                    </div>
                                </div>
                                <div className="ml-6 flex justify-center text-center text-sm text-gray-500 p-6">
                                    <button 
                                        onClick={handleSubmit}
                                        disabled={loading}
                                        className="w-full flex items-center justify-center rounded-md border border-transparent bg-indigo-600 px-6 py-3 text-base font-medium text-white shadow-xs hover:bg-indigo-700">
                                        {loading ? "Processando..." : "Finalizar Compra"}
                                    </button>                      
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    )
}

export default Checkout;