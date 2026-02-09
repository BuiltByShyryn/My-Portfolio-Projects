import { NavLink } from 'react-router-dom'
import logo from '../assets/logo.svg'
import cart from '../assets/cart.svg'
import favorites from '../assets/favorites.svg'
import account from '../assets/account.svg'
import './Header.css'


const Header = ({ setOpen }) => {


    return (
        <header>
            <div className="container">
                <div className="header__logo">
                    <img src={logo} alt="" />
                </div>
                <div className="header__btns">
                    <div className="header__cart" onClick={() => setOpen(true)}>
                        <img src={cart} alt="" />
                        <p>1200 руб.</p>
                    </div>
                    <NavLink to="/favorites">
                        <div className="header__favorites">
                            <img src={favorites} alt="" />
                        </div>
                    </NavLink>
                    <div className="header__acount">
                        <img src={account} alt="" />
                    </div>
                </div>
            </div>
        </header>
    )
}

export default Header;