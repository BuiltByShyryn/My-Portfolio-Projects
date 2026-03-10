

import { useState } from 'react';
import { StyleSheet, Text, Touchable, TouchableOpacity, View, ImageBackground } from 'react-native';
import { TextInput } from 'react-native-web';

export default function App() {
  const [text, setText] = useState("")
  const [result, setResult] = useState("")


  return (
    <View style={styles.container}>

      <ImageBackground
        source={require("./assets/cat.webp")}
        style={styles.background}
        resizeMode="cover"
      >





        <Text style={styles.text}>
          Would you like to share your last meal?

        </Text>
        <TextInput style={styles.input}
          value={text}
          onChangeText={(text) => setText(text)}
          placeholder="type in here"
        />
        <TouchableOpacity style={styles.btn}
          onPress={() => {
            setResult(text);
            setText("");
          }}
        >
          <Text style={styles.btnText}>Press Me!</Text>
        </TouchableOpacity>
        <Text style={styles.new_text}

        >
          Your thoughts were delivered here:

        </Text>
        <Text style={styles.new_text}>
          {result}
        </Text>
      </ImageBackground>
    </View>
  );
}

const styles = StyleSheet.create({
 background: {
  flex: 1,                
  width: "100%",
  height: "100%",
  justifyContent: "flex-start",  
  alignItems: "center",          
},

  body: {
    backgroundColor: "#F7F2F8"
  },
  btnText: {
    fontSize: 20,
    fontWeight: 700,
    color: "white",
  },
  container: {
    flex: 1,
    alignItems: 'center',
    backgroundColor: "#FFFFFF"

  },
  text: {
    marginTop: 100,
    padding: 30,
    width: 400,
    height: "auto",
    fontSize: 29,
    fontWeight: 600,
    backgroundColor: "#A78BFA",
    borderRadius: 30,
    color: "#3A3A3A"
  },
  input: {

    borderWidth: 2,
    borderColor: "black",

    padding: 10,
    borderRadius: 15,
    fontSize: 16,
    fontWeight: 400,
    width: 300,
    marginTop: 50,
  },
  btn: {
    alignItems: "center",
    justifyContent: "center",
    marginTop: 60,
    backgroundColor: "#A78BFA",
    color: "#Black",
    width: 100,
    height: 100,
    borderRadius: 30,
    alignItems: "center",
    justifyContent: "center",
    fontSize: 20,
    fontWeight: 700,
  },
  new_text: {
    marginTop: 60,
    fontSize: 20,
    fontWeight: 600,


  },
});
