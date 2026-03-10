import React, { useState } from 'react';
import { StyleSheet, Text, View, TextInput, TouchableOpacity, FlatList } from 'react-native';

export default function App() {
  const [contacts, setContacts] = useState([]);
  const [name, setName] = useState('');
  const [number, setNumber] = useState('');
  const [category, setCategory] = useState('');

  const addContact = () => {
    if (name && number && category) {
      const newContact = { name, number, category };
      setContacts([...contacts, newContact]);
      setName('');
      setNumber('');
      setCategory('');
    } else {
      alert("Заполните все поля!");
    }
  };

  const deleteContact = (number) => {
    setContacts(contacts.filter(contact => contact.number !== number));
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Мои Контакты</Text>

      <TextInput
        placeholder="Имя"
        value={name}
        onChangeText={setName}
        style={styles.input}
      />
      <TextInput
        placeholder="Номер"
        value={number}
        onChangeText={setNumber}
        style={styles.input}
        keyboardType="phone-pad"
      />
      <TextInput
        placeholder="Категория"
        value={category}
        onChangeText={setCategory}
        style={styles.input}
      />

      <TouchableOpacity onPress={addContact} style={styles.button}>
        <Text style={styles.buttonText}>Добавить контакт</Text>
      </TouchableOpacity>

      <FlatList
        data={contacts}
        keyExtractor={(item) => item.number}
        style={{ width: '90%', marginTop: 20 }}
        renderItem={({ item }) => (
          <View style={styles.contactItem}>
            <Text>{item.name} - {item.number} ({item.category})</Text>
            <TouchableOpacity onPress={() => deleteContact(item.number)}>
              <Text style={{ color: 'red' }}>Удалить</Text>
            </TouchableOpacity>
          </View>
        )}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    paddingTop: 50,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 20,
  },
  input: {
    borderWidth: 1,
    borderColor: '#ccc',
    padding: 10,
    marginVertical: 5,
    width: '80%',
    borderRadius: 5,
  },
  button: {
    backgroundColor: '#007AFF',
    padding: 10,
    borderRadius: 5,
    marginTop: 10,
    width: '50%',
    alignItems: 'center',
  },
  buttonText: {
    color: 'white',
    fontWeight: 'bold',
  },
  contactItem: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#eee',
  },
});
