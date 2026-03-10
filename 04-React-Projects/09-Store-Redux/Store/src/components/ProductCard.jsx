import './ProductsCard.css'


const ProductCard = ({ product }) => {
    const { title, price, image } = product;


    return (
        <div className="product-card">
            <div className="image">
                {image && <img src={image} alt={image}/>}
            </div>
            <div className="desc">
                <h3>{title}</h3>
                <p>- {price}$</p>
            </div>
        </div>
    );
};

export default ProductCard;
