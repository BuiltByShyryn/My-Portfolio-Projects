import PersonCard from "../components/PersonCard";
import '../components/PersonCard.css'
const Students = ({ students }) => {


    return (
        <>
            <section>
                <div className="paper">
                    <h2>Students</h2>
                    {students.map((student, index) => (
                        <PersonCard key={index} {...student} />
                    ))}
                </div>
            </section>
        </>
    )
}
export default Students