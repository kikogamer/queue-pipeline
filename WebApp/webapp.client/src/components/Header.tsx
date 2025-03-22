import { ShoppingBagIcon } from '@heroicons/react/16/solid';
import React from 'react';
import { useCarrinho } from '../context/CarrinhoContext';


const Header: React.FC = () => {
    const { carrinho, dispatch } = useCarrinho();
    
    return (
        <div className="lg:flex lg:items-center lg:justify-between">
            <div className="min-w-0 flex-1">
                <h2 className="text-2xl/7 font-bold text-gray-900 sm:truncate sm:text-3xl sm:tracking-tight">
                    <a href='/'>Loja Virtual Camisetas de Programação</a>
                </h2>
            </div>
            <div className="mt-5 flex lg:mt-0 lg:ml-4">
                <div className="ml-4 flow-root lg:ml-6">
                  <button className="group -m-2 flex items-center p-2" onClick={() => dispatch({ type: "TOGGLE_CART", payload: null })}>
                    <ShoppingBagIcon
                      aria-hidden="true"
                      className="size-8 shrink-0 text-gray-400 group-hover:text-gray-500"
                    />
                    <span className="ml-1 mt-2 text-sm font-medium text-gray-700 group-hover:text-gray-800">{ carrinho.length }</span>
                    <span className="sr-only">items in cart, view bag</span>
                  </button>
                </div>
            </div>
        </div>
    );
};

export default Header;