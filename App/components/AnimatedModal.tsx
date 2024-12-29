import {Dimensions, StyleSheet, TouchableOpacity} from 'react-native';
import Animated, {
  useAnimatedStyle,
  withDelay,
  withSpring,
  withTiming,
} from 'react-native-reanimated';
import React from 'react';

const d = Dimensions.get("window");

export default function AnimatedModal({isToggled, handlePress, children}) {
  const duration = 1800;

  const animatedStyles = useAnimatedStyle(() => ({
    transform: [{ translateY: withSpring(isToggled ? (-50) : (d.height*0.312+5)) }],
  }));

  const animatedStyle = useAnimatedStyle(() => {
    return {
      opacity: withSpring(isToggled ? 1 : 0, {duration}),
      zIndex: isToggled
        ? 1
        : withDelay(duration, withTiming(-1, { duration: 0 })),
    };
  });

  return (
    <>
      <Animated.View style={[styles.backdrop, animatedStyle]}>
        <TouchableOpacity
          style={{flex: 1}}
          onPress={() => {
            if (isToggled)
              handlePress();
          }}
        />
      </Animated.View>
      <Animated.View style={[styles.popup, animatedStyles,]}>
        {children}
      </Animated.View>
    </>
  );
}

const styles = StyleSheet.create({
  backdrop: {
    ...StyleSheet.absoluteFillObject,
    backgroundColor: 'rgba(0, 0, 0, 0.3)',
  },
  popup: {
    height: d.height * 0.312,
    width: d.width * 0.9,
    backgroundColor: 'white',
    borderRadius: 18,
    position: 'absolute',
    alignSelf: 'center',
    zIndex: 2,
    bottom: 0,
  },
});
