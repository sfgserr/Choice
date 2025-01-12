import {Image, Switch, Text, View} from 'react-native';

export default function SocialMediaItem({item}: any) {
  return (
    <View
      style={{
        flexDirection: 'row',
        justifyContent: 'space-between',
        paddingVertical: 10,
        borderBottomWidth: 1,
        borderColor: '#D7D8D9',
      }}>
      <View
        style={{
          flexDirection: 'row',
          alignSelf: 'center'
        }}>
        <Image
          source={item.icon}
          style={{
            width: 25,
            height: 25,
            resizeMode: 'contain',
            alignSelf: 'center'
          }}/>
        <Text
          style={{
            fontWeight: '400',
            fontSize: 15,
            color: 'black',
            paddingLeft: 10,
            alignSelf: 'center'
          }}>
          {item.title}
        </Text>
      </View>
      <Switch
        value={item.uri != ''}
        thumbColor={'white'}
        trackColor={{true: '#2688EB', false: '#001C3D14'}}
        onValueChange={item.onPress}/>
    </View>
  )
}
