import { View, Text, StyleSheet } from 'react-native';

export default function DetailsScreen({ route }) {
  const { itemId, message } = route.params;

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Details Screen</Text>
      <Text style={styles.label}>Item ID:</Text>
      <Text style={styles.value}>{itemId}</Text>
      <Text style={styles.label}>Message:</Text>
      <Text style={styles.value}>{message}</Text>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f2f2f2', 
    alignItems: 'center',
    justifyContent: 'center',
    padding: 20,
  },
  title: {
    fontSize: 26,
    fontWeight: '700',
    marginBottom: 20,
    color: '#333',
    
  },
  label: {
    fontSize: 18,
    fontWeight: '600',
    marginTop: 10,
    color: '#555',
   
  
  },
  value: {
    fontSize: 18,
    color: '#000',
    
  },
});
