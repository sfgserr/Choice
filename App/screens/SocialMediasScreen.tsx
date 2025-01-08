import {FlatList, Text, View} from 'react-native';
import React from 'react';
import SocialMediaItem from '../components/listItems/SocialMediaItem.tsx';

export default function SocialMediasScreen() {
  const [photoUris, setPhotoUris] = React.useState<string[]>(['', '', '', '']);

  const socialMedias = [
    {
      title: 'Instagram',
      index: 0
    },
    {
      title: 'Facebook',
      index: 1
    },
    {
      title: 'ВК',
      index: 2
    },
    {
      title: 'Telegram',
      index: 3
    },
  ];

  return (
    <View
      style={{
        flex: 1,
        backgroundColor: 'white',
        paddingTop: 10
      }}>
      <Text
        style={{
          fontWeight: '700',
          fontSize: 17,
          color: 'black',
        }}>
        Социальные сети
      </Text>
      <FlatList
        data={socialMedias}
        style={{paddingTop: 20}}
        renderItem={item => (
          <SocialMediaItem item={item.item}/>
        )}/>
    </View>
  )
}
