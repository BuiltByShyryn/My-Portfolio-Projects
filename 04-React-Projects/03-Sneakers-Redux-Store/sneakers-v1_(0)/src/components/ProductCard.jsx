import { useState } from "react";
import iconPlus from "../assets/plusIcon.svg";
import checkedIcon from "../assets/checked.svg";
import { useDispatch, useSelector } from "react-redux";
const ProductCard = ({ product }) => {
  const { id, title, image, price } = product;
  const [checked, setChecked] = useState(false);
  const dispatch = useDispatch();
  const cartItems = useSelector((state) => state.cart);

  const addToCartHandler = () => {
    setChecked(true);
    dispatch({
      type: "ADD_TO_CART",
      payload: {
        id,
        title,
        image,
        price,
      },
    });
  };
  return (
    <div className="product__card">
      
      <div className="card__img">
        <img src={image} alt="" />
      </div>
      <div className="card__name">{title}</div>
      <div className="card__bottom">
        <div className="card__price">
          <span>Цена:</span>
          <p>{price}</p>
        </div>
        <div
          className={`card__btn ${
            checked && cartItems.length > 0 ? "checked" : ""
          }`}
        >
          <button onClick={addToCartHandler}>
            <img
              src={checked && cartItems.length > 0 ? checkedIcon : iconPlus}
              alt=""
            />
          </button>
          
        </div>
      </div>
    </div>
  );
};
export default ProductCard;
