import * as React from 'react';
import {
  ScrollView, Text,
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
  const [isError, setIsError] = React.useState(false);

  const onEmailChanged = (text: string) => {
    setIsError(false);
    setEmail(text);
    setIsDisabled(text == '' || password == '');
  };

  const onPasswordChanged = (text: string) => {
    setIsError(false);
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
        placeholder={'Введите E-mail'}
        isError={isError}/>
      <TextInputTitle
        s={'Пароль'}
        top={20}
        bottom={5}/>
      <PasswordBox
        value={password}
        onChanged={onPasswordChanged}
        isError={isError}/>
      {isError ? (
        <>
          <Text
            style={{
              color: '#E64646',
              fontWeight: '400',
              fontSize: 13
            }}>
            Логин или пароль неверны
          </Text>
        </>
      ) : (<></>)}
      <StyledButton
        content={'Войти'}
        top={20}
        bottom={0}
        isDisabled={isDisabled}
        pressed={async () => {
          let result = await userService.login(email, password);

          if (result != null)
            signIn(result[0], result[1]);
          else
            setIsError(true);
        }}/>
    </ScrollView>
  )
}
