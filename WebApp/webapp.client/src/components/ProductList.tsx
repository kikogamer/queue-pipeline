import React from 'react';
import { Produto } from '../models/produto';

const ProductList: React.FC<{ produtos: Produto[] }> = ({ produtos }) => {
    return (
        <div className="grid grid-cols-1 gap-x-6 gap-y-10 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 xl:gap-x-8">
            {produtos.map((produto) => {
                return (
                    <div key={produto.id} className="group relative">
                        <img
                            alt={`camiseta ${produto.nome}`}
                            src={produto.imagemUrl}
                            className="aspect-square w-full rounded-md bg-gray-200 object-cover group-hover:opacity-75 lg:aspect-auto lg:h-80"
                        />
                        <div className="mt-4 flex justify-between">
                            <div>
                                <h3 className="text-sm text-gray-700">
                                    <a href={`/produtos/detail/${produto.id}`}>
                                    <span aria-hidden="true" className="absolute inset-0" />
                                    {produto.nome}
                                    </a>
                                </h3>
                            </div>
                            <p className="text-sm font-medium text-gray-900">R$ {produto.preco}</p>
                        </div>
                    </div>
            )})}
        </div>
    );
};

export default ProductList;