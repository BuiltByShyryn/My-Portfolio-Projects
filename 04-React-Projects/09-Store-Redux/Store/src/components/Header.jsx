import './Header.css'
import fascoImg from '../assets/FASCO.svg'
import { useState } from 'react'

const Header = () => {
  const [city, setCity] = useState('')
  const [selectedCity, setSelectedCity] = useState('')

  return (
    <header>
      <div className="navbar">
        <div className="logo">
          <img src={fascoImg} alt="Fasco logo" />
          {selectedCity && <p className="city-name">{selectedCity}</p>}
        </div>

        <div className="btn">
          <input
            type="text"
            placeholder="City"
            value={city}
            onChange={(e) => setCity(e.target.value)}
          />
          <button onClick={() => setSelectedCity(city)}>Press Me!</button>
        </div>
      </div>
    </header>
  )
}

export default Header
