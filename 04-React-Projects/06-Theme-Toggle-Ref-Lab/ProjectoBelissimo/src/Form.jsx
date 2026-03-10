import { useState } from "react";

const Form = () => {
   const [value,setValue] = useState('');
   
    return (
        <>
            <div className="form">
                <input type="text" value={value} onChange = {(e) => setValue(e.target.value)}/>

            </div>
        </>
    );
}

export default Form;
