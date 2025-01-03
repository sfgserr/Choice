import {ActivityIndicator, StyleSheet} from 'react-native';
import Animated, {useAnimatedStyle, withDelay, withSpring, withTiming} from 'react-native-reanimated';
import React from 'react';

export default function LongRunningOperationIndicator({isRefreshing}: {isRefreshing: boolean}) {
  const duration = 1800;

  const animatedStyle = useAnimatedStyle(() => {
    return {
      opacity: withSpring(isRefreshing ? 1 : 0, {duration}),
      zIndex: isRefreshing
        ? 1
        : withDelay(duration, withTiming(-1, { duration: 0 })),
    };
  });

  return (
    <Animated.View style={[styles.backdrop, animatedStyle]}>
      <ActivityIndicator size='large' color='#2D81E0'/>
    </Animated.View>
  )
}

const styles = StyleSheet.create({
  backdrop: {
    ...StyleSheet.absoluteFillObject,
    backgroundColor: 'rgba(0, 0, 0, 0.3)',
    justifyContent: 'center'
  },
})
