import PersonCard from "../components/PersonCard";
import '../components/PersonCard.css'
const Teachers = ({ teachers }) => {


    return (
        <>
            <section>
                <div className="paper">
                    <h2>Teachers</h2>
                    {teachers.map((teacher, index) => (
                        <PersonCard key={index} {...teacher} />
                    ))}
                </div>
            </section>
        </>
    )
}
export default Teachers