import React from 'react';
import {KeyboardAvoidingView, ScrollView, StyleSheet, Text, View} from 'react-native';
import {RegisterClientScreenProps} from '../types/NavigationTypes.ts';
import TextInputTitle from '../components/TextInputTitle.tsx';
import TextButton from '../components/buttons/TextButton.tsx';
import SuccessfulRequestModal from '../components/modals/SuccessfulRequestModal.tsx';
import UnsuccessfulRequestModal from '../components/modals/UnsuccessfulRequestModal.tsx';
import LongRunningOperationIndicator from '../components/LongRunningOperationIndicator.tsx';
import {useDependency} from '../services/Hooks.ts';
import {ClientService} from '../services/domain/ClientService.ts';
import PhoneBox from '../components/inputs/PhoneBox.tsx';
import GestureBorderedTextInput from '../components/inputs/GestureBordererdTextInput.tsx';
import GesturePasswordBox from '../components/inputs/GesturePasswordBox.tsx';
import {GestureStyledButton} from '../components/buttons/GestureStyledButton.tsx';
import {SafeAreaView} from 'react-native-safe-area-context';

export default function RegisterClientScreen({route, navigation}: RegisterClientScreenProps) {
  const clientService = useDependency<ClientService>('ClientService');

  const [form, setForm] = React.useState({
    name: '',
    surname: '',
    email: '',
    phoneNumber: '',
    city: '',
    street: '',
    password: '',
    confirmPassword: '',
  });

  const [errorMessage, setErrorMessage] = React.useState('');

  const [toggled, setToggled] = React.useState(false);
  const [errorToggled, setErrorToggled] = React.useState(false);

  const [refreshing, setRefreshing] = React.useState(false);

  const isDisabled = React.useCallback(() => {
    return form.email == '' || form.password == '' ||
      form.confirmPassword == '' || form.city == '' ||
      form.street == '' || form.phoneNumber == '';
  }, [form]);

  const createClient = React.useCallback(async () => {
    setRefreshing(true);

    const response = await clientService.create(
      `${form.name} ${form.surname}`,
      form.password,
      form.email,
      form.phoneNumber,
      form.city,
      form.street);

    setRefreshing(false);

    if (response.result == 'successful') {
      setToggled(true);
    }
    else {
      setErrorMessage(response.error);
      setErrorToggled(true);
    }
  }, [form]);

  return (
    <SafeAreaView
      style={styles.container}>
      <KeyboardAvoidingView
        style={{flex: 1}}
        behavior={'height'}>
        <ScrollView
          style={styles.contentContainer}
          showsVerticalScrollIndicator={false}
          automaticallyAdjustKeyboardInsets>
          <Text style={styles.title}>Регистрация клиента</Text>
          <TextInputTitle
            s={'Имя'}
            top={20}
            bottom={5}/>
          <GestureBorderedTextInput
            value={form.name}
            onChanged={(name) => setForm(prev => ({...prev, name}))}
            placeholder={'Введите имя'}
            isError={false}
            isBig={false}/>
          <TextInputTitle
            s={'Фамилия'}
            top={20}
            bottom={5}/>
          <GestureBorderedTextInput
            value={form.surname}
            onChanged={(surname) => setForm(prev => ({...prev, surname}))}
            placeholder={'Введите фамилию'}
            isError={false}
            isBig={false}/>
          <TextInputTitle
            s={'E-mail'}
            top={20}
            bottom={5}/>
          <GestureBorderedTextInput
            value={form.email}
            onChanged={(email) => setForm(prev => ({...prev, email}))}
            placeholder={'Введите E-mail'}
            isError={false}
            isBig={false}/>
          <TextInputTitle
            s={'Номер телефона'}
            top={20}
            bottom={5}/>
          <PhoneBox
            value={form.phoneNumber}
            onChanged={(phoneNumber) => setForm(prev => ({...prev, phoneNumber}))}
            isError={false}
            isReadonly={false}/>
          <TextInputTitle
            s={'Город'}
            top={20}
            bottom={5}/>
          <GestureBorderedTextInput
            value={form.city}
            onChanged={(city) => setForm(prev => ({...prev, city}))}
            placeholder={'Введите название города'}
            isError={false}
            isBig={false}/>
          <TextInputTitle
            s={'Улица'}
            top={20}
            bottom={5}/>
          <GestureBorderedTextInput
            value={form.street}
            onChanged={(street) => setForm(prev => ({...prev, street}))}
            placeholder={'Введите название улицы'}
            isError={false}
            isBig={false}/>
          <TextInputTitle
            s={'Пароль'}
            top={20}
            bottom={5}/>
          <GesturePasswordBox
            value={form.password}
            onChanged={(password) => setForm(prev => ({...prev, password}))}
            isError={false}/>
          <TextInputTitle
            s={'Повторите пароль'}
            top={20}
            bottom={5}/>
          <GesturePasswordBox
            value={form.confirmPassword}
            onChanged={(confirmPassword) => setForm(prev => ({...prev, confirmPassword}))}
            isError={false}/>
          <GestureStyledButton
            content={'Создать аккаунт'}
            top={20}
            bottom={20}
            isDisabled={isDisabled()}
            pressed={createClient}/>
          <Text style={styles.loginText}>У меня есть аккаунт</Text>
          <View style={styles.loginButtonContainer}>
            <TextButton
              text={'Войти'}
              onPress={() => navigation.goBack()}/>
          </View>
          <SuccessfulRequestModal
            isToggled={toggled}
            handlePress={() => navigation.goBack()}
            title={'Аккаунт клиента создан'}
            text={'Теперь вы можете создавать заказы'}/>
          <UnsuccessfulRequestModal
            isToggled={errorToggled}
            handlePress={() => setErrorToggled(false)}
            errorMessage={errorMessage}/>
          <LongRunningOperationIndicator isRefreshing={refreshing}/>
        </ScrollView>
      </KeyboardAvoidingView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  contentContainer: {
    paddingHorizontal: 15,
  },
  title: {
    color: '#313131',
    fontWeight: '700',
    fontSize: 24,
    paddingTop: 30,
  },
  loginText: {
    fontSize: 16,
    fontWeight: '400',
    color: '#9C9C9C',
    alignSelf: 'center',
  },
  loginButtonContainer: {
    alignSelf: 'center',
    paddingTop: 20,
  },
});
