import { useState } from "react"

const Reverse = () => {
    const [text, setText] = useState("");
    const reversedText = text.split('').reverse().join('');

    console.log(text)
    return (
        <>
            <input type="text" value={text} onChange={(e) => setText(e.target.value)} />
            <p>{reversedText}</p>

        </>
    )
}
export default Reverse