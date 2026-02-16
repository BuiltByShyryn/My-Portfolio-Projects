import Banner from "./Banner";
import Products from "./Products";

const Home = ({ productsArr }) => {
  return (
    <>
      <Banner />
      <Products productsArr={productsArr} />
    </>
  );
};
export default Home;
