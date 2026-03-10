import React, { useState, useContext } from 'react';
import { View, TextInput, Button, StyleSheet } from 'react-native';
import { NotesContext } from '../context/NotesContext';

export default function AddNoteScreen({ navigation }) {
  const [text, setText] = useState('');
  const { addNote } = useContext(NotesContext);

  const handleSave = () => {
    if (text.trim() === '') return;
    addNote(text);
    navigation.goBack();
  };

  return (
    <View style={styles.container}>
      <TextInput
        style={styles.input}
        placeholder="Введите текст заметки"
        value={text}
        onChangeText={setText}
      />
      <Button title="Сохранить" onPress={handleSave} />
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, padding: 16 },
  input: { borderWidth: 1, borderColor: '#ccc', padding: 10, marginBottom: 10, borderRadius: 5 },
});
