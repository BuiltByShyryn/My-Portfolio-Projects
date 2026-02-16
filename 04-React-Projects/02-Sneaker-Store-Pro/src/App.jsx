import { Routes, Route } from "react-router-dom";
import { useEffect, useState } from "react";
import Header from "./components/Header";
import Cart from "./components/Cart";
import Favorites from "./components/Favorites";
import Home from "./components/Home";
import "./App.css";
import { useDispatch, useSelector } from "react-redux";

function App() {
  const [open, setOpen] = useState(false);

  const products = useSelector((state) => state.products);

  const dispatch = useDispatch();
  const API = "https://68efb725b06cc802829e8235.mockapi.io/products";

  useEffect(() => {
    fetch(API)
      .then((res) => res.json())
      .then((data) => dispatch({ type: "RENDER_PRODUCTS", payload: data }));
  }, []);

  return (
    <>
      <div className="app">
        <Header setOpen={setOpen} />
        <Cart open={open} setOpen={setOpen} />
        <Routes>
          <Route path="/favorites" element={<Favorites />} />
          <Route path="/" element={<Home productsArr={products} />} />
        </Routes>
      </div>
    </>
  );
}

export default App;
