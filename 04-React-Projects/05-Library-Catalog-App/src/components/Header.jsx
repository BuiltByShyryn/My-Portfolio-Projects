import './Header.css'
import logo from '../assets/logo.svg'  

const Header = () => {
  

  const links = ['Главная', 'Каталог', 'Контакты']
  const phone = '+7 (999) 123-45-67'
  const email = 'library@example.com'

  return (
    <header>
      <div className="container">
        <div className="logo">
          <img src={logo} alt="логотип" />
        </div>

        <div className="header__buttons">
          <div className="header__links">
            {links.map((link, index) => (
              <a key={index} href="#">
                {link}
              </a>
            ))}
          </div>

          <div className="header__emails">{email}</div>

          <div className="header__phone">{phone}</div>
        </div>
      </div>
    </header>
  )
}

export default Header
