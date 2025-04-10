import { createContext, ReactNode, useContext, useReducer, useEffect } from "react";
import { CarrinhoItem } from "../models/carrinho-item";
import { Produto } from "../models/produto";

const addItem = (state: typeof initialState, item: CarrinhoItem) => {
  const newCarrinho = [...state.carrinho];
  const index = newCarrinho.findIndex((ci: CarrinhoItem) => ci.produto.id === item.produto.id);
  
  if (index !== -1) {
    newCarrinho[index] = { ...newCarrinho[index], quantidade: newCarrinho[index].quantidade + 1 };
    return newCarrinho;
  }

  return [...state.carrinho, item];
}

const getCarrinho = () => {
  const data = JSON.parse(localStorage.getItem("carrinho") || "[]");
  return data.map((item: CarrinhoItem) => new CarrinhoItem(Object.assign(new Produto(), item.produto), item.quantidade));
}

const updateQty = (state: typeof initialState, produto: Produto, quantidade: number) => {
  const newCarrinho = [...state.carrinho];
  const index = newCarrinho.findIndex((ci: CarrinhoItem) => ci.produto.id === produto.id);
  
  if (index !== -1) {
    newCarrinho[index] = { ...newCarrinho[index], quantidade: quantidade };
    return newCarrinho;
  }

  return state.carrinho;
}

const initialState = {
    carrinho: getCarrinho(),
    isVisible: false,
}

const carrinhoReducer = (state: typeof initialState, action: { type: string; payload?: any }) => {
  switch (action.type) {
    case "ADD_TO_CART":
      return { ...state, carrinho: addItem(state, action.payload) };
    case "CLEAR_CART":
      return { ...state, carrinho: [] };
    case "REMOVE_FROM_CART":
      return { ...state, carrinho: state.carrinho.filter((item: CarrinhoItem) => item !== action.payload) };
    case "TOGGLE_CART": 
      return { ...state, isVisible: !state.isVisible };
    case "UPDATE_CART":
      return { ...state, carrinho: updateQty(state, action.payload.produto, action.payload.quantidade) };
    default:
      return state;
  }
};

interface CarrinhoContextType {
  carrinho: CarrinhoItem[];
  isVisible: boolean;
  dispatch: React.Dispatch<{ type: any; payload: any }>;
}

const CarrinhoContext = createContext<CarrinhoContextType>({
    carrinho: [],
    isVisible: false,
    dispatch: () => undefined,
});

export const CarrinhoProvider = ({ children }: { children: ReactNode }) => {
  const [state, dispatch] = useReducer(carrinhoReducer, initialState);

  useEffect(() => {
    localStorage.setItem("carrinho", JSON.stringify(state.carrinho));
  }, [state.carrinho]);

  return (
    <CarrinhoContext.Provider value={{ carrinho: state.carrinho, isVisible: state.isVisible, dispatch }}>
      {children}
    </CarrinhoContext.Provider>
  );
};

export const useCarrinho = () => {
  return useContext(CarrinhoContext);
};
