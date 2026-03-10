import { StatusBar } from "expo-status-bar";
import React, { useState } from "react";
import { StyleSheet, View, Text } from "react-native";
import { NavigationContainer } from "@react-navigation/native";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import HomeScreen from "./screen/HomeScreen";
import SearchScreen from "./screen/SearchScreen";
import { Ionicons } from "@expo/vector-icons";
import DetailsScreen from "./screen/DetailsScreen";

const Tab = createBottomTabNavigator();

export default function App() {
  return (
    <NavigationContainer>
      <Tab.Navigator
        screenOptions={({ route }) => ({
          tabBarIcon: ({ color, size }) => {
            let icon;

            if (route.name === "Home") icon = "home";
            if (route.name === "Search") icon = "person";
            if (route.name === "Details") icon = "ellipsis-horizontal-outline";
            return <Ionicons name={icon} size={size} color={color} />;
          },
        })}
      >
        <Tab.Screen name="Home" component={HomeScreen} />
        <Tab.Screen name="Search" component={SearchScreen} />
        <Tab.Screen name="Details" component={DetailsScreen} />
      </Tab.Navigator>
    </NavigationContainer>
  );
}
