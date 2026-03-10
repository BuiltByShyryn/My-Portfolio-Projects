import { View, Text, StyleSheet, TouchableOpacity, FlatList, TextInput } from "react-native";
import { useContext, useState } from "react";
import { AppContext } from "../context/AppContext";

export default function HomeScreen({ navigation }) {
  const { profile, posts, addPost } = useContext(AppContext);

  const [newTitle, setNewTitle] = useState("");
  const [newText, setNewText] = useState("");

  const handleAddPost = () => {
    if (!newTitle || !newText) return;
    addPost({
      title: newTitle,
      text: newText
    });

    setNewTitle("");
    setNewText("");
  }

  return (
    <View style={styles.container}>
      <Text style={styles.text}>Home, {profile.name}</Text>
      <TextInput
        placeholder="Post Title"
        value={newTitle}
        onChangeText={setNewTitle}
        style={styles.input}
      />
      <TextInput
        placeholder="Post Text"
        value={newText}
        onChangeText={setNewText}
        style={styles.input}
      />
      <TouchableOpacity style={styles.addBtn} onPress={handleAddPost}>
        <Text style={styles.addBtnText}>Add Post</Text>
      </TouchableOpacity>
      <FlatList
        style={styles.box}
        data={posts}
        keyExtractor={(item) => item.id.toString()}
        renderItem={({ item }) => (
          <TouchableOpacity style={styles.post__item}
            onPress={() => navigation.navigate('PostDetails', { postId: item.id })}>
            <Text style={styles.post__item__title}>{item.title}</Text>
          </TouchableOpacity>
        )}
      />

    </View>
  );
}



const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  input: {
    width: "100%",
    height: 100,
    backgroundColor: "#9b8cff",
    padding: 10,
    marginVertical: 10,
    borderRadius: 12,
    borderColor: "#black",
    borderWidth: 1,
    fontSize:18,
    fontWeight:400,
    

  },
  addBtn: {
    width: "100%",
    height:50,
    borderRadius:10,
    borderColor:"#black",
    borderWidth:1,
    backgroundColor:"#4a90e2",


  },
  addBtnText:{
fontSize:20,
fontWeight:600,
padding:10,

  },
  text: {
    fontSize: 22,
    marginBottom: 20,
    marginTop: 20,
    marginLeft: 20,

  },
  box: {
    padding: 20
  },
  post__item: {
    width: '100%',
    height: 60,
    backgroundColor: '#576ea6ff',
    marginBottom: 20,
    borderRadius: 12,
    justifyContent: "center",
    paddingHorizontal: 20,
  },
  post__item__title: {
    color: "#fff",
    fontSize: 16,
  }


})