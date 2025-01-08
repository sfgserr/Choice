import React from 'react';
import {Dimensions, Text, View} from 'react-native';
import {FillDataScreenProps} from '../types/NavigationTypes.ts';
import ContactDetailsScreen from './ContactDetailsScreen.tsx';
import SocialMediasScreen from './SocialMediasScreen.tsx';
import AboutScreen from './AboutScreen.tsx';

export default function FillDataScreen({route, navigation}: FillDataScreenProps) {
  const d = Dimensions.get('screen');

  const screens = [
    <ContactDetailsScreen next={() => setCurrentIndex(prev => ++prev)}/>,
    <SocialMediasScreen/>,
    <AboutScreen/>
  ];

  const [currentIndex, setCurrentIndex] = React.useState(0);

  return (
    <View
      style={{
        flex: 1,
        backgroundColor: 'white',
        paddingTop: 20,
        paddingHorizontal: 15
      }}>
      <Text
        style={{
          fontSize: 21,
          fontWeight: '600',
          alignSelf: 'center',
          color: 'black'
        }}>
        Карточка компании
      </Text>
      <View
        style={{
          justifyContent: 'space-evenly',
          flexDirection: 'row',
          paddingTop: 20,
          paddingBottom: 20
        }}>
        {screens.map((i, n) => (
          <View
            style={{
              width: d.width/screens.length*0.85,
              height: 4,
              borderRadius: 5,
              backgroundColor: currentIndex >= n ? '#2688EB' : '#DFDFDF'
            }}
            key={n}/>
        ))}
      </View>
      <View
        style={{
          backgroundColor: '#D7D8D9',
          width: 'auto',
          height: .25
        }}/>
      {screens[currentIndex]}
    </View>
  )
}
