import { useEffect, useState } from 'react';
import { View, Text, StyleSheet, FlatList } from 'react-native';
import UserCard from '../components/UserCard';


export default function UsersScreen() {
    const [users, setUsers] = useState([])
    const [loading, setLoading] = useState(true)

    useEffect(() => {
        fetch("https://jsonplaceholder.typicode.com/users")
            .then(res => res.json())
            .then(data => {
                setUsers(data)
                setLoading(false)
            })


    }, [])

    if (loading) {
        return (
            <View>
                <Text>Loading...</Text>
            </View>
        )
    }
    return (
        <View style={{ flex: 1, padding: 10 }}>

            <FlatList
                data={users}
                keyExtractor={item => item.id.toString()}
                renderItem={({ item }) =>
                    <UserCard name={item.name} email={item.email} />
                }
            />
        </View>
    )
}