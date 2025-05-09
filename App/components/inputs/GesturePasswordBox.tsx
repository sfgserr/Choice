import {View} from 'react-native';
import React from 'react';
import Styles from '../../constants/Styles.tsx';
import { Icon } from '@rneui/themed';
import {PasswordBoxProps} from '../../types/ComponentTypes.ts';
import {Pressable, TextInput} from 'react-native-gesture-handler';

export default function GesturePasswordBox({value, onChanged, isError}: PasswordBoxProps) {
  const [isFocused, setIsFocused] = React.useState(false);
  const [isPassword, setIsPassword] = React.useState(true);

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
      <TextInput
        value={value}
        onChangeText={onChanged}
        onEndEditing={() => setIsFocused(false)}
        placeholder={'Введите пароль'}
        secureTextEntry={isPassword}
        onFocus={() => setIsFocused(true)}
        style={Styles.borderedTextInput}
      />

      <Pressable
        style={{
          paddingRight: 10,
          justifyContent: 'center',
        }}
        onPress={() => setIsPassword(p => !p)}>
        <Icon
          name={isPassword ? 'eye' : 'eye-off'}
          type="material-community"
          color="#99A2AD" />
      </Pressable>
    </View>
  );
}
