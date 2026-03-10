import './Main.css'
import bookImg from '../assets/book.svg'


const Main = () => {

    const books = [
        {
            title: 'Мастер и Маргарита',
            description: 'Классика русской литературы о любви и мистике.',
            image: 'https://placehold.co/150x200',
        },
        {
            title: 'Гарри Поттер',
            description: 'История о мальчике, который выжил.',
            image: 'https://placehold.co/150x200',
        },
        {
            title: 'Преступление и наказание',
            description: 'Драма и философия Достоевского.',
            image: 'https://placehold.co/150x200',
        },
    ]


    return (
        <main>

            <div className="main__container">
                <h2>Библиотека</h2>
                <p>Количество книг: {books.length}</p>
                <div className="books__list">
                    {books.map((book, index) => (
                        <div key={index} className="book__card">

                            <img src={bookImg} alt="" />
                            <h3>{book.title}</h3>
                            <p>{book.description}</p>
                            <button>Читать</button>

                        </div>

                    ))}

                </div>
            </div>
        </main>




    )
}
export default Main