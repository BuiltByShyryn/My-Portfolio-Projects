import { StyleSheet } from 'react-native';
import { NavigationContainer } from '@react-navigation/native';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import ActiveScreen from './screens/ActiveScreen';
import CompletedScreen from './screens/CompletedScreen';
import { Ionicons } from '@expo/vector-icons';
import { useState } from 'react';


const Tab = createBottomTabNavigator();

export default function App() {

  const [todos, setTodos] = useState([]);
  const [completed, setCompleted] = useState([]);

  const addTodoHandler = (title) => {
    if (!title.trim()) return;
    setTodos([...todos, { id: Date.now().toString(), title }]);
  };

  const compledtedTaskHandler = (id) => {
    const doneElem = todos.find((item) => item.id === id);
    setTodos(todos.filter((todo) => todo.id !== id));
    setCompleted([...completed, doneElem]);
  };

  return (
    <NavigationContainer>
      <Tab.Navigator
        screenOptions={({ route }) => ({
          tabBarIcon: ({ color, size }) => {
            let icon;

            if (route.name === "Activity") {
              icon = 'document-outline';
            } else if (route.name === "Completed") {
              icon = 'checkmark-done-outline';
            }

            return <Ionicons name={icon} size={size} color={color} />;
          }
        })}
      >
        <Tab.Screen name="Activity">
          {(props) => (
            <ActiveScreen
              {...props}
              todos={todos}
              addTodoHandler={addTodoHandler}
              compledtedTaskHandler={compledtedTaskHandler}
              completed={completed}
            />
          )}
        </Tab.Screen>

        <Tab.Screen name="Completed">
          {(props) => (
            <CompletedScreen
              {...props}
              completed={completed}
            />
          )}
        </Tab.Screen>
      </Tab.Navigator>
    </NavigationContainer>
  );
}

const styles = StyleSheet.create({
  container: {},
});
