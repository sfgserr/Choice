import {Image, TouchableOpacity} from 'react-native';
import {
  View,
  Dimensions
} from 'react-native';

export default function ImageBox() {
  const d = Dimensions.get('screen');

  return (
    <View
      style={{
        width: d.width*0.274,
        height: d.width*0.274,
        borderRadius: 12,
        borderWidth: 1,
        borderStyle: 'dashed',
        backgroundColor: '#F9F9F9',
        borderColor: '#C8C8C8',
        justifyContent: 'center'
      }}>
      <TouchableOpacity>
        <Image
          source={require('../assets/images/imagebox.png')}
          style={{
            alignSelf: 'center',
            resizeMode: 'contain',
            width: d.width*0.091,
            height: d.width*0.091
          }}/>
      </TouchableOpacity>
    </View>
  )
}
