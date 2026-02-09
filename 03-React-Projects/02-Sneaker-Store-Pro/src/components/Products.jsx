import ProductCard from "./ProductCard";
import "./Products.css";

const Products = ({ productsArr }) => {
  const productsCardList = productsArr.map((product) => {
    return <ProductCard product={product} key={product.id} />;
  });
  return (
    <div className="products">
      <div className="container">
        <div className="products__head">
          <div className="products__title">
            <h2>Все кроссовки</h2>
          </div>
          <div className="products__search">
            <input type="search" placeholder="Поиск" />
          </div>
        </div>
        <div className="products__grid">{productsCardList}</div>
      </div>
    </div>
  );
};

export default Products;
