import {TextInput, TouchableOpacity, View} from 'react-native';
import React from 'react';
import Styles from '../constants/Styles.tsx';
import { Icon } from '@rneui/themed';
import {PasswordBoxProps} from '../types/ComponentTypes.ts';

export default function PasswordBox({value, onChanged, isError}: PasswordBoxProps) {
  const [isFocused, setIsFocused] = React.useState(false);
  const [isPassword, setIsPassword] = React.useState(true);

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
