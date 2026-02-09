import logoImg from '../assets/logo.svg'
import favoriteImg from '../assets/favorite.svg'
import cartImg from '../assets/cart.svg'
import phoneImg from '../assets/miniphone.svg'
import arrowImg from '../assets/arrowIcon.svg'
import { Link } from 'react-router-dom'
import { useNavigate } from 'react-router-dom';
import './Header.css'
import { useState } from 'react'

const Header = ({ favoritesCount, cartCount }) => {
    const [openPhones, setOpenPhones] = useState(false)
    const navigate = useNavigate();
    return (
        <header>
            <div className="header__container">
                <div className="header__logo">
                    <div className="header__logo__logo">
                        <img src={logoImg} alt="" className='logo' onClick={() => navigate('/')} />
                        <div className="header__logo__selecter">
                            <img src={phoneImg} alt="" className='phoneImg' />
                            <p>Выбрать модель телефона</p>
                            <img 
                                src={arrowImg} 
                                alt="" 
                                className={`arrow ${openPhones ? 'active' : ''}`}
                                onClick={() => setOpenPhones(!openPhones)}
                            />
                        </div>
                    </div>
                </div>

                <div className="header__btns">
                    <div className="header__btns__favourites">
                        <img src={favoriteImg} alt="" />
                        <span className="favorite-count">{favoritesCount}</span>
                    </div>
                    <div className="header__btns__cart">
                        <Link to="/cart">
                            <img src={cartImg} alt="" />
                             <span className="cart__badge">{cartCount}</span>
                        </Link>
                    </div>
                </div>
            </div>
        </header>
    )
}

export default Header
