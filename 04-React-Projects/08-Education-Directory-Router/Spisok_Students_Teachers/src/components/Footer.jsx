import './PersonCard'
import { useRef, useState } from "react";
import { useNavigate,useLocation } from "react-router-dom";

import musicFile from "../assets/music.mp3";



const Footer = () => {
    const audioRef = useRef(null);
    const [isPlaying, setIsPlaying] = useState(false);
    const navigate = useNavigate();
    const location = useLocation();
    const togglePlay = () => {
        const audio = audioRef.current;
        if (!audio) return;

        if (isPlaying) {
            audio.pause();
        } else {
            audio.play();
        }
        setIsPlaying(!isPlaying);
    }
    return (
        <>
            <footer>
                {location.pathname === "/" ? (
                    <button disabled>🏠 You’re Home</button>
                ) : (
                    <button onClick={() => navigate("/")}>🏡 Back to Main</button>
                )}
                <h6 className='cautious'>CAUTIOUS!
                    Can be Loud!
                    ✦Basement Actions✦-⋆</h6>

                <div className="party__space">

                    <audio ref={audioRef} src={musicFile} loop />
                    <h3 onClick={togglePlay}>
                        {isPlaying ? "⏸ Pause Music" : "🎵 Play Music"}
                    </h3>
                </div>

            </footer>
        </>
    )
}
export default Footer