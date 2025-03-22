import React from 'react';
import { useParams } from 'react-router-dom';
import ProductDetail from '../components/ProductDetail';
import { Produto } from '../models/produto';

const ProductDetailPage: React.FC = () => {
    const { productId } = useParams<{ productId: string }>();
    const [product, setProduct] = React.useState<Produto | null>(null);

    React.useEffect(() => {
        const fetchProduct = async () => {
            const response = await fetch(`/api/v1/produtos/${productId}`);
            const data = await response.json();
            setProduct(Object.assign(new Produto(), data));
        };

        fetchProduct();
    }, [productId]);

    if (!product) {
        return <div>Loading...</div>;
    }

    return (
        <div className="bg-white">
            <div className="mx-auto max-w-2xl px-4 py-16 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
                <h2 className="sr-only">Detalhe Produto</h2>
                <ProductDetail produto={product} />
            </div>
        </div>
    );
};

export default ProductDetailPage;