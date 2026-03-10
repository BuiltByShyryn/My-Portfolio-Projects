
import { useState } from "react"
import { StyleSheet, View, Text, TouchableOpacity, FlatList, TextInput, Image } from "react-native"



export default function SearchScreen({ navigation }) {
    const [book, setBook] = useState("")
    const [results, setResults] = useState([])
    const [loading, setLoading] = useState(false);
    const api = 'https://openlibrary.org/search.json'
    const searchBooks = async () => {
        setLoading(true);
        const response = await fetch(`${api}?title=${book}`);
        const data = await response.json();
        setResults(data);
        console.log(data);
        setLoading(false);
        setBook("")


    }
    return (
        <View style={{ flex: 1, padding: 20 }}>
            <View style={styles.container}>
                {loading && <Text style={styles.loading}>Loading...</Text>}
                <Text style={styles.title}>SearchScreen</Text>

                <TextInput
                    value={book}
                    onChangeText={text => setBook(text)}
                    style={styles.input} />
                <TouchableOpacity
                    onPress={searchBooks}
                    style={styles.button}>
                    <Text style={styles.buttonText}>Search</Text>
                </TouchableOpacity>
                <FlatList
                    data={results.docs}
                    keyExtractor={(item, index) => index.toString()}
                    renderItem={({ item }) => (
                        <TouchableOpacity onPress={() => navigation.navigate("Details", { book: item })}>
                            <View style={{ marginBottom: 10, borderWidth: 1, justifyContent: 'center', }}>
                                <Text style={styles.title_name}>Title:{item.title}</Text>
                                <Text style={styles.author}>
                                    Author: {item.author_name ? item.author_name.join(", ") : "Unknown Author"}
                                </Text>
                                <Image
                                    source={{ uri: `https://covers.openlibrary.org/b/id/${item.cover_i}-M.jpg` }}
                                    style={styles.image}
                                />

                            </View>
                        </TouchableOpacity>
                    )}
                />


            </View>
        </View>
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: "center",
        alignItems: "center",
        paddingHorizontal: 20

    },
    image: {
        width: 80,
        height: 130,
        padding: 10,
        marginBottom: 20,
        marginLeft: 10,


    },
    author: {
        fontSize: 16,
        fontWeight: 500,
        padding: 10,
        color: "gray",

    }, title_name: {
        fontWeight: 600,
        fontSize: 18,
        color: "#7400c2",
        padding: 10,

    },
    loading: {

        fontSize: 18,
        fontWeight: 600,
        color: "7400c2ff"

    },

    input: {
        borderWidth: 1,
        borderColor: "black",
        width: "100%",
        height: 50,
        borderRadius: 5,
        marginBottom: 15,
        padding: 5

    }, button: {
        backgroundColor: "#7400c2ff",
        height: 50,
        width: "100%",
        borderRadius: 5,
        justifyContent: "center",
        alignItems: "center",
    }, title: {
        fontSize: 20,
        marginBottom: 30,

    }, buttonText: {
        fontSize: 20,
        fontWeight: "500",


    }

})