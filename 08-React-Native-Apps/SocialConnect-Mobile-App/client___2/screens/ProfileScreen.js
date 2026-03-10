import { useActionState, useContext, useState } from "react";
import { View, Text, StyleSheet, Touchable, TouchableOpacity } from "react-native";
import { TextInput } from "react-native-gesture-handler";
import { ProfileContext } from "../context/AppContext";

export default function ProfileScreen() {
  const [name, setName] = useState('')
  const [email, setEmail] = useState('')
  const [phone, setPhone] = useState('')
  const [about, setAbout] = useState('')
  const {profile, UpdateProfile} = useContext(ProfileContext);
  const formSaveHandler =()=>{
UpdateProfile({name, email,phone,about});
setName("");
setEmail("");
setPhone("");
setAbout("");
  }
  return (
    <View style={styles.container}>
      <Text style={styles.text}>Edit Profile</Text>
      <View style={styles.box}>
        <Text style={styles.label}>Your Name:</Text>
        <TextInput value={name}
          onChangeText={(text) => setName(text)}
          style={styles.input} />
        <Text style={styles.label}>Your Email:</Text>
        <TextInput value={email}
          onChangeText={(text) => setEmail(text)}
          style={styles.input} />
        <Text style={styles.label}>Your Phone Number:</Text>
        <TextInput value={phone}
          onChangeText={(text) => setPhone(text)}
          style={styles.input} />
        <Text style={styles.label}>About Yourself:</Text>
        <TextInput value={about}
          onChangeText={(text) => setAbout(text)}
          style={styles.input} />
      </View>
      <TouchableOpacity style={styles.save__btn}
      onPress={formSaveHandler}
      >
        <Text style={styles.save__btn__text}>Save</Text>
      </TouchableOpacity>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,

  },
  text: {
    fontSize: 22,
    marginBottom: 20,
  },
  label: {
    marginTop: 10,
    fontSize: 14,
    opacity: 0.6,
    marginBottom: 5,
  },
  box: {
    backgroundColor: "#fff",
    paddingHorizontal: 40,
    paddingVertical: 20,
  },
  input: {
    width: "100%",
    height: "50",
    borderWidth: 1,
    borderColor: "#ddd",
    padding: 12,
    borderRadius: 10,
    marginBottom: 10,
    backgroundColor: "#fafafa"

  },
  save__btn: {
    backgroundColor: "#232323ff",
    paddingVertical: 14,
    alignItems: "center",
    borderRadius: 12,

  },
  save__btn__text: {
    color: "#fff",
    fontSize: 18,
    fontWeight: 700
}
});
