import {Image, Text, TouchableOpacity, View} from 'react-native';
import React, {useContext} from 'react';
import {AuthContext} from '../contexts/authorized/Context.tsx';

export default function BannedScreen() {
  const { signOut } = useContext(AuthContext);

  return (
    <View
      style={{
        flex: 1,
        justifyContent: 'center',
        backgroundColor: 'white',
      }}>
      <View
        style={{
          justifyContent: 'center',
          gap: 20
        }}>
        <Text
          style={{
            color: 'black',
            alignSelf: 'center',
            fontSize: 21,
            fontWeight: 700
          }}>
          Вы заблокированы
        </Text>
        <TouchableOpacity
          style={{alignSelf: 'center'}}
          onPress={signOut}>
          <Image
            style={{
              width: 30,
              height: 30,
              resizeMode: 'contain',
            }}
            source={require('../assets/images/signout.png')}/>
        </TouchableOpacity>
      </View>
    </View>
  )
}
