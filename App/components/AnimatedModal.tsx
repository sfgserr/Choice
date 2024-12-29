import {Button, Dimensions, StyleSheet, View} from 'react-native';
import Animated, {
  useAnimatedStyle,
  useSharedValue,
  withSpring,
  withTiming,
} from 'react-native-reanimated';
import React from 'react';

const { height } = Dimensions.get("window");

export default function AnimatedModal() {
  const [visible, setVisible] = React.useState(false);
  const translateY = useSharedValue(height); // Initial position: off-screen

  const togglePopup = () => {
    if (visible) {
      // Slide down to hide
      translateY.value = withSpring(height);
    } else {
      // Slide up to show
      translateY.value = withSpring(height * 0.7); // Adjust 0.7 to position the popup
    }
    setVisible(!visible);
  };

  const animatedStyle = useAnimatedStyle(() => {
    return {
      transform: [{ translateY: translateY.value }],
    };
  });

  return (
    <View style={styles.container}>
      <Button title="Toggle Popup" onPress={togglePopup} />
      <Animated.View style={[styles.popup, animatedStyle]}>
        <View style={styles.innerPopup} />
      </Animated.View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "#f0f0f0",
  },
  popup: {
    position: "absolute",
    width: "100%",
    height: height * 0.3, // Adjust this value for the popup height
    backgroundColor: "red",
    borderTopLeftRadius: 20,
    borderTopRightRadius: 20,
    bottom: 0,
    shadowColor: "#000",
    shadowOffset: { width: 0, height: -2 },
    shadowOpacity: 0.25,
    shadowRadius: 4,
    elevation: 5,
  },
  innerPopup: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "#ddd",
  },
});
