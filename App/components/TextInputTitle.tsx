import React from 'react';
import {Text, View} from 'react-native';
import {TextInputTitleProps} from '../types/ComponentTypes.ts';
import Styles from '../constants/Styles.tsx';

export default function TextInputTitle({s, top, bottom}: TextInputTitleProps) {
  return (
    <View
      style={{
        paddingTop: top,
        paddingBottom: bottom,
      }}>
      <Text
        style={Styles.title}>
        {s}
      </Text>
    </View>
  );
}
