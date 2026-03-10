import './Main.css'
import ProductCard from "./ProductCard";


const Main = ({ productsArr }) => {
    const productsCardList = productsArr.map((product) => {
        return <ProductCard product={product} key={product.id} />;
    });


    return (
        <>
            <main><div className="products">
                <div className="container">
                    {productsCardList}
                       
                    
                </div>
            </div>
            </main>
            

        </>
    )
}
export default Main