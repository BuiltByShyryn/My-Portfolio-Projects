import { useEffect, useState } from "react";
import {
  View,
  Text,
  StyleSheet,
  Pressable,
  TextInput,
  TouchableOpacity,
} from "react-native";
import { searchMovies } from "../api/movieApi";
import MovieCard from "../components/movieCard";
import { ScrollView, FlatList } from "react-native-web";

export default function HomeScreen({ navigation }) {
  const [movies, setMovies] = useState([]);

  useEffect(() => {
    loadMovies();
  }, []);

  const loadMovies = async () => {
    const data = await searchMovies("Batman");
    console.log(data);
    setMovies(data);
  };

  return (
    <View style={styles.container}>
      <TouchableOpacity>
        <Text>Поиск</Text>
      </TouchableOpacity>

      <FlatList
        data={movies}
        keyExtractor={(item) => item.imdbID}
        renderItem={({ item }) => (
          <MovieCard
            movie={item}
            onPress={() => navigation.navigate("Details", { id: item.imdbID })}
          />
        )}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
});
