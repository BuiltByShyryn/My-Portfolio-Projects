
import { StyleSheet, View, Text, Image, TouchableOpacity } from "react-native"

export default function DetailsScreen({ route, navigation, setFavorites }) {
    const book = route.params.book;

    return (
        <View style={styles.container}>
            <View style={{ borderWidth: 1, padding: 10, borderColor: "gray", }}>
                <Text style={styles.title}>DetailsScreen</Text>
                <Text style={styles.title__book}>Book title: {book.title}</Text>
                <Text style={styles.author}>Authors: {book.author_name ? book.author_name.join(", ") : "Unknown"}</Text>
                <Text style={styles.year}>Year:{book.first_publish_year ? book.first_publish_year : "No data"} </Text>
                <Image
                    source={{
                        uri: book.cover_i
                            ? `https://covers.openlibrary.org/b/id/${book.cover_i}-M.jpg`
                            : "https://via.placeholder.com/100x150"
                    }}
                    style={styles.image}
                />
                <TouchableOpacity
                    style={styles.button}
                    onPress={() => {
                        setFavorites((prev) => [...prev, book]);
                        navigation.navigate("Favorites");
                    }}
                >
                    <Text style={styles.buttonText}>Add to favorites</Text>
                </TouchableOpacity>

                <TouchableOpacity style={styles.button}
                    onPress={() => navigation.goBack()}
                >
                    <Text style={styles.buttonText}>Go Back</Text>
                </TouchableOpacity>
            </View >
        </View >
    )
}

const styles = StyleSheet.create({

    container: {
        flex: 1,
        justifyContent: "center",
        alignItems: "center",
        paddingHorizontal: 20,
        alignItems: "baseline",


    }, button: {
        backgroundColor: "#7400c2ff",
        marginBottom: 5,
        marginTop: 10,
        width: "100%",
        height: 50,
        borderRadius: 5,
        alignItems: "center",
        justifyContent: "center",

    }, buttonText: {
        alignItems: "center",
        fontSize: 18,
        fontWeight: 400,
        color: "#fff"
    },
    title: {
        fontSize: 20,
        marginBottom: 30, fontWeight: 500,

    }, image: {
        width: 160,
        height: 250,
        resizeMode: 'contain',
        marginTop: 20,

    }, title__book: {
        fontSize: 18,
        fontWeight: 400,
        color: "black",

    }, author: {
        color: "black",
        fontSize: 18,
        fontWeight: 400,
    }, year: {
        fontSize: 18,
        fontWeight: 400,
    }
})