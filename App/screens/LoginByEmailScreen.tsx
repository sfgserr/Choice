import * as React from 'react';
import {
  ScrollView, StyleSheet, Text,
} from 'react-native';
import BorderedTextInput from '../components/inputs/BorderedTextInput.tsx';
import TextInputTitle from '../components/TextInputTitle.tsx';
import PasswordBox from '../components/inputs/PasswordBox.tsx';
import {StyledButton} from '../components/buttons/StyledButton.tsx';
import {TokenService} from '../services/auth/TokenService.ts';
import {AuthContext} from '../AuthorizedContextProvider.tsx';
import {useDependency} from '../stores/DependencyInjection.ts';

export default function LoginByEmailScreen() {
  const { signIn } = React.useContext(AuthContext);

  const tokenService = useDependency<TokenService>('TokenService');

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
      style={styles.container}
      showsVerticalScrollIndicator={false}>
      <TextInputTitle
        s={'E-mail'}
        top={0}
        bottom={5}/>
      <BorderedTextInput
        value={email}
        onChanged={onEmailChanged}
        placeholder={'Введите E-mail'}
        isError={isError}
        isBig={false}/>
      <TextInputTitle
        s={'Пароль'}
        top={20}
        bottom={5}/>
      <PasswordBox
        value={password}
        onChanged={onPasswordChanged}
        isError={isError}/>
      {isError && (
        <>
          <Text style={styles.errorText}>Логин или пароль неверны</Text>
        </>
      )}
      <StyledButton
        content={'Войти'}
        top={20}
        bottom={0}
        isDisabled={isDisabled}
        pressed={async () => {
          let result = await tokenService.login(email, password);

          if (result != null)
            signIn(result[0], result[1]);
          else
            setIsError(true);
        }}/>
    </ScrollView>
  )
}

const styles = StyleSheet.create({
  container: {
    paddingHorizontal: 15,
    paddingTop: 20
  },
  errorText: {
    color: '#E64646',
    fontWeight: '400',
    fontSize: 13
  },
});
