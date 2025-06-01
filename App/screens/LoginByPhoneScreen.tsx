import {KeyboardAvoidingView, View} from 'react-native';
import {useCallback, useState} from 'react';
import TextInputTitle from '../components/TextInputTitle.tsx';
import BorderedTextInput from '../components/inputs/BorderedTextInput.tsx';
import {StyledButton} from '../components/buttons/StyledButton.tsx';
import {AuthService} from '../services/auth/AuthService.ts';
import {useDependency} from '../services/Hooks.ts';
import PhoneBox from "../components/inputs/PhoneBox.tsx";

export default function LoginByPhoneScreen() {
  const authService = useDependency<AuthService>('AuthService');

  const [isCodeSent, setIsCodeSent] = useState<boolean>(false);
  const [phone, setPhone] = useState<string>('');
  const [code, setCode] = useState<string>();

  const sendCode = useCallback(async () => {
    const isSent = await authService.sendCode(phone);

    setIsCodeSent(isSent);
  }, [phone]);

  return (
    <KeyboardAvoidingView
      style={{flex: 1}}
      behavior={'height'}>
      <View style={{paddingHorizontal: 15}}>
        {!isCodeSent ? (
          <>
            <TextInputTitle s={'Номер телефона'} top={20} bottom={5}/>
            <PhoneBox
              value={phone}
              onChanged={setPhone}
              isError={false}
              isReadonly={false}/>
            <StyledButton
              content={'Отправить код'}
              top={20}
              bottom={0}
              isDisabled={phone == ''}
              pressed={sendCode}
              type={'default'}/>
          </>
        ) : (
          <>
            <TextInputTitle s={'Код'} top={20} bottom={5}/>
            <BorderedTextInput
              value={code}
              onChanged={setCode}
              placeholder={'Введите код из смс'}
              isError={false}
              isBig={false}
              keyboard={'phone-pad'}
              isReadonly={false}/>
            <StyledButton
              content={'Отправить код'}
              top={20}
              bottom={0}
              isDisabled={phone == ''}
              pressed={() => {}}
              type={'default'}/>
          </>)}
      </View>
    </KeyboardAvoidingView>
  );
}
