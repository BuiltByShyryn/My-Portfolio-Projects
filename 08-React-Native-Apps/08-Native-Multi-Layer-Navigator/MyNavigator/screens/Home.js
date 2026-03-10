import { View, Text, StyleSheet,Button } from "react-native"

export default function Home({ navigation }) {
    return (
        <View style={styles.container}>
            <Text style={styles.text}></Text>
            <Button
                title="Go to Details"
                onPress={() =>
                    navigation.navigate('Details', { itemId: 1, message: 'Hello from Home' })
                }
            />
        </View>
    )
}
const styles = StyleSheet.create({
    container: {
        flex: 1,
        alignItems: "center",
        justifyContent: "center"
    }
    ,
    text: {
        fontSize: 20,
        fontWeight: 600
    }
})