import { useNavigate } from "react-router-dom";
import './Main.css'
const Main = () => {
    const navigate = useNavigate();

    return (
        <main>
            <div className="box">
                <div className="box__container">
                    <div className="list">
                        <h4 onClick={() => navigate("/students")}>Students</h4>
                        <h4 onClick={() => navigate("/teachers")}>Teachers</h4>
                    </div>
                </div>
            </div>
        </main>
    );
};

export default Main;
