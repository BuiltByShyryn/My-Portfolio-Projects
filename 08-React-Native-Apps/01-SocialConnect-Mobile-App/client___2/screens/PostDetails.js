
import { useContext } from "react";
import { View, Text, StyleSheet } from "react-native";
import { AppContext } from "../context/AppContext";
import { Button } from "react-native-web";



export default function PostDetails({ route, navigation }) {

    const { postId } = route.params;
    const { posts } = useContext(AppContext);

    const post = posts.find((item) => item.id === postId);

    return (
        <View style={styles.postBlock}>

            <Text style={styles.post__title}> {post.title}</Text>
            <Text style={styles.post__body}>{post.text}</Text>
            <Button title='Go Back' onPress={() => navigation.goBack()}></Button>

        </View>
    )
}

const styles = StyleSheet.create({
    postBlock: {

    },
    post__title: {

    },
    post__body: {

    },
})