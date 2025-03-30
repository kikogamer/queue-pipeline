import React from 'react';
import { Produto } from '../models/produto';
import { useNavigate } from 'react-router-dom';
import { useCarrinho } from '../context/CarrinhoContext';
import { CarrinhoItem } from '../models/carrinho-item';

const ProductDetail: React.FC<{ produto: Produto }> = ({ produto }) => {

    const navigate = useNavigate(); 
    const { dispatch } = useCarrinho();
    
    const handleSubmit = async (e: { preventDefault: () => void; }) => {
        e.preventDefault();   
        dispatch({ type: "ADD_TO_CART", payload: new CarrinhoItem(produto) });
        navigate("/");
    };

    return (
        <div className="grid w-full grid-cols-1 items-start gap-x-6 gap-y-8 sm:grid-cols-12 lg:gap-x-8">
            <img
                alt={produto.nome}
                src={produto.imagemUrl}
                className="aspect-2/3 w-full rounded-lg bg-gray-100 object-cover sm:col-span-4 lg:col-span-5"
            />
            <div className="sm:col-span-8 lg:col-span-7">
                <h2 className="text-2xl font-bold text-gray-900 sm:pr-12">{produto.nome}</h2>

                <section aria-labelledby="information-heading" className="mt-2">
                    <h3 id="information-heading" className="sr-only">
                        Product information
                    </h3>
                    <p className="text-2x1 text-gray-900">{produto.descricao}</p>
                    <p className="text-2x1 text-gray-900">R$ {produto.preco}</p>
                </section>

                <form onSubmit={handleSubmit}>
                    <input type="hidden" name="produtoId" value={produto.id} />
                    <button
                        type="submit"
                        className="mt-6 flex w-full items-center justify-center rounded-md border border-transparent bg-indigo-600 px-8 py-3 text-base font-medium text-white hover:bg-indigo-700 focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 focus:outline-hidden"
                    >
                        Adicionar ao Carrinho
                    </button>
                </form>
            </div>
        </div>
    );
}

export default ProductDetail;