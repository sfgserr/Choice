import AnimatedModal from './AnimatedModal.tsx';
import {Image, StyleSheet, Text, View} from 'react-native';
import React from 'react';
import {StyledButton} from '../buttons/StyledButton.tsx';
import {SuccessfulRequestModalProps} from '../../types/ComponentTypes.ts';

export default function SuccessfulRequestModal({isToggled, handlePress, title, text}: SuccessfulRequestModalProps) {
  return (
    <AnimatedModal
      isToggled={isToggled}
      handlePress={handlePress}
      withBackdrop={true}>
      <View style={styles.container}>
        <View style={styles.imageContainer}>
          <Image
            source={require('../../assets/images/thumb-up.png')}
            style={styles.image}/>
        </View>
        <View style={styles.titleContainer}>
          <Text style={styles.title}>
            {title}
          </Text>
          {text != undefined ? (
            <>
              <Text style={styles.text}>
                {text}
              </Text>
            </>
          ) : (<></>)}
        </View>
        <View style={styles.buttonContainer}>
          <StyledButton
            content={'Ok'}
            top={20}
            bottom={5}
            isDisabled={false}
            pressed={async () => handlePress()}/>
        </View>
      </View>
    </AnimatedModal>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  imageContainer: {
    paddingTop: 30
  },
  image: {
    alignSelf: 'center',
    width: 40,
    height: 40,
    resizeMode: 'contain'
  },
  titleContainer: {
    alignItems: 'baseline'
  },
  title: {
    fontWeight: '500',
    fontSize: 20,
    color: 'black',
    paddingTop: 10,
    alignSelf: 'center',
  },
  text: {
    paddingTop: 10,
    fontWeight: '400',
    fontSize: 14,
    alignSelf: 'center',
    textAlign: 'center',
    color: '#6D7885'
  },
  buttonContainer: {
    paddingHorizontal: 15,
  }
})
