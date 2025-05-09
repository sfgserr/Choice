import React from 'react';
import {Text} from 'react-native';
import {TextButtonProps} from '../../types/ComponentTypes.ts';
import {Pressable} from 'react-native-gesture-handler';

export default function TextButton({text, onPress}: TextButtonProps) {
  return (
    <Pressable
      style={{
        alignItems: 'baseline',
      }}
      onPress={onPress}>
      <Text
        style={{
          color: '#2D81E0',
          fontSize: 16,
          fontWeight: '400',
          alignSelf: 'center',
        }}>
        {text}
      </Text>
    </Pressable>
  )
}
