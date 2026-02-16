import { useDispatch, useSelector } from "react-redux";
import "./Cart.css";
import CartProduct from "./CartProduct";
import boxImg from "../assets/empty__box.svg";
import arrow__back__image from "../assets/arrow_Back.svg";
import arrow_orderImg from "../assets/arrow_order.svg";
import orderImg from "../assets/order_paper.svg";
import { useState } from "react";

const Cart = ({ open, setOpen }) => {
  const cartItems = useSelector((state) => state.cart);
  const [orderPlaced, setOrderPlaced] = useState(false);

  const dispatch = useDispatch();

  const handleOrder = () => {
    dispatch({ type: "CLEAR_CART" });
    setOrderPlaced(true);
  };

  const total = cartItems.reduce((sum, product) => sum + product.price, 0);
  const tax = (total * 0.05).toFixed(2);

  return (
    <div className={`overlay ${open ? "active" : ""}`}>
      <div className="cart">
        <h1>Корзина</h1>
        <div className="cart__close" onClick={() => setOpen(false)}></div>

        {orderPlaced ? (
          <div className="empty__box">
            <img src={orderImg} alt="order placed" />
            <span className="topic-1">Заказ оформлен!</span>
            <span className="desc">
              Ваш заказ #18 скоро будет передан курьерской доставке
            </span>
            <button className="back" onClick={() => {
  setOpen(false);
  setOrderPlaced(false);
}}>
              <img src={arrow__back__image} alt="arrow back" /> Вернуться назад
            </button>
          </div>
        ) : cartItems.length === 0 ? (
          <div className="cart__elements">
            <div className="empty__box">
              <img src={boxImg} alt="empty box" />
              <span className="topic">Корзина пустая</span>
              <span className="desc">
                Добавьте хотя бы одну пару кроссовок, чтобы сделать заказ.
              </span>
              <button className="back" onClick={() => setOpen(false)}>
                <img src={arrow__back__image} alt="arrow back" /> Вернуться назад
              </button>
            </div>
          </div>
        ) : (
          <>
            <div className="cart__elements">
              {cartItems.map((product, index) => (
                <CartProduct product={product} key={index} />
              ))}
            </div>

            <div className="amount_box">
              <div className="amount">
                <span className="label">Итого:</span>
                <span className="dots"></span>
                <span className="total">{total} тг</span>
              </div>
              <div className="tax">
                <span className="label">Налог 5%:</span>
                <span className="dots"></span>
                <span className="total">{tax} тг</span>
              </div>
              <div className="btn__move">
                <button className="agree" onClick={handleOrder}>
                  Оформить заказ
                  <img src={arrow_orderImg} alt="arrow order" />
                </button>
              </div>
            </div>
          </>
        )}
      </div>
    </div>
  );
};

export default Cart;
