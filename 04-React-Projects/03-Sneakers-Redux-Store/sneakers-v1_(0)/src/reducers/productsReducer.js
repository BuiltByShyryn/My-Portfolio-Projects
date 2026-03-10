const initialState = {
  products: [],
  cart: [],
};

const productsReducer = (state = initialState, action) => {
  switch (action.type) {
    case "RENDER_PRODUCTS":
      return { ...state, products: [...action.payload] };
    case "ADD_TO_CART":
      return { ...state, cart: [...state.cart, action.payload] };
    case "DELETE_FROM_CART":
      return {
        ...state,
        cart: state.cart.filter((product) => product.id != action.payload),
      };
      case "CLEAR_CART":
        return {
          ...state,cart:[]
        };
    default:
      return state;
  }
};

export default productsReducer;
