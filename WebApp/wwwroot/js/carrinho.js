function setCarrinho(carrinho) {
    localStorage.setItem('carrinho', JSON.stringify(carrinho));
}

function getCarrinho() {
    const carrinhoSalvo = localStorage.getItem('carrinho');

    if (carrinhoSalvo) {
        return JSON.parse(carrinhoSalvo);
    }

    return [];
}