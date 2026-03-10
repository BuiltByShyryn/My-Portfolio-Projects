import { useState } from 'react'
import Form from "./Form"
import './App.css'
import Switch from './Switch'


function App() {
  const[value,setValue] = useState("")
const bodyInput = useRef();

console.log(bodyInput.current.value)

  return (
    <>
    <input type="text" ref={bodyInput} value ={value} onChange={(E)=> setValue(e.target.value)} />
<Form/>
<Switch/>

    </>
  )
}

export default App
