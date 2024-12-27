import {Image, TouchableOpacity} from 'react-native';
import {
  View,
  Dimensions
} from 'react-native';
import {ImageBoxProps} from '../types/ComponentTypes.ts';

export default function ImageBox({uri, onPress, onRemovePress}: ImageBoxProps) {
  const d = Dimensions.get('screen');

  return (
    <View
      style={{
        justifyContent: 'center',
        alignItems: 'center',
      }}>
      <View
        style={{
          width: d.width*0.274,
          height: d.width*0.274,
          borderRadius: 12,
          borderWidth: 1,
          borderStyle: 'dashed',
          position: 'relative',
          backgroundColor: '#F9F9F9',
          borderColor: uri != '' ? '#4D4D4D' : '#C8C8C8',
          justifyContent: 'center',
        }}>
        {uri == '' ? (
          <>
            <TouchableOpacity
              onPress={onPress}>
              <Image
                source={require('../assets/images/imagebox.png')}
                style={{
                  alignSelf: 'center',
                  resizeMode: 'contain',
                  width: d.width*0.091,
                  height: d.width*0.091,
                }}/>
            </TouchableOpacity>
          </>
        ) : (
          <>
            <Image
              source={{uri}}
              style={{
                width: '100%',
                height: '100%',
                borderRadius: 12
              }}/>

            <TouchableOpacity
              style={{
                width: d.width*0.06,
                height: d.width*0.06,
                borderWidth: 1,
                borderColor: '#E7E7E7',
                backgroundColor: 'white',
                position: 'absolute',
                top: -6,
                right: -6,
                borderRadius: d.width*0.03,
                justifyContent: 'center'
              }}
              onPress={onRemovePress}>
              <Image
                style={{
                  width: d.width*0.03,
                  height: d.width*0.03,
                  resizeMode: 'contain',
                  alignSelf: 'center'
                }}
                source={require('../assets/images/cross.png')}/>
            </TouchableOpacity>
          </>
        )}
      </View>
    </View>
  )
}
