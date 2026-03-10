const API = "https://www.omdbapi.com/?apikey=15558de";

export const searchMovies = async (query) => {
  const res = await fetch(`${API}&s=${query}`);
  const data = await res.json();
  return data.Search;
};

export const getMovieDetails = async (id) => {
  const res = await fetch(`${API}&i=${id}`);
  const data = await res.json();
  return data;
};
