import {Switch, Text, View} from 'react-native';

export default function SocialMediaItem({item}: any) {
  return (
    <View
      style={{
        flexDirection: 'row',
        justifyContent: 'space-between',
        paddingVertical: 4,
        borderBottomWidth: 1,
        borderColor: '#D7D8D9',
      }}>
      <View
        style={{
          flexDirection: 'row',
          alignSelf: 'center'
        }}>
        <Text
          style={{
            fontWeight: '400',
            fontSize: 15,
            color: 'black'
          }}>
          {item.title}
        </Text>
      </View>
      <Switch
        value={true}
        thumbColor={'white'}
        trackColor={{true: '#2688EB', false: '#001C3D14'}}/>
    </View>
  )
}
