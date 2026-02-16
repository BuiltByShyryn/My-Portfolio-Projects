import { useSelector } from 'react-redux';
import './Content.css'
import Products from './Products';
import firstImg from '../assets/phone_1.svg'
import secondImg from '../assets/phone_2.svg'
import thirdImg from '../assets/phone_3.svg'
const Content = ({ onFavoriteToggle, onCartToggle }) => {
    const productArr = useSelector((state) => state.products.items);


    const headphones = productArr.filter(p => p.category === 'headphones');
    const airpads = productArr.filter(p => p.category === 'airpads');



    return (
        <>
            <section><div className="everything">
                <div className="two__suggestions">
                    <div className="suggestion">
                        <p className="advice">Чехлы</p>
                        <p className="advice">Беспроводные наушники</p>
                    </div>
                </div>
                <div className="content__container">
                    <div className="first">
                        <div className="first__phone"><img src={firstImg} alt="" /><p>Стеклянные</p></div>
                    </div>
                    <div className="first">
                        <div className="second__phone"><img src={secondImg} alt="" /><p>Силиконовые</p></div>
                    </div>
                    <div className="first">
                        <div className="third__phone"><img src={thirdImg} alt="" /><p>Кожаные</p></div>
                    </div>


                </div>
                <div className="content__container__second">
                    <h2>Наушники</h2>
                    <div className="headphones-section">

                        <Products
                            products={headphones}
                            onFavoriteToggle={onFavoriteToggle}
                            onCartToggle={onCartToggle} />
                    </div>

                </div>
                <div className="content__container__third">
                    <h2>Беспроводные наушники</h2>
                    <div className="headphones-section">

                        <Products
                            products={airpads}
                            onFavoriteToggle={onFavoriteToggle}
                            onCartToggle={onCartToggle} />
                    </div>



                </div>
            </div>
            </section>

        </>
    )
}
export default Content;