import * as React from 'react';
import {
  ScrollView
} from 'react-native';
import BorderedTextInput from '../components/BorderedTextInput.tsx';
import TextInputTitle from '../components/TextInputTitle.tsx';
import PasswordBox from '../components/PasswordBox.tsx';
import {StyledButton} from '../components/StyledButton.tsx';
import {UserService} from '../services/UserService.ts';
import {AuthContext} from '../App.tsx';

export default function LoginByEmailScreen({userService}: {userService: UserService}) {
  const { signIn } = React.useContext(AuthContext);

  const [email, setEmail] = React.useState('');
  const [password, setPassword] = React.useState('');
  const [isDisabled, setIsDisabled] = React.useState(true);

  const onEmailChanged = (text: string) => {
    setEmail(text);
    setIsDisabled(text == '' || password == '');
  };

  const onPasswordChanged = (text: string) => {
    setPassword(text);
    setIsDisabled(text == '' || email == '');
  };

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
        onChanged={onEmailChanged}
        placeholder={'Введите E-mail'}/>
      <TextInputTitle
        s={'Пароль'}
        top={20}
        bottom={5}/>
      <PasswordBox
        value={password}
        onChanged={onPasswordChanged}/>
      <StyledButton
        content={'Войти'}
        top={20}
        bottom={0}
        isDisabled={isDisabled}
        pressed={async () => {
          let result = await userService.login(email, password);

          signIn(result[0], result[1]);
        }}/>
    </ScrollView>
  )
}
