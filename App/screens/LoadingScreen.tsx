import * as React from 'react';
import {
  View,
  Text, ActivityIndicator,
} from 'react-native';

export default function LoginScreen() {
  return (
    <View
      style={{
        flex: 1,
        justifyContent: 'center',
        backgroundColor: 'white',
      }}>
      <ActivityIndicator size='large' color='#2D81E0'/>
    </View>
  )
}
