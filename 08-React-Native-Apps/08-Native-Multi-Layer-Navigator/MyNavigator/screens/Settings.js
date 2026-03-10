import { View, Text, StyleSheet, } from "react-native"

export default function Settings() {
    return (
        <View>
            <Text style={styles.text}> </Text>
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