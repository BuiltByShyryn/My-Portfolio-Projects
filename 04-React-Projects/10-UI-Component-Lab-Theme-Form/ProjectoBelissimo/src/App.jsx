import { useState } from 'react'
import Form from "./Form"
import './App.css'
import Switch from './Switch'


function App() {
  const [count, setCount] = useState(0)

  return (
    <>
<Form/>
<Switch/>

    </>
  )
}

export default App
