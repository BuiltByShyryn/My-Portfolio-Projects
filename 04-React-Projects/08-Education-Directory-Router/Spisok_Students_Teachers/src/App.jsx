import Students from './pages/Students';
import Teachers from './pages/Teachers';
import { Routes, Route } from 'react-router-dom';
import Footer from './components/Footer';
import './App.css'
import Main from './components/Main'
import Header from './components/Header';


function App() {


  const students = [
    {
      fullName: "Ivan Petrov",
      age: 20,
      course: "React Basics",
      phone: "+7 900 123 45 67",
      email: "ivan.petrov@example.com"
    },
    {
      fullName: "Anna Smirnova",
      age: 22,
      course: "JavaScript Advanced",
      phone: "+7 911 234 56 78",
      email: "anna.smirnova@example.com"
    },
    {
      fullName: "Pavel Kozlov",
      age: 19,
      course: "Frontend Fundamentals",
      phone: "+7 912 345 67 89",
      email: "pavel.kozlov@example.com"
    }
  ];

  const teachers = [
    {
      fullName: "Elena Pavlova",
      age: 35,
      subject: "React",
      phone: "+7 900 222 33 44",
      email: "elena.pavlova@example.com"
    },
    {
      fullName: "Sergey Ivanov",
      age: 40,
      subject: "JavaScript",
      phone: "+7 911 333 44 55",
      email: "sergey.ivanov@example.com"
    },
    {
      fullName: "Natalia Petrova",
      age: 29,
      subject: "HTML & CSS",
      phone: "+7 912 444 55 66",
      email: "natalia.petrova@example.com"
    }
  ];




  return (

    <>
    <Header/>
    <Routes>
      <Route path="/" element={<Main />} />
      <Route path="/students" element={<Students students = {students}  />} />
      <Route path="/teachers" element={<Teachers teachers = {teachers} />} />
      
    </Routes>
    
    <Footer/>
    </>
  )
}

export default App
