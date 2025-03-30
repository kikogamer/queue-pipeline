import React from "react";
import Checkout from "../components/Checkout";

const CheckoutPage: React.FC = () => {
    return (
        <div className="bg-white">
            <div className="mx-auto max-w-2xl px-4 py-16 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
                <h2 className="sr-only">Checkout</h2>
                <Checkout />
            </div>
        </div>
    )
}

export default CheckoutPage;