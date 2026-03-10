import { StyleSheet } from 'react-native';
import { NavigationContainer } from '@react-navigation/native';
import { createDrawerNavigator } from '@react-navigation/drawer';
import ActiveScreen from './screens/ActiveScreen';
import CompletedScreen from './screens/CompletedScreen';
import { useState } from 'react';

const Drawer = createDrawerNavigator();

export default function App() {

  const [todos, setTodos] = useState([]);
  const [completed, setCompleted] = useState([]);

  const addTodoHandler = (title) => {
    if (!title.trim()) return;
    setTodos([...todos, { id: Date.now().toString(), title }]);
  };

  const completedTaskHandler = (id) => {
    const doneElem = todos.find((item) => item.id === id);
    if (!doneElem) return;


    setCompleted([...completed, doneElem]);

   
    setTodos(todos.filter((todo) => todo.id !== id));
  };

  return (
    <NavigationContainer>
      <Drawer.Navigator initialRouteName="Activity">

        <Drawer.Screen name="Activity">
          {(props) => (
            <ActiveScreen
              {...props}
              todos={todos}
              addTodoHandler={addTodoHandler}
              completedTaskHandler={completedTaskHandler}
              completed={completed}
            />
          )}
        </Drawer.Screen>

        <Drawer.Screen name="Completed">
          {(props) => (
            <CompletedScreen
              {...props}
              completed={completed}
            />
          )}
        </Drawer.Screen>

      </Drawer.Navigator>
    </NavigationContainer>
  );
}

const styles = StyleSheet.create({
  container: {},
});
