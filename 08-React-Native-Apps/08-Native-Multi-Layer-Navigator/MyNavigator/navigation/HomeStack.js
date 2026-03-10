
import { createStackNavigator } from '@react-navigation/stack';
import Home from '../screens/Home';
import DetailsScreen from '../screens/DetailsScreen';

const Stack = createStackNavigator();

export default function HomeStack() {
  return (
    <Stack.Navigator>
     <Stack.Screen name="HomeScreen" component={Home}   options={{ headerShown: false }}  />
 <Stack.Screen name="Details" component={DetailsScreen} />
    </Stack.Navigator>
  );
}
