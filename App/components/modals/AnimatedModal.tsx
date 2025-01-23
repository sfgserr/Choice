import {Dimensions, StyleSheet, View} from 'react-native';
import Animated, {
  useAnimatedStyle,
  withDelay,
  withSpring,
  withTiming,
} from 'react-native-reanimated';
import React from 'react';
import CloseButton from '../buttons/CloseButton.tsx';
import {AnimatedModalProps} from '../../types/ComponentTypes.ts';
import {TouchableOpacity} from 'react-native-gesture-handler';

const d = Dimensions.get("window");

export default function AnimatedModal({isToggled, handlePress, children, withBackdrop}: AnimatedModalProps) {
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
      {withBackdrop ? (
        <>
          <Animated.View style={[styles.backdrop, animatedStyle]}>
            <TouchableOpacity
              style={{flex: 1}}
              onPress={() => handlePress()}
              disabled={!isToggled}/>
          </Animated.View>
        </>) : (<></>)}
      <Animated.View
        style={[
          styles.popup,
          withBackdrop ? styles.fixedHeight : [styles.autoHeight, styles.shadow],
          animatedStyles
        ]}>
        {withBackdrop ? (
          <>
            <View style={styles.closeButtonContainer}>
              <CloseButton
                close={() => handlePress()}/>
            </View>
          </>) : (<></>)}
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
    width: d.width * 0.9,
    backgroundColor: 'white',
    borderRadius: 18,
    position: 'absolute',
    alignSelf: 'center',
    zIndex: 2,
    bottom: 0,
  },
  fixedHeight: {
    height: d.height * 0.312
  },
  autoHeight: {
    height: 'auto'
  },
  closeButtonContainer: {
    position: 'absolute',
    top: 10,
    right: 10
  },
  shadow: {
    shadowColor: 'black',
    shadowOffset: {
      width: 10,
      height: 10
    },
    shadowOpacity: 1,
    elevation: 5
  }
});
