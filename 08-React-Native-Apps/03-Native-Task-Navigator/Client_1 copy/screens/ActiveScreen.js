import { useState } from "react";
import { Ionicons } from "@expo/vector-icons";
import { View, Text, StyleSheet, TextInput, TouchableOpacity, FlatList } from "react-native";

export default function ActiveScreen({ todos, completedTaskHandler, addTodoHandler }) {

    const [input, setInput] = useState("");

    const submitHandler = () => {
        addTodoHandler(input);
        setInput("");
    };

    return (
        <View style={styles.container}>
            <Text style={styles.text}>Active Screen</Text>

            <View style={styles.wrapper}>
                <TextInput
                    placeholder="Enter the Text"
                    placeholderTextColor="#aaa"
                    value={input}
                    onChangeText={setInput}
                    style={styles.input}
                />

                <TouchableOpacity style={styles.input_btn} onPress={submitHandler}>
                    <Ionicons name="add-outline" size={28} color="#fff" />
                </TouchableOpacity>
            </View>

            <FlatList
                data={todos}
                keyExtractor={(item) => item.id}
                renderItem={({ item }) => (
                    <View style={styles.todo_item}>
                        <Text style={styles.item_text}>
                            {item.title}
                        </Text>

                        <TouchableOpacity
                            style={styles.item_btn}
                            onPress={() => completedTaskHandler(item.id)}
                        >
                            <Ionicons name="checkbox-outline" size={20} color="#005826ff" />
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
        padding: 20,
        backgroundColor: "#313131",
    },

    text: {
        color: "white",
        fontSize: 20,
        fontWeight: "700",
        marginBottom: 20,
    },

    wrapper: {
        flexDirection: "row",
        alignItems: "center",
        marginBottom: 20,
    },

    input: {
        width: 240,
        height: 50,
        borderWidth: 1,
        borderColor: "#6e6e6eff",
        borderRadius: 5,
        color: "#fff",
        paddingHorizontal: 20,
        fontSize: 16,
    },

    input_btn: {
        width: 50,
        height: 50,
        backgroundColor: "#29f8c8ff",
        borderRadius: 5,
        justifyContent: "center",
        alignItems: "center",
        marginLeft: 15,
    },

    todo_item: {
        width: 305,
        height: 50,
        backgroundColor: "#5beec2ff",
        borderRadius: 5,
        paddingHorizontal: 10,
        alignItems: "center",
        marginTop: 10,
        flexDirection: "row",
    },

    item_text: {
        color: "#005826ff",
        fontSize: 16,
        fontWeight: "500",
    },

    item_btn: {
        marginLeft: "auto",
    },
});
