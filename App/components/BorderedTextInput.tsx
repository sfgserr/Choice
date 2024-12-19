import React from 'react';
import { TextInput, View } from 'react-native';
import Styles from '../constants/Styles.tsx';

type BorderedTextInputProps = {
  value: string,
  onChanged: (s: string) => void,
  placeholder: string,
  isError: boolean
}

export default function BorderedTextInput({ value, onChanged, placeholder, isError }: BorderedTextInputProps) {
  const [isFocused, setIsFocused] = React.useState(false);

  const borderColor = () => {
    return isFocused ? Styles.borderedTextInputFocused : Styles.borderedTextInputUnfocused;
  }

  return (
    <View
      style={[
        Styles.borderedTextInputView,
        !isError ? [borderColor(), Styles.borderedTextInputViewColor] : Styles.borderedTextInputError
      ]}>
      <TextInput
        value={value}
        onChangeText={onChanged}
        onEndEditing={() => setIsFocused(false)}
        placeholder={placeholder}
        onFocus={() => setIsFocused(true)}
        style={Styles.borderedTextInput}/>
    </View>
  )
}
