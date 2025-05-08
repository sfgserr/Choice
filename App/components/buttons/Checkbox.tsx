import {CheckboxProps} from '../../types/ComponentTypes.ts';
import {
  Dimensions,
  Image,
} from 'react-native';
import {Pressable} from 'react-native-gesture-handler';

export default function Checkbox({checked, pressed, readonly = false}: CheckboxProps) {
  const d = Dimensions.get('screen');

  return (
    <>
      {(!readonly || checked) && (
        <Pressable
          style={{
            width: d.height * 0.023,
            height: d.height * 0.023,
            borderWidth: checked ? 0 : 2,
            borderColor: '#B8C1CC',
            borderRadius: 3,
            alignSelf: 'center',
            opacity: readonly ? 0.5 : 1,
          }}
          onPress={pressed}
          disabled={readonly}>
          {checked && (
            <Image
              source={require('../../assets/images/checkbox.png')}
              style={{
                width: d.height * 0.023,
                height: d.height * 0.023,
              }}/>
          )}
        </Pressable>
      )}
    </>
  )
}
