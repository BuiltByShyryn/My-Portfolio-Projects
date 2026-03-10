
import { View, Text, StyleSheet } from "react-native"

export default function UserCard({ name, email }) {
    return (
        <View style={styles.card}>
            <Text style={{ fontWeight: 'bold', fontSize: 16 }}>{name}</Text>
            <Text style={{ color: '#555' }}>{email}</Text>
        </View>
    )
}
const styles = StyleSheet.create({
    card: {
        padding: 15,
        marginBottom: 10,
        borderWidth: 1,
        borderColor: '#ccc',
        borderRadius: 8,
        backgroundColor: '#fff',
        shadowColor: '#000',
        shadowOpacity: 0.1,
        shadowRadius: 4,
        elevation: 3,
    }
});
