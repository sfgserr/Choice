import React from 'react';
import {Text, TouchableOpacity} from 'react-native';
import {TextButtonProps} from '../../types/ComponentTypes.ts';

export default function TextButton({text, onPress}: TextButtonProps) {
  return (
    <TouchableOpacity
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
    </TouchableOpacity>
  )
}
