import './Products.css';
import second_heart from '../assets/second_heart.svg';
import second_heart_filled from '../assets/second_heart_filled.svg';
import starImg from '../assets/star.svg'
import { useState } from 'react';

const Products = ({ products, onFavoriteToggle, onCartToggle }) => {

    const [active, setActive] = useState({});
const handleHeartClick = (product) => {
    const isActive = !!active[product.id]; 
    const newActive = { ...active, [product.id]: !isActive };
    setActive(newActive);

    onFavoriteToggle(!isActive ? 1 : -1); 
    onCartToggle(product);
};

    return (
        <>
            {products.map(product => (
                <div key={product.id} className="product__card">

                    <div className="heart">
                        <img 
                            src={active[product.id] ? second_heart_filled : second_heart}
                            alt="favorite"
                            onClick={() => handleHeartClick(product)}
                        />
                    </div>

                    <div className="product__card__image">
                        <img src={product.image} alt={product.name} />
                    </div>

                    <div className="product__card__name">
                        <div className="product__card__name__name">
                            <p className='name'>{product.name}</p>
                            <div className="star">
                                <img src={starImg} alt="" />{product.star}
                            </div>
                        </div>

                        <div className="product__card__price">
                            <p className='price'>{product.price}</p>
                            {product.old_price && (
                                <p className='old__price'>{product.old_price}</p>
                            )}
                        </div>
                    </div>

                </div>
            ))}
        </>
    );
};

export default Products;
