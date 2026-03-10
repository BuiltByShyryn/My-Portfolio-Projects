
import { useState } from "react"
import { View, Text, TouchableOpacity, StyleSheet,TextInput } from "react-native"

export default function WelcomeScreenn({ navigation }) {
    const [name, setName] = useState("")
    return (
        <View style={styles.container}>
            <Text style={styles.title}>Welcome!</Text>
            <TextInput placeholder="Enter your name" value={name}
                onChangeText={text => setName(text)}
                style={styles.input}
            />
            <TouchableOpacity style={styles.button}
                onPress={() => navigation.navigate("Profile", { name })}
            >
                <Text style={styles.buttonText}>Continue</Text>
            </TouchableOpacity>
        </View>
    )
}
const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: "center",
        alignItems: "center",
        padding: 20,
    },
    title: {
        fontSize: 24,
        marginBottom: 20,
    },
    input: {
        borderWidth: 1,
        borderColor: "#CCC",
        width: "100%",
        padding: 10,
        marginBottom: 20,
    },
    button: {
        backgroundColor: "#007AFF",
        padding: 15,
        borderRadius: 5,
    },
    buttonText: {
        color: "#fff",
        fontSize: 16,
    }
})