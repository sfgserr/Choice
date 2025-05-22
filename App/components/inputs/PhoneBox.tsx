import Styles from '../../constants/Styles.tsx';
import {TextInput} from 'react-native-gesture-handler';
import {Text, View} from 'react-native';
import React from 'react';

export default function PhoneBox({value, onChanged, isError, isReadonly}: {
  value: string,
  onChanged: (val: string) => void,
  isError: boolean,
  isReadonly: boolean}) {
  const [isFocused, setIsFocused] = React.useState(false);
  const borderColor = () => {
    return isFocused ? Styles.borderedTextInputFocused : Styles.borderedTextInputUnfocused;
  }

  return (
    <View
      style={[
        Styles.borderedTextInputView,
        Styles.borderedTextInputHeight,
        !isError ? [borderColor(), Styles.borderedTextInputViewColor] : Styles.borderedTextInputError
      ]}>
      <Text
        style={{
          fontSize: 16,
          color: 'black',
          fontWeight: 600,
          alignSelf: 'center',
          paddingRight: 10,
        }}>
        +7
      </Text>
      <View
        style={{
          backgroundColor: '#818C99',
          height: '80%',
          width: 1,
          alignSelf: 'center',
          opacity: 0.2,
        }}/>
      <TextInput
        value={value}
        onChangeText={onChanged}
        onEndEditing={() => setIsFocused(false)}
        placeholder={'(000) 000-00-00'}
        onFocus={() => setIsFocused(true)}
        style={[Styles.borderedTextInput, {paddingLeft: 10}]}
        maxLength={10}
        readOnly={isReadonly}
      />
    </View>
  );
}
