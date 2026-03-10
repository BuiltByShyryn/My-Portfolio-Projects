
import { useState } from 'react';
import { StyleSheet, Text, View, TextInput, FlatList, TouchableOpacity } from 'react-native';



export default function App() {
  const [product, setProduct] = useState("")
  const [productsList, setProductsList] = useState([]);

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Shopping List Screen </Text>
      <TextInput
        style={styles.input}
        placeholder='Add'
        value={product}
        onChangeText={text => setProduct(text)} />
      <TouchableOpacity style={styles.button}
        onPress={() => {
          if (product.trim() !== "") {
            setProductsList([...productsList, { id: Date.now().toString(), name: product }]);
            setProduct("")
          }
        }}

      >
        <Text style={styles.buttonText}>Add</Text>
      </TouchableOpacity>

      <FlatList
        data={productsList}
        keyExtractor={item => item.id}
        renderItem={({ item }) => (
          <TouchableOpacity
            onPress={() => {
              setProductsList(productsList.filter(p => p.id !== item.id))
            }}
            style={{
              marginVertical: 5,
              backgroundColor: "#5b00a5",
              padding: 10,
              borderRadius: 5
            }}
              >
              <Text style={styles.buttonText}>{item.name}</Text>
          </TouchableOpacity>

  )
}
      />

    </View >
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 20,
  },
  input: {
    borderWidth: 1,
    borderColor: "#000",
    padding: 10,
    borderRadius: 5,
    width: '100%',
    marginBottom: 10,
  },

  button: {
    backgroundColor: "#5b00a5",
    padding: 15,
    borderRadius: 5,
    width: '100%',
  }
  ,
  title: {
    fontSize: 24,
    fontWeight: "bold",
    marginBottom: 20,
    textAlign: "center",
  },


  buttonText: {
    textAlign: "center",
    fontSize: 16,
    color: "#fff",
  }


});
