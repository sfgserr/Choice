import * as React from 'react';
import {
  SafeAreaView,
  ScrollView, StyleSheet, Text,
} from 'react-native';
import TextInputTitle from '../components/TextInputTitle.tsx';
import {TokenService} from '../services/auth/TokenService.ts';
import {AuthContext} from '../contexts/authorized/Context.tsx';
import {useDependency} from '../services/Hooks.ts';
import {GestureStyledButton} from '../components/buttons/GestureStyledButton.tsx';
import GestureBorderedTextInput from '../components/inputs/GestureBordererdTextInput.tsx';
import GesturePasswordBox from '../components/inputs/GesturePasswordBox.tsx';

export default function LoginByEmailScreen({refresh}: {refresh: () => void}) {
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
    <ScrollView style={styles.container}>
      <SafeAreaView>
        <TextInputTitle
          s={'E-mail'}
          top={0}
          bottom={5}/>
        <GestureBorderedTextInput
          value={email}
          onChanged={onEmailChanged}
          placeholder={'Введите E-mail'}
          isError={isError}
          isBig={false}/>
        <TextInputTitle
          s={'Пароль'}
          top={20}
          bottom={5}/>
        <GesturePasswordBox
          value={password}
          onChanged={onPasswordChanged}
          isError={isError}/>
        {isError && (
          <>
            <Text style={styles.errorText}>Логин или пароль неверны</Text>
          </>
        )}
        <GestureStyledButton
          content={'Войти'}
          top={20}
          bottom={0}
          isDisabled={isDisabled}
          pressed={async () => {
            refresh();

            let result = await tokenService.login(email, password);

            refresh();

            if (result != null)
              signIn(result[0], result[1]);
            else
              setIsError(true);
          }}/>
      </SafeAreaView>
    </ScrollView>
  )
}

const styles = StyleSheet.create({
  container: {
    paddingHorizontal: 15,
    paddingTop: 20,
  },
  errorText: {
    color: '#E64646',
    fontWeight: '400',
    fontSize: 13,
  },
});
