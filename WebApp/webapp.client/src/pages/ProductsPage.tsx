import React, { useEffect, useState } from 'react';
import ProductList from '../components/ProductList';
import { Produto } from '../models/produto';

const ProductsPage: React.FC = () => {
  const [products, setProducts] = useState<Produto[]>([]);

  useEffect(() => {
    async function fetchProducts() {
      const response = await fetch('api/v1/produtos');
      if (response.ok) {
        const data = await response.json();
        setProducts(data.$values.map((p: Produto) => Object.assign(new Produto(), p)));
      }
    }

    fetchProducts();
  }, []);

  return (
    <div className="bg-white">
        <div className="mx-auto max-w-2xl px-4 py-16 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
        <h2 className="sr-only">Produtos</h2>
            <ProductList produtos={products} />
        </div>
    </div>
  );
};

export default ProductsPage;