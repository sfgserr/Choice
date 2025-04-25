import {Image, StyleSheet} from 'react-native';
import React from 'react';
import {CloseButtonProps} from '../../types/ComponentTypes.ts';
import {Pressable} from 'react-native-gesture-handler';

export default function CloseButton({close}: CloseButtonProps) {
  return (
    <Pressable
      style={styles.closeButton}
      onPress={() => close()}>
      <Image
        style={styles.image}
        source={require('../../assets/images/cross.png')}
      />
    </Pressable>
  )
}

const styles = StyleSheet.create({
  closeButton: {
    alignSelf: 'center',
    borderRadius: 360,
    backgroundColor: '#f0f1f3',
    overflow: 'hidden',
    width: 25,
    height: 25,
    justifyContent: 'center',
  },
  image: {
    width: '50%',
    height: '50%',
    resizeMode: 'contain',
    alignSelf: 'center',
  },
});
