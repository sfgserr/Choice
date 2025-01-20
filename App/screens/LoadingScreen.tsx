import * as React from 'react';
import {View, ActivityIndicator, StyleSheet} from 'react-native';

export default function LoginScreen() {
  return (
    <View style={styles.container}>
      <ActivityIndicator size='large' color='#2D81E0'/>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    backgroundColor: 'white',
  },
});
