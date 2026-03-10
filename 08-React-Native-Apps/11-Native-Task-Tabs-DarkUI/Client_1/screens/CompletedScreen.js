import { View, Text, StyleSheet, FlatList } from 'react-native';

export default function CompletedScreen({ completed }) {

    return (
        <View style={styles.container}>
            <Text style={styles.text}>Completed Screen</Text>

            <FlatList
                data={completed}
                keyExtractor={(item) => item.id}
                renderItem={({ item }) => (
                    <View style={styles.todo_item}>
                        <Text style={styles.item_text}>
                            {item.title}
                        </Text>
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
        alignItems: 'center',
        backgroundColor: "#313131",
    },

    text: {
        color: "#fff",
        fontSize: 20,
        marginBottom: 20,
    },

    todo_item: {
        width: 305,
        height: 50,
        backgroundColor: "#d55bee",
        borderRadius: 5,
        paddingHorizontal: 10,
        alignItems: "center",
        justifyContent: "center",
        marginTop: 10,
        flexDirection: "row",
    },

    item_text: {
        color: "#8b03a6",
        fontSize: 16,
        fontWeight: "500",
    },
});
