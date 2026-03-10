import { Routes, Route } from "react-router-dom";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";

import "./App.css";

import Header from "./components/Header";
import Main from "./components/Main";
import Discount from "./components/Discount";
import Footer from "./components/Footer";

function App() {
  const products = useSelector((state) => state.products);
  const dispatch = useDispatch();
  const API = "https://68f0e2470b966ad500348e10.mockapi.io/sneakers";

  useEffect(() => {
    fetch(API)
      .then((res) => res.json())
      .then((data) => dispatch({ type: "RENDER_PRODUCTS", payload: data }))
      
  }, [dispatch]);

  return (
    <>
      <Header/>
      <Routes>
        <Route path="/" element={<Main productsArr={products} />} />
      </Routes>
      <Discount/>
      <Footer/>
    </>
  );
}

export default App;
