import { Ionicons } from "@expo/vector-icons";
import { useEffect, useState } from "react";
import {
  View,
  Text,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  FlatList,
} from "react-native";

export default function ActiveScreen({
  todos,
  completedTaskHandler,
  addTodoHandler,
}) {
  const [input, setInput] = useState("");

  const inputHandler = () => {
    if (!input.trim()) return;
    addTodoHandler(input);
    setInput("");
  };

  //   useEffect(() => {
  //     if (navigation && navigation.setParams) {
  //       navigation.setParams({ completed });
  //     }
  //   }, [completed, navigation]);

  return (
    <View style={styles.container}>
      <Text style={styles.text}>Active Screen</Text>
      <View style={styles.wrapper}>
        <TextInput
          placeholder="Введите текст"
          value={input}
          onChangeText={(text) => setInput(text)}
          style={styles.input}
        />
        <TouchableOpacity style={styles.input__btn} onPress={inputHandler}>
          <Text>
            <Ionicons
              name="add-outline"
              color="#fff"
              style={styles.input__btn__text}
            />
          </Text>
        </TouchableOpacity>
      </View>
      <FlatList
        data={todos}
        keyExtractor={(item) => item.id}
        renderItem={({ item }) => (
          <View style={styles.todo__item}>
            <Text style={styles.item__text}>{item.title}</Text>
            <TouchableOpacity
              style={styles.item__btn}
              onPress={() => completedTaskHandler(item.id)}
            >
              <Ionicons name="checkbox-outline" size={24} color="#005826ff" />
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
    paddingTop: 20,
    alignItems: "center",
    backgroundColor: "#313131ff",
  },
  text: {
    color: "#fff",
    fontSize: 20,
    fontWeight: 700,
    marginBottom: 20,
  },
  input: {
    width: 240,
    height: 50,
    border: "1px solid #6e6e6eff",
    borderRadius: 5,
    color: "#fff",
    paddingHorizontal: 20,
    fontSize: 16,
    outlineWidth: 0,
  },
  input__btn: {
    width: 50,
    height: 50,
    backgroundColor: "#29f8c8ff",
    borderRadius: 5,
    justifyContent: "center",
    alignItems: "center",
    marginLeft: 15,
  },
  input__btn__text: {
    fontSize: 22,
  },
  wrapper: {
    flexDirection: "row",
    marginBottom: 20,
  },
  todo__item: {
    width: 305,
    height: 50,
    backgroundColor: "#5beec2ff",
    borderRadius: 5,
    paddingHorizontal: 10,
    alignItems: "center",
    marginTop: 10,
    flexDirection: "row",
  },
  item__text: {
    color: "#005826ff",
    fontSize: 16,
    fontWeight: 500,
  },
  item__btn: {
    marginLeft: "auto",
  },
});
