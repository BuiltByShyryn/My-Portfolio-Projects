import { createContext, useEffect, useState } from "react";

export const AppContext = createContext();

export const AppProvider = ({ children }) => {
  const [profile, setProfile] = useState({
    name: "Jhon",
    email: "Jgon30@gmail.com",
    phone: "+7 777 777 77 77",
    about: "Frontend Developer",
  });

  const [posts, setPosts] = useState([
  {
    "id": "1f2c9c52-4a7c-4a21-9c13-018c3f0f1d11",
    "title": "Новые возможности",
    "text": "Новый день — новые возможности. Главное начать!"
  },
  {
    "id": "b3a8fe21-cc91-4c1e-9d55-a382a435e52f",
    "title": "Работа над собой",
    "text": "Работа над собой — самый ценный вклад, который мы можем сделать."
  },
  {
    "id": "e79bf4d2-9027-4410-beb5-8258c1f90fc3",
    "title": "Маленькие шаги",
    "text": "Маленькие шаги каждый день приводят к большим результатам."
  },
  {
    "id": "a012ab76-77f9-4f85-a007-68d593e90759",
    "title": "Движение вперёд",
    "text": "Иногда лучший способ двигаться вперёд — просто не останавливаться."
  },
  {
    "id": "c0d1b44a-71ed-44fb-bb19-d1e2ea62bd3e",
    "title": "Будьте добры к себе",
    "text": "Будьте добры к себе — вы делаете всё возможное."
  },
  {
    "id": "e1c93b85-3f3a-447b-b951-f19e5f9dac2b",
    "title": "Сила креативности",
    "text": "Креативность — это смелость выражать своё видение."
  },
  {
    "id": "73cfdd59-6d0e-41de-8b58-d9f8bbf33c9d",
    "title": "Вдохновение рядом",
    "text": "Иногда одно вдохновение может изменить весь день."
  },
  {
    "id": "8d6de750-3952-4a91-9b38-867cc6e0b66e",
    "title": "Начать снова",
    "text": "Успех приходит к тем, кто не боится начинать снова."
  },
  {
    "id": "f9a47263-85a4-4c12-ae61-90f7b93b1ee9",
    "title": "Сила окружения",
    "text": "Окружайте себя теми, кто верит в ваши амбиции."
  },
  {
    "id": "6f52c939-d47e-431f-98bf-660db9a11571",
    "title": "Каждый шаг важен",
    "text": "Помните: каждый шаг вперёд приближает вас к цели."
  }
]);

 // const API = "https://68f0e2470b966ad500348e10.mockapi.io/sneakers";

 // const fetchPosts = async () => {
  //  try {
 //     const res = await fetch(API);
//const data = await res.json();
 //     setPosts(data);
 //   } catch (err) {
//console.log("Error fetching posts:", err);
 //   }
 // };

const addPost = (newPost) => {
    setPosts((prev) => [{
       id: Date.now().toString(), 
       ...newPost }, 
       ...prev]);
  };

  const UpdateProfile = (newData) => {
    setProfile((prev) => ({ ...prev, ...newData }));
  };

  //useEffect(() => {
  //  fetchPosts();
 // }, []);

  return (
    <AppContext.Provider value={{ profile, UpdateProfile, posts, addPost }}>
      {children}
    </AppContext.Provider>
  );
};
