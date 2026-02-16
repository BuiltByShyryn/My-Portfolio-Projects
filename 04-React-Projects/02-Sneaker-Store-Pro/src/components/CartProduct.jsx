import { useDispatch } from "react-redux";
import closeIcon from "../assets/close-icon.svg";
const CartProduct = ({ product }) => {
  const { id, title, price, image } = product;
  const dispatch = useDispatch();
  return (

    <div className="cart__product">

      <div className="product__img">
        <img src={image} alt="" />
      </div>
      <div className="product__desc">
        <h3>{title}</h3>
        <p>{price}</p>
      </div>
      <div className="product__delete">
        <button
          onClick={() => dispatch({ type: "DELETE_FROM_CART", payload: id })}
        >
          <img src={closeIcon} alt="" />
        </button>
      </div>


    </div>



  );
};

export default CartProduct;
