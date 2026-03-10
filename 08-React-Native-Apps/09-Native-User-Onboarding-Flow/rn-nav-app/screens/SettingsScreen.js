
import { View, Text, StyleSheet, FlatList, TouchableOpacity } from "react-native"


const settings = [
    { id: '1', title: 'Изменить тему' },
    { id: '2', title: 'Уведомления' },
    { id: '3', title: 'О приложении' },
    { id: '4', title: 'Конфиденциальность' }];

export default function SettingsScreen() {

    return (
        <View style={styles.container}>
            <Text style={styles.title}>
                Settings
            </Text>
            <FlatList
                data={settings}
                keyExtractor={item => item.id}
                renderItem={({ item }) => (
                    <TouchableOpacity onPress={() => alert(item.title)}
                        style={styles.button}>
                        <Text style={styles.buttonText}>{item.title}</Text>
                    </TouchableOpacity>
                )}

            />
            <TouchableOpacity style={styles.button} onPress={() => navigation.navigate("Profile")}>
                <Text style={styles.buttonText}>Back to Profile</Text>
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
    }, button: {
        backgroundColor: "#007AFF",
        padding: 15,
        borderRadius: 5,
        margin: 20,
    },
    buttonText: {
        color: "#fff",
        fontSize: 16,
    }
})