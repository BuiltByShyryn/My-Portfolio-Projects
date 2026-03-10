import './Footer.css'
import logo from '../assets/FASCO.svg'
const Footer = () => {
    const sponsors = ["Nike", "Apple", "Samsung", "Zara"];
    return (
        <>
            <footer>
                <div className="footer_container">
                    <div className="footer_logo">
                        <img src={logo} alt="Logo" />
                    </div>

                    <div className="sponsors">

                        {sponsors.map((item) => <p key={item}>{item}</p>)}
                    </div>
                </div>
                <div className="footer__container">
Copyright © 2022 Xpro . All Rights Reseved.
                </div>
            </footer>
        </>
    )
}
export default Footer