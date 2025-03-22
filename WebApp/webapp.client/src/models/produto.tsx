export class Produto {
    id: number;
    nome: string;
    descricao: string;
    preco: number;
    imagem: string;

    constructor() {
        this.id = 0;
        this.nome = "";
        this.descricao = "";
        this.preco = 0;
        this.imagem = "";    
    }

    get imagemUrl(): string {
        return `/produtos/${this.imagem}`;
    }
}