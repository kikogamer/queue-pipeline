import { Route, Routes } from 'react-router-dom';
import './App.css';
import { BrowserRouter as Router } from 'react-router-dom';
import ProductsPage from './pages/ProductsPage';
import ProductDetailPage from './pages/ProductDetailPage';
import Header from './components/Header';
import { CarrinhoProvider } from './context/CarrinhoContext';
import Carrinho from './components/Carrinho';

function App() {
    return (
        <CarrinhoProvider>
            <Header />
            <Router>
                <Routes>
                    <Route path="/produtos" Component={ProductsPage} />
                    <Route path="/produtos/detail/:productId" Component={ProductDetailPage} />
                    <Route path="/" Component={ProductsPage} />
                </Routes>
            </Router>
            <Carrinho />
        </CarrinhoProvider>
    );
}

export default App;