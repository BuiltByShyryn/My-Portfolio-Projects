
import { StyleSheet, Text, View } from 'react-native';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import SearchScreen from './screens/SearchScreen';
import DetailsScreen from './screens/DetailsScreen';
import FavoritesScreen from './screens/FavoritesScreen';
import { useState } from 'react';




export default function App() {
  const Stack = createNativeStackNavigator();
  const [favorites, setFavorites] = useState([]);
  return (
    <NavigationContainer>
      <Stack.Navigator >
        <Stack.Screen name="Search" component={SearchScreen} />
        <Stack.Screen name="Details">
          {props => <DetailsScreen {...props} favorites={favorites} setFavorites={setFavorites} />}
        </Stack.Screen>

        <Stack.Screen name="Favorites">
          {props => <FavoritesScreen {...props} favorites={favorites} setFavorites={setFavorites} />}
        </Stack.Screen>

      </Stack.Navigator>
    </NavigationContainer>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
