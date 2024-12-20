import * as React from 'react';
import {View, Dimensions, Image, Text} from 'react-native';
import TextButton from '../components/TextButton.tsx';
import LoginByEmailScreen from './LoginByEmailScreen.tsx';
import TabBar from '../components/TabBar.tsx';
import {NativeStackScreenProps} from '@react-navigation/native-stack';
import {StackProps} from '../types/NavigationTypes.ts';

type Props = NativeStackScreenProps<StackProps, 'Login'>;

export default function LoginScreen({route, navigation}: Props) {
  const {width, height} = Dimensions.get('screen');

  const tabs = [
    {element: <LoginByEmailScreen userService={route.params.userService}/>, title: 'E-mail'},
    {element: <View><Text>Phone</Text></View>, title: 'Телефон'}
  ]

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
      <View
        style={{
          flexDirection: 'row',
          justifyContent: 'space-around',
          paddingTop: 30
        }}>
        <Text
          style={{
            color: '#313131',
            fontWeight: '700',
            fontSize: 24
          }}>
          Авторизация
        </Text>
        <TextButton text={'Создать аккаунт'}/>
      </View>
      <TabBar tabs={tabs}/>
    </View>
  )
}
