import React from 'react';
import {Text, View} from 'react-native';
import {TextInputTitleProps} from '../types/ComponentTypes.ts';

export default function TextInputTitle({s, top, bottom}: TextInputTitleProps) {
  return (
    <View
      style={{
        paddingTop: top,
        paddingBottom: bottom,
      }}>
      <Text
        style={{
          color: '#6D7885',
          fontSize: 14,
          fontWeight: '400',
        }}>
        {s}
      </Text>
    </View>
  );
}
