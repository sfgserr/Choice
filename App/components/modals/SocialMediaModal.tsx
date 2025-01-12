import {Dimensions, StyleSheet, Text, TouchableOpacity, View} from 'react-native';
import AnimatedModal from './AnimatedModal.tsx';
import React from 'react';
import BorderedTextInput from '../inputs/BorderedTextInput.tsx';
import Animated, {useAnimatedStyle, withDelay, withSpring, withTiming} from 'react-native-reanimated';
import CloseButton from '../buttons/CloseButton.tsx';
import {StyledButton} from '../buttons/StyledButton.tsx';

const d = Dimensions.get('screen');

export default function SocialMediaModal({isToggled, handlePress, title, onChange}: {
  isToggled: boolean
  handlePress: () => void
  title: string
  onChange: (s: string) => void
}) {
  const [value, setValue] = React.useState('');

  const duration = 1800;

  const animatedStyles = useAnimatedStyle(() => ({
    transform: [{ translateY: withSpring(isToggled ? (-50) : (d.height*0.312+5)) }],
  }));

  const animatedStyle = useAnimatedStyle(() => {
    return {
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
          onPress={() => handlePress()}
          disabled={!isToggled}
        />
      </Animated.View>
      <Animated.View style={[styles.popup, animatedStyles]}>
        <View>
          <View
            style={{
              flexDirection: 'row',
              justifyContent: 'space-between',
              paddingTop: 10,
            }}>
            <Text
              style={{
                fontWeight: '600',
                fontSize: 20,
              }}>
              {`Ссылка на ваш ${title}`}
            </Text>
            <CloseButton close={() => handlePress()} />
          </View>
          <View
            style={{
              paddingTop: 20,
            }}>
            <BorderedTextInput
              value={value}
              onChanged={setValue}
              placeholder={'Ссылка'}
              isError={false}
              isBig={false}
            />
          </View>
          <StyledButton
            content={'Сохранить'}
            top={20}
            bottom={5}
            isDisabled={false}
            pressed={() => {
              onChange(value);
              handlePress();
              setValue('');
            }}
          />
        </View>
      </Animated.View>
    </>
  );
}

const styles = StyleSheet.create({
  backdrop: {
    ...StyleSheet.absoluteFillObject,
    backgroundColor: 'transparent',
  },
  popup: {
    width: d.width * 0.9,
    paddingHorizontal: 15,
    height: 'auto',
    backgroundColor: 'white',
    borderRadius: 18,
    position: 'absolute',
    alignSelf: 'center',
    zIndex: 2,
    bottom: 0,
    shadowColor: 'black',
    shadowOffset: {
      width: 10,
      height: 10
    },
    shadowOpacity: 1,
    elevation: 5
  },
});
