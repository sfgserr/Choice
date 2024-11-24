import * as React from 'react';
import {
  Text,
  View,
} from 'react-native';
import {StackProps} from '../App.tsx';
import {NativeStackScreenProps} from '@react-navigation/native-stack';

type Props = NativeStackScreenProps<StackProps, 'Categories'>

export default function LoginByEmailScreen() {
  return (
    <View>
      <Text
        style={{
          alignSelf: 'center',
          fontSize: 30,
          color: 'black'
        }}>
        LoginByEmail
      </Text>
    </View>
  )
}
