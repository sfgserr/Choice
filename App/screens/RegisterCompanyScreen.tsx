import React from 'react';
import {ScrollView, Text, View} from 'react-native';
import {RegisterCompanyScreenProps} from '../types/NavigationTypes.ts';
import TextInputTitle from '../components/TextInputTitle.tsx';
import BorderedTextInput from '../components/inputs/BorderedTextInput.tsx';
import PasswordBox from '../components/inputs/PasswordBox.tsx';
import {StyledButton} from '../components/buttons/StyledButton.tsx';
import TextButton from '../components/buttons/TextButton.tsx';
import {AuthContext} from '../App.tsx';
import SuccessfulRequestModal from '../components/modals/SuccessfulRequestModal.tsx';
import UnsuccessfulRequestModal from '../components/modals/UnsuccessfulRequestModal.tsx';
import LongRunningOperationIndicator from '../components/LongRunningOperationIndicator.tsx';

export default function RegisterCompanyScreen({route, navigation}: RegisterCompanyScreenProps) {
  const { changeState } = React.useContext(AuthContext);

  const [name, setName] = React.useState('');
  const [surname, setSurname] = React.useState('');
  const [email, setEmail] = React.useState('');
  const [phoneNumber, setPhoneNumber] = React.useState('');
  const [city, setCity] = React.useState('');
  const [street, setStreet] = React.useState('');
  const [password, setPassword] = React.useState('');
  const [confirmPassword, setConfirmPassword] = React.useState('');

  const [errorMessage, setErrorMessage] = React.useState('');

  const [toggled, setToggled] = React.useState(false);
  const [errorToggled, setErrorToggled] = React.useState(false);

  const [refreshing, setRefreshing] = React.useState(false);

  const createCompany = async () => {
    setRefreshing(true);

    const response = await route.params.companyService.createCompany(
      name,
      password,
      email,
      phoneNumber,
      city,
      street,
      changeState);

    if (response.result == 'successful') {
      setToggled(true);
    }

    setRefreshing(false);
  }

  return (
    <ScrollView
      style={{
        flex: 1,
        backgroundColor: 'white',
      }}
      showsVerticalScrollIndicator={false}>
      <View style={{paddingHorizontal: 15}}>
        <Text
          style={{
            color: '#313131',
            fontWeight: '700',
            fontSize: 24,
            paddingTop: 30,
          }}>
          Регистрация компании
        </Text>
        <TextInputTitle
          s={'Название'}
          top={20}
          bottom={5}/>
        <BorderedTextInput
          value={name}
          onChanged={setName}
          placeholder={'Введите имя'}
          isError={false}
          isBig={false}/>
        <TextInputTitle
          s={'E-mail'}
          top={20}
          bottom={5}/>
        <BorderedTextInput
          value={email}
          onChanged={setEmail}
          placeholder={'Введите E-mail'}
          isError={false}
          isBig={false}/>
        <TextInputTitle
          s={'Номер телефона'}
          top={20}
          bottom={5}/>
        <BorderedTextInput
          value={phoneNumber}
          onChanged={setPhoneNumber}
          placeholder={'Введите номер телефона'}
          isError={false}
          isBig={false}/>
        <TextInputTitle
          s={'Город'}
          top={20}
          bottom={5}/>
        <BorderedTextInput
          value={city}
          onChanged={setCity}
          placeholder={'Введите название города'}
          isError={false}
          isBig={false}/>
        <TextInputTitle
          s={'Улица'}
          top={20}
          bottom={5}/>
        <BorderedTextInput
          value={street}
          onChanged={setStreet}
          placeholder={'Введите название улицы'}
          isError={false}
          isBig={false}/>
        <TextInputTitle
          s={'Пароль'}
          top={20}
          bottom={5}/>
        <PasswordBox
          value={password}
          onChanged={setPassword}
          isError={false}/>
        <TextInputTitle
          s={'Повторите пароль'}
          top={20}
          bottom={5}/>
        <PasswordBox
          value={confirmPassword}
          onChanged={setConfirmPassword}
          isError={false}/>
        <StyledButton
          content={'Создать аккаунт'}
          top={20}
          bottom={20}
          isDisabled={
            name == '' ||
            surname == '' ||
            email == '' ||
            phoneNumber == '' ||
            city == '' ||
            street == '' ||
            password == '' ||
            confirmPassword == '' ||
            password != confirmPassword
          }
          pressed={createCompany}/>
        <Text
          style={{
            fontSize: 16,
            fontWeight: '400',
            color: '#9C9C9C',
            alignSelf: 'center'
          }}>
          У меня есть аккаунт
        </Text>
        <View
          style={{
            alignSelf: 'center',
            paddingTop: 20,
          }}>
          <TextButton
            text={'Войти'}
            onPress={() => navigation.goBack()}/>
        </View>
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
  )
}
