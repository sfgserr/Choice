import React from 'react';
import {Text, View} from 'react-native';


export default function TextInputTitle({s, top, bottom}: {s: string, top: number, bottom: number}) {
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
