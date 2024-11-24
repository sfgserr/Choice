import React from 'react';
import {Text, TouchableOpacity} from 'react-native';

export default function TextButton() {
  return (
    <TouchableOpacity
      style={{
        backgroundColor: 'transparent',
        justifyContent: 'center'
      }}>
      <Text
        style={{
          color: '#2D81E0',
          fontSize: 16,
          fontWeight: '400'
        }}>
        Создать аккаунт
      </Text>
    </TouchableOpacity>
  )
}
