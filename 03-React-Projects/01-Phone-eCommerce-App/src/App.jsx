import { useEffect, useState } from 'react';
import './App.css';
import Header from './components/Header';
import Content from './components/Content';
import Footer from './components/Footer';
import Banner from './components/Banner';
import Cart from './components/Cart';
import { useDispatch, useSelector } from 'react-redux';
import { setProducts } from './redux/productsSlice';
import { addToCart } from './Slicers/cartSlice'; 
import { Routes, Route, useLocation, useNavigate } from 'react-router-dom';

function App() {
  const navigate = useNavigate();
  const location = useLocation();

  const showBanner = location.pathname !== '/cart';
  const [favoritesCount, setFavoritesCount] = useState(0);

  const dispatch = useDispatch();
  const api = 'https://68f0e2470b966ad500348e10.mockapi.io/phones';

  const cartItems = useSelector(state => state.cart.items);

  const handleFavoriteToggle = (change) => {
    setFavoritesCount(prev => prev + change);
  };


  const handleCartToggle = (product) => {
    dispatch(addToCart(product));
  };

  const goToCart = () => {
    navigate('/cart');
  };

  useEffect(() => {
    fetch(api)
      .then(res => res.json())
      .then(data => dispatch(setProducts(data)));
  }, [dispatch]);

  return (
    <div className="app-wrapper">
      <Header
        favoritesCount={favoritesCount}
        cartCount={cartItems.length}
        onCartClick={goToCart}
      />

      {showBanner && <Banner />}

      <main className="app-content">
        <Routes>
          <Route
            path="/"
            element={<Content onFavoriteToggle={handleFavoriteToggle} onCartToggle={handleCartToggle} />}
          />
          <Route
            path="/cart"
            element={<Cart />}
          />
        </Routes>
      </main>

      <Footer />
    </div>
  );
}

export default App;
