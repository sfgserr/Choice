import * as React from 'react';
import {View, Dimensions, Image, Text} from 'react-native';

export default function LoginScreen() {
  const {width, height} = Dimensions.get('screen');

  return (
    <View
      style={{
        backgroundColor: 'white',
        flex: 1,
        flexDirection: 'column',
      }}>
      <Image
        source={require('../assets/images/logo.png')}
        style={{
          width: height/7.38,
          height: height/7.38,
          alignSelf: 'center',
          marginTop: 60
      }}/>
      <Text
        style={{
          color: '#313131',
          fontSize: 20,
          fontWeight: '600',
          alignSelf: 'center',
          letterSpacing: 3,
          marginTop: 50
        }}>
        ВЫБОР
      </Text>
      <Text
        style={{
          color: '#9C9C9C',
          fontWeight: '400',
          fontSize: 16,
          alignSelf: 'center',
          textAlign: 'center',
          marginTop: 10
        }}>
        {'Приложение для выбора\nлучших условий'}
      </Text>
    </View>
  )
}
