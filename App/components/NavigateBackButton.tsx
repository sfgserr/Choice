import {Image, TouchableOpacity} from 'react-native';

export default function NavigateBackButton({navigation}) {
  return (
    <TouchableOpacity
      onPress={() => {
        navigation.goBack();
      }}>
      <Image
        style={{
          resizeMode: 'contain',
          alignSelf: 'center',
          width: 20,
          height: 20,
          paddingLeft: 30
        }}
        source={require('../assets/images/chevron-left.png')}/>
    </TouchableOpacity>
  )
}
