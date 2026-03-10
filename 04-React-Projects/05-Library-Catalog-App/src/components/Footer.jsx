import './Footer.css'
import logo from '../assets/logo.svg'

const Footer = () => {
  const socials = ['VK', 'Telegram', 'Instagram']

  return (
    <footer>
      <div className="footer__container">
        <div className="footer__logo">
          <img src={logo} alt="логотип" />
          <p>© 2025 MyLibrary</p>
        </div>

        <div className="footer__socials">
          <p>Мы в соцсетях:</p>
          <ul>
            {socials.map((social, i) => (
              <li key={i}>
                <a href="#">{social}</a>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </footer>
  )
}

export default Footer
