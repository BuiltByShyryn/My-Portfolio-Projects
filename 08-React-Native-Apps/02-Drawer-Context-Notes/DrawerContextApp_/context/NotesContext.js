import React, { createContext, useState } from 'react';
import { v4 as uuidv4 } from 'uuid';

export const NotesContext = createContext();

export const NotesProvider = ({ children }) => {
  const [notes, setNotes] = useState([]);

  const addNote = (text) => {
    setNotes((prevNotes) => [...prevNotes, { id: uuidv4(), text }]);
  };

  const deleteNote = (id) => {
    setNotes((prevNotes) => prevNotes.filter(note => note.id !== id));
  };

  return (
    <NotesContext.Provider value={{ notes, addNote, deleteNote }}>
      {children}
    </NotesContext.Provider>
  );
};
