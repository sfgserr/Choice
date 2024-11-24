import {TextInput, TouchableOpacity, View} from 'react-native';
import React from 'react';
import Styles from '../constants/Styles.tsx';
import { Icon } from '@rneui/themed';

type PasswordBoxProps = {
  value: string,
  onChanged: (s: string) => void,
}

export default function PasswordBox({value, onChanged}: PasswordBoxProps) {
  const [isFocused, setIsFocused] = React.useState(false);
  const [isPassword, setIsPassword] = React.useState(true);

  return (
    <View
      style={[
        Styles.borderedTextInputView,
        {borderColor: isFocused ? '#3F8AE0' : '#d5d6d8'},
      ]}>
      <TextInput
        value={value}
        onChangeText={onChanged}
        onEndEditing={() => setIsFocused(false)}
        placeholder={'Введите пароль'}
        secureTextEntry={isPassword}
        onFocus={() => setIsFocused(true)}
        style={Styles.borderedTextInput}
      />

      <TouchableOpacity
        style={{
          paddingRight: 10,
          justifyContent: 'center',
        }}
        onPress={() => setIsPassword(p => !p)}>
        <Icon
          name={isPassword ? 'eye' : 'eye-off'}
          type="material-community"
          color="#99A2AD" />
      </TouchableOpacity>
    </View>
  );
}
