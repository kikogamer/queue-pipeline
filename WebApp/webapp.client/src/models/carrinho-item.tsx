import { Produto } from "./produto";

export class CarrinhoItem {
    produto!: Produto;
    quantidade!: number;
    valorTotal!: number;

    constructor(produto: Produto, quantidade: number = 1) {
        this.produto = produto;
        this.quantidade = quantidade;
        this.valorTotal = produto.preco * quantidade;                
    }
}