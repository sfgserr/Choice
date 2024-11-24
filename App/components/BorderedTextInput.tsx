import React from 'react';
import { TextInput, View } from 'react-native';
import Styles from '../constants/Styles.tsx';

type BorderedTextInputProps = {
  value: string,
  onChanged: (s: string) => void,
  placeholder: string
}

export default function BorderedTextInput({ value, onChanged, placeholder }: BorderedTextInputProps) {
  const [isFocused, setIsFocused] = React.useState(false);

  return (
    <View
      style={[Styles.borderedTextInputView, { borderColor: isFocused ? '#3F8AE0' : '#d5d6d8' }]}>
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
