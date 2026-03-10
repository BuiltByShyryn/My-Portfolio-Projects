import { Pressable, StyleSheet, Image, Text } from "react-native";

export default function MovieCard({ movie, onPress }) {
  return (
    <Pressable style={styles.card} onPress={onPress}>
      <Image
        style={styles.image}
        source={{
          uri:
            movie.Poster !== "N/A"
              ? movie.Poster
              : "https://placehold.co/300x450",
        }}
      />
      <Text style={styles.title}>{movie.Title}</Text>
    </Pressable>
  );
}

const styles = StyleSheet.create({
  card: {
    width: "100%",
    marginBottom: 20,
  },
  image: {
    width: "100%",
    height: 220,
    borderRadius: 10,
    backgroundColor: "#ddd",
  },
  title: {
    marginTop: 10,
    fontWeight: 600,
    fontSize: 20,
  },
});
