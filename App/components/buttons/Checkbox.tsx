import {CheckboxProps} from '../../types/ComponentTypes.ts';
import {
  View,
  Dimensions,
  Image, TouchableOpacity,
} from 'react-native';

export default function Checkbox({checked, pressed}: CheckboxProps) {
  const d = Dimensions.get('screen');

  return (
    <TouchableOpacity
      style={{
        width: d.height*0.023,
        height: d.height*0.023,
        borderWidth: checked ? 0 : 2,
        borderColor: '#B8C1CC',
        borderRadius: 3,
        alignSelf: 'center'
      }}
      onPress={pressed}>
      {checked ? (
        <>
          <Image
            source={require('../../assets/images/checkbox.png')}
            style={{
              width: d.height*0.023,
              height: d.height*0.023
            }}/>
        </>
      ) : (<></>)}
    </TouchableOpacity>
  )
}
