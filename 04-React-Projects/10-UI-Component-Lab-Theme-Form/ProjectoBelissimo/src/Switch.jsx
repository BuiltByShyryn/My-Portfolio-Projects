import { useState } from "react";
import "./App.css";

const Switch = () => {
  const [isDark, setIsDark] = useState(false);

  const toggleTheme = () => {
    setIsDark(!isDark);
  };

  return (
    <div className={isDark ? "dark" : "light"}>
      <p>{isDark ? "Dark Mode (；′⌒`)" : "Light Mode( •̀ ω •́ )✧"}</p>
      <button onClick={toggleTheme}>
        Toggle
      </button>
    </div>
  );
};

export default Switch;
