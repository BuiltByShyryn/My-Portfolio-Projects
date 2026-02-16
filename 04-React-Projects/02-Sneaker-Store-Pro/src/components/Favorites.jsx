import { NavLink } from 'react-router-dom'
import arrowIcon from '../assets/arrow.svg'
import './Favorites.css'
const Favorites = () => {
    return (
        <div className="favorites">
            <div className="container">
                <div className="favorites__title">
                    <NavLink to="/">
                        <div className="back__btn">
                            <img src={arrowIcon} alt="" />
                        </div>
                    </NavLink>
                    <h2>Мои закладки</h2>
                </div>
            </div>
        </div>
    )
}

export default Favorites;