import {
  View,
  Text,
  StyleSheet,
  TouchableOpacity,
  FlatList,
} from "react-native";

export default function CompletedScreen({ completed }) {
  return (
    <View style={styles.container}>
      <Text style={styles.text}>Completed Screen</Text>
      <FlatList
        data={completed}
        keyExtractor={(item) => item.id}
        renderItem={({ item }) => (
          <View style={styles.todo__item}>
            <Text style={styles.item__text}>{item.title}</Text>
          </View>
        )}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: "center",
    backgroundColor: "#313131ff",
    paddingTop: 20,
  },
  text: {
    color: "#fff",
    fontSize: 20,
  },
  todo__item: {
    width: 305,
    height: 50,
    backgroundColor: "#d55beeff",
    borderRadius: 5,
    paddingHorizontal: 10,
    alignItems: "center",
    marginTop: 10,
    flexDirection: "row",
  },
  item__text: {
    color: "#8b03a6ff",
    fontSize: 16,
    fontWeight: 500,
  },
});
