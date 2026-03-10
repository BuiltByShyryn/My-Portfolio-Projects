
import { useEffect, useState } from "react";
import { StyleSheet, View, Text, FlatList, TouchableOpacity, Image } from "react-native"

export default function FavoritesScreen({ favorites, setFavorites }) {


    const remove = (id) => {
        setFavorites((prev) => prev.filter((_, index) => index !== id))
    }
    return (
        <View style={styles.container}>
            <Text>FavoritesScreen</Text>
            <FlatList
                data={favorites}
                keyExtractor={(item, index) => index.toString()}
                renderItem={({ item ,index}) => (
                    <View style={styles.item}>
                        <View style={styles.titleRow}>
                            <Text style={styles.bookTitle}>{item.title}</Text>
                            <Image
                                source={{
                                    uri: item.cover_i
                                        ? `https://covers.openlibrary.org/b/id/${item.cover_i}-M.jpg`
                                        : "https://via.placeholder.com/50"
                                }}
                                style={styles.image}
                            />
                        </View>

                        <Text style={styles.bookAuthor}>
                            {item.author_name ? item.author_name.join(", ") : "Unknown Author"}

                        </Text>
                        <TouchableOpacity style={styles.button}
                            onPress={() => remove(index)}

                        >
                            <Text style={styles.buttonText}>Delete</Text>
                        </TouchableOpacity>
                    </View>
                )}
            />

        </View>
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
    },
    titleRow: {
        flexDirection: "row",
        alignItems: "center", 
        marginBottom: 1,
    },
    image: {
        width:40,
        height: 40,
        resizeMode: "contain",

    },
    title: {
        fontSize: 24,
        marginBottom: 20,
    },
    item: {
        borderWidth: 1,
        borderColor: "gray",
        padding: 10,
        marginBottom: 10,
        alignItems: "flex-start",
        textAlign: "left"
    },
    bookTitle: {
        fontSize: 18,
        fontWeight: "500",
        marginTop:20,

    },
    bookAuthor: {
        fontSize: 16,
        color: "gray",
    },
    buttonText: {
        alignItems: "center",
        fontSize: 18,
        fontWeight: 400,
        color: "#fff",
    }, button: {
        backgroundColor: "#7400c2ff",
        marginBottom: 5,
        marginTop: 10,
        width: "100%",
        height: 50,
        borderRadius: 5,
        alignItems: "center",
        justifyContent: "center",
    },

})