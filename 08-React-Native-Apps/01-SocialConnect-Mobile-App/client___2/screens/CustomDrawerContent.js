import {
  DrawerContentScrollView,
  DrawerItemList,
} from "@react-navigation/drawer";
import {
  View,
  StyleSheet,
  Image,
  Text,
  TouchableOpacity,
  Alert,
} from "react-native";
import icon from "../assets/user-img.svg";
import { useContext } from "react";
import { AppContext } from "../context/AppContext";

export default function CustomDrawerContent(props) {
const {profile} = useContext(AppContext);

  const onLogautHandler = () => {
    console.log("logout");
    Alert.alert("Выход", "Выйти из аккаунта?", [
      { text: "Отмена", style: "cancel" },
      {
        text: "Выйти",
        style: "destructive",
        onPress: () => console.log("logout"),
      },
    ]);
  };
  return (
    <View style={styles.container}>
      <DrawerContentScrollView>
        <View style={styles.header}>
          <Image source={icon} style={styles.avatar} />
          <Text style={styles.name}>{profile.name}</Text>
          <Text style={styles.email}>{profile.email}</Text>
          <Text style={styles.email}>{profile.about}</Text>
        </View>
        <DrawerItemList {...props} />
      </DrawerContentScrollView>
      <TouchableOpacity style={styles.footer__btn} onPress={onLogautHandler}>
        <Text style={styles.btn__text}>Выйти</Text>
      </TouchableOpacity>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  header: {
    alignItems: "center",
    padding: 20,
    backgroundColor: "#f7f7f7ff",
    borderRadius: 10,
  },
  name: {
    fontSize: 18,
    fontWeight: 700,
    marginTop: 20,
  },
  avatar: {
    width: 100,
    height: 100,
  },
  email: {
    color: "#666",
    fontSize: 12,
    marginTop: 5,
  },
  footer__btn: {
    width: "100%",
    height: 50,
    backgroundColor: "#8ed0f4ff",
    justifyContent: "center",
    alignItems: "center",
  },
  btn__text: {
    color: "#fff",
    fontSize: 18,
    fontWeight: 500,
  },
});
