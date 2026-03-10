
import { View, Text, StyleSheet, TouchableOpacity } from "react-native"
export default function ProfileScreen({ name, navigation }) {
    return (
        <View style={styles.container}>
            <Text style={styles.title}>Hello, {name} </Text>
            <TouchableOpacity style={styles.button}
                onPress={() => navigation.navigate("Settings")}
            >
                <Text style={styles.buttonText}>Go to Settings</Text>
            </TouchableOpacity>
            <TouchableOpacity
                style={styles.button}
                onPress={() => navigation.navigate("Welcome")}>
                <Text style={styles.buttonText} >Go back</Text>
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
    button: {
        backgroundColor: "#007AFF",
        padding: 15,
        borderRadius: 5,
        margin :20,
    },
    buttonText: {
        color: "#fff",
        fontSize: 16,
    }

})