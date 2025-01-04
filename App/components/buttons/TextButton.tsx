import React from 'react';
import {Text, TouchableOpacity} from 'react-native';
import {TextButtonProps} from '../../types/ComponentTypes.ts';

export default function TextButton({text, onPress}: TextButtonProps) {
  return (
    <TouchableOpacity
      style={{
        backgroundColor: 'transparent',
        justifyContent: 'center'
      }}
      onPress={onPress}>
      <Text
        style={{
          color: '#2D81E0',
          fontSize: 16,
          fontWeight: '400'
        }}>
        {text}
      </Text>
    </TouchableOpacity>
  )
}
