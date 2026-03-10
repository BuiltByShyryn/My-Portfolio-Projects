import { useEffect, useState } from "react";
import { View, Text, Image, StyleSheet } from "react-native";
import { getMovieDetails } from "../api/movieApi";
import { ScrollView } from "react-native-web";

export default function DetailsScreen({ route }) {
  const { id } = route.params;
  const [movie, setMovie] = useState({});
  useEffect(() => {
    load();
  }, []);

  const load = async () => {
    const data = await getMovieDetails(id);
    setMovie(data);
  };

  return (
    <ScrollView style={{ flex: 1 }}>
      <Image
        style={styles.image}
        source={{
          uri:
            movie.Poster !== "N/A"
              ? movie.Poster
              : "https://placehold.co/300x450",
        }}
        resizeMode="contain"
      />
      <View style={styles.box}>
        <Text style={styles.title}>{movie.Title}</Text>
        <Text style={styles.info}>Год: {movie.Year}</Text>
        <Text style={styles.info}>Жанр: {movie.Genre}</Text>
        <Text style={styles.info}>Рейтинг: {movie.rating}</Text>
      </View>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  box: {},
  title: {},
  info: {},
});
