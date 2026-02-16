import { useDispatch, useSelector } from "react-redux";
import { incrementQuantity, decrementQuantity, removeFromCart } from "../Slicers/cartSlice";
import emptyImg from '../assets/empty.svg';
import { useNavigate } from 'react-router-dom';
import trash from '../assets/trashBucket.svg'
import './Cart.css';

const Cart = () => {
    const dispatch = useDispatch();
    const cartItems = useSelector(state => state.cart.items);
    const navigate = useNavigate();
    if (cartItems.length === 0) {
        return (
            <div className="container">
                <div className="image">
                    <img src={emptyImg} alt="" />
                </div>
                <div className="cart__name">
                    <p>Корзина пуста</p>
                </div>
                <div className="cart__extra">
                    <p>Но это никогда не поздно исправить :)</p>
                </div>
                <div className="btn">
                    <button onClick={() => navigate('/')}>В каталог товаров</button>
                </div>
            </div>
        );
    }

    const totalPrice = cartItems.reduce((sum, item) => {
        const price = Number(String(item.price).replace(/\D/g, ""));
        return sum + price * item.quantity;
    }, 0);

    return (
        <div className="container__Cart">
            <h2>Корзина</h2>
            <div className="cart__list">
                <div className="cart__items">
                    {cartItems.map(item => (
                        <div key={item.id} className="cart__products">
                            <div className="cart__item__image">
                                <img src={item.image} alt={item.name} />
                                <div className="cart__item__buttons">
                                    <button onClick={() => dispatch(decrementQuantity(item.id))}>-</button>
                                    <span>{item.quantity}</span>
                                    <button onClick={() => dispatch(incrementQuantity(item.id))}>+</button>
                                </div>
                            </div>

                            <div className="cart__products__name">
                                <h1>{item.name}</h1>
                                <p>{item.price}</p>
                            </div>

                            <div className="cart__last">
                                <div className="delete__button">
                                    <img
                                        src={trash}
                                        alt="Удалить"
                                        onClick={() => dispatch(removeFromCart(Number(item.id)))}

                                    />
                                </div>
                            </div>
                        </div>
                    ))}
                </div>

                <div className="order">
                    <div className="orderprice">
                        <p>ИТОГО</p>
                        <p>{totalPrice}₽</p>
                    </div>
                    <button onClick={() => navigate('/')} className='btn__order'>Перейти к оформлению</button>
                </div>
            </div>
        </div>
    );
};

export default Cart;
