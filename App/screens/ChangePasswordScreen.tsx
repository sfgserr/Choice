import {StyleSheet, Text, View, StatusBar, Dimensions, KeyboardAvoidingView} from 'react-native';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {ChangePasswordScreenProps} from '../types/NavigationTypes.ts';
import TextInputTitle from '../components/TextInputTitle.tsx';
import {useCallback, useState} from 'react';
import BorderedTextInput from '../components/inputs/BorderedTextInput.tsx';
import {StyledButton} from '../components/buttons/StyledButton.tsx';
import {useDependency} from '../services/Hooks.ts';
import {IdentityService} from '../services/domain/IdentityService.ts';
import SuccessfulRequestModal from '../components/modals/SuccessfulRequestModal.tsx';
import { SafeAreaView } from 'react-native-safe-area-context';

export default function ChangePasswordScreen({navigation}: ChangePasswordScreenProps) {
  const identityService = useDependency<IdentityService>('IdentityService');

  const [oldPassword, setOldPassword] = useState<string>('');
  const [newPassword, setNewPassword] = useState<string>('');
  const [confirmPassword, setConfirmPassword] = useState<string>('');

  const [isToggled, setIsToggled] = useState<boolean>(false);

  const disable = () => oldPassword == '' || newPassword == '' || confirmPassword != newPassword;

  const changePassword = async () => {
    const response = await identityService.changePassword(oldPassword, newPassword);

    if (response.result == 'successful') {
      setIsToggled(true);
    }
  };

  const handlePress = useCallback(() => {
    setIsToggled(false);
    navigation.goBack();
  }, []);

  return (
    <SafeAreaView style={styles.container}>
      <KeyboardAvoidingView
        style={{flex: 1}}
        behavior={'height'}>
        <View style={{flex: 1}}>
          <View style={styles.navigateBackButtonContainer}>
            <NavigateBackButton
              navigation={navigation}
              onGoBack={() => {}}/>
          </View>
          <Text style={styles.title}>Изменить пароль</Text>
          <View style={styles.inputContainer}>
            <TextInputTitle
              s={'Старый пароль'}
              top={30}
              bottom={5}/>
            <BorderedTextInput
              value={oldPassword}
              onChanged={setOldPassword}
              placeholder={'Введите текущий пароль'}
              isError={false}
              isBig={false}
              keyboard={'default'}/>
            <TextInputTitle
              s={'Новый пароль'}
              top={30}
              bottom={5}/>
            <BorderedTextInput
              value={newPassword}
              onChanged={setNewPassword}
              placeholder={'Введите новый пароль'}
              isError={false}
              isBig={false}
              keyboard={'default'}/>
            <TextInputTitle
              s={'Повторите новый пароль'}
              top={30}
              bottom={5}/>
            <BorderedTextInput
              value={confirmPassword}
              onChanged={setConfirmPassword}
              placeholder={'Введите новый пароль'}
              isError={false}
              isBig={false}
              keyboard={'default'}/>
          </View>
          <View style={styles.buttonContainer}>
            <StyledButton
              content={'Сохранить новый пароль'}
              top={0}
              bottom={0}
              isDisabled={disable()}
              pressed={changePassword}
              type={'default'}/>
          </View>
          <SuccessfulRequestModal
            isToggled={isToggled}
            handlePress={handlePress}
            title={'Пароль изменен'}
            text={''}/>
        </View>
      </KeyboardAvoidingView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  navigateBackButtonContainer: {
    flexDirection: 'row',
    justifyContent: 'flex-start',
    paddingHorizontal: 15,
    paddingTop: 20,
  },
  title: {
    position: 'absolute',
    fontWeight: '700',
    fontSize: 21,
    color: 'black',
    alignSelf: 'center',
    top: 15,
  },
  inputContainer: {
    paddingHorizontal: 15,
  },
  buttonContainer: {
    width: '90%',
    alignSelf: 'center',
    flex: 1,
    justifyContent: 'flex-end'
  },
});
