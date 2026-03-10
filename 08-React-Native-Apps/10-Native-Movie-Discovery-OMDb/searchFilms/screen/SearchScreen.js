import { useEffect, useState } from "react";
import { View, FlatList, Text, TextInput, StyleSheet,} from "react-native";
import { searchMovies } from "../api/movieApi";

import MovieCard from "../components/movieCard";

export default function SearchScreen() {
  const [search, setSearch] = useState('');
  const [movies, setMovies] = useState([])

  useEffect(() => {

    if (search.length > 2) {
      loadMovies();
    } else {
      setMovies([]);
    }
  }, [search]);
  const loadMovies = async () => {
    const data = await searchMovies(search);
    setMovies(data)
  }

  return (
    <View style={styles.container}>
      <TextInput
        style={{ borderWidth: 1, margin: 10, padding: 10, borderRadius: 8 }}
        value={search} onChangeText={search => setSearch(search)} />
      <FlatList
        data={movies}
        keyExtractor={item => item.imdbID}
        renderItem={({ item }) => (<MovieCard movie={item} />

        )}
      />

    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,


  }
})