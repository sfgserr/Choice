import * as React from 'react';
import {
  ScrollView
} from 'react-native';
import BorderedTextInput from '../components/BorderedTextInput.tsx';
import TextInputTitle from '../components/TextInputTitle.tsx';
import PasswordBox from '../components/PasswordBox.tsx';

export default function LoginByEmailScreen() {
  const [email, setEmail] = React.useState('');
  const [password, setPassword] = React.useState('');

  return (
    <ScrollView
      style={{
        paddingHorizontal: 15,
        paddingTop: 20
      }}
      showsVerticalScrollIndicator={false}>
      <TextInputTitle
        s={'E-mail'}
        top={0}
        bottom={5}/>
      <BorderedTextInput
        value={email}
        onChanged={setEmail}
        placeholder={'Введите E-mail'}/>
      <TextInputTitle
        s={'Пароль'}
        top={20}
        bottom={5}/>
      <PasswordBox
        value={password}
        onChanged={setPassword}/>
    </ScrollView>
  )
}
