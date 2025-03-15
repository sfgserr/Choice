import * as React from 'react';
import {ActivityIndicator, Image, ScrollView, StyleSheet, Text, TextInput, View} from 'react-native';
import {AuthContext} from '../../../contexts/authorized/Context.tsx';
import {StyledButton} from '../../../components/buttons/StyledButton.tsx';
import {AccountScreenProps} from '../../../types/NavigationTypes.ts';
import {useDependency} from '../../../services/Hooks.ts';
import {UserService} from '../../../services/domain/UserService.ts';
import TextButton from '../../../components/buttons/TextButton.tsx';
import ChangeIconUriModal from '../../../components/modals/ChangeIconUriModal.tsx';
import TextInputTitle from '../../../components/TextInputTitle.tsx';
import BorderedTextInput from '../../../components/inputs/BorderedTextInput.tsx';
import {SetStateAction} from 'react';
import {useIsFocused} from '@react-navigation/native';
import SuccessfulRequestModal from '../../../components/modals/SuccessfulRequestModal.tsx';
import {ClientService} from '../../../services/domain/ClientService.ts';

type Form = {
  id: string
  name: string
  email: string
  phoneNumber: string
  city: string
  street: string
  iconUri: string
}

export default function CompanyAccountScreen({route, navigation}: AccountScreenProps) {
  const userService = useDependency<UserService>('UserService');
  const clientService = useDependency<ClientService>('ClientService');

  const { signOut } = React.useContext(AuthContext);

  const [form, setForm] = React.useState<Form | null>(null);
  const [isChanged, setIsChanged] = React.useState<boolean>(false);

  const [isChangeIconUriModalToggled, setIsChangeIconUriModalToggled] = React.useState(false);
  const [isSuccessfulRequestModalToggled, setIsSuccessfulRequestModalToggled] = React.useState(false);

  const toggle = React.useCallback(() => setIsChangeIconUriModalToggled(prev => !prev), []);

  const setIcon = React.useCallback((objectName: string) => {
    setForm(prev => ({...prev, iconUri: objectName}));
  }, []);

  const set = React.useCallback((func: SetStateAction<Form | null>) => {
    setForm(func);

    setIsChanged(true);
  }, []);

  const saveChanges = React.useCallback(async () => {
    if (form != null) {
      const response = await clientService.changeData(
        form.name,
        form.email,
        form.phoneNumber,
        form.city,
        form.street);

      if (response.result == 'successful') {
        setIsSuccessfulRequestModalToggled(true);
      }
    }
  }, [form]);

  const isDisable = () => {
    return form?.name == '' ||
      form?.email == '' ||
      form?.phoneNumber == '' ||
      form?.city == '' ||
      form?.street == '';
  };

  const isFocused = useIsFocused();

  React.useEffect(() => {
    const getUser = async () => {
      const user = await userService.getUserWithoutCache();
      if (user != null) {
        setForm(user);
      }
    };

    getUser();
    setIsChanged(false);
  }, [isFocused]);

  return (
    <View style={styles.container}>
      {form == null ? (
        <ActivityIndicator size={'large'} color={'#2D81E0'}/>
      ) : (
        <ScrollView showsVerticalScrollIndicator={false}>
          <Text style={styles.title}>Аккаунт</Text>
          <View style={styles.iconContainer}>
            <Image
              style={styles.icon}
              source={{
                uri: `${process.env.MINIO_URL}/app-files/${form.iconUri}`,
              }}/>
          </View>
          <View style={styles.textButtonContainer}>
            <TextButton
              text={'Изменить логотип'}
              onPress={toggle}/>
          </View>
          <View style={{paddingHorizontal: 15}}>
            <View style={styles.splitterContainer}>
              <View style={styles.splitter}/>
            </View>
            <Text style={styles.sectionTitle}>Контактные данные</Text>
            <Text style={styles.sectionDescription}>Укажите информацию, которая будет отображаться в карточке вашей компании, ее увидят тысячи наших пользователей</Text>
            <TextInputTitle
              s={'Название'}
              top={20}
              bottom={5}/>
            <BorderedTextInput
              value={form?.name}
              onChanged={(text: string) => set(prev => ({...prev, name: text}))}
              placeholder={'Введите название'}
              isError={false}
              isBig={false}
              keyboard={'default'}/>
            <TextInputTitle
              s={'E-mail'}
              top={20}
              bottom={5}/>
            <BorderedTextInput
              value={form?.email}
              onChanged={(text: string) => set(prev => ({...prev, email: text}))}
              placeholder={'Введите e-mail'}
              isError={false}
              isBig={false}
              keyboard={'default'}/>
            <TextInputTitle
              s={'Номер телефона'}
              top={20}
              bottom={5}/>
            <BorderedTextInput
              value={form?.phoneNumber}
              onChanged={(text: string) => set(prev => ({...prev, phoneNumber: text}))}
              placeholder={'Введите номер телефона'}
              isError={false}
              isBig={false}
              keyboard={'phone-pad'}/>
            <TextInputTitle
              s={'Город'}
              top={20}
              bottom={5}/>
            <BorderedTextInput
              value={form?.city}
              onChanged={(text: string) => set(prev => ({...prev, city: text}))}
              placeholder={'Город'}
              isError={false}
              isBig={false}
              keyboard={'default'}/>
            <TextInputTitle
              s={'Улица'}
              top={20}
              bottom={5}/>
            <BorderedTextInput
              value={form?.street}
              onChanged={(text: string) => set(prev => ({...prev, street: text}))}
              placeholder={'Улица'}
              isError={false}
              isBig={false}
              keyboard={'default'}/>
            <StyledButton
              content={'Изменить пароль'}
              top={20}
              bottom={0}
              isDisabled={false}
              pressed={() => navigation.navigate('ChangePassword')}
              type={'reversed'}/>
            <StyledButton
              content={'Выйти из акканта'}
              top={20}
              bottom={0}
              isDisabled={false}
              pressed={signOut}
              type={'warn'}/>
            {isChanged && (
              <StyledButton
                content={'Сохранить изменения'}
                top={20}
                bottom={0}
                isDisabled={isDisable()}
                pressed={saveChanges}/>
            )}
          </View>
        </ScrollView>
      )}
      <ChangeIconUriModal
        isToggled={isChangeIconUriModalToggled}
        handlePress={toggle}
        setIcon={setIcon}/>
      <SuccessfulRequestModal
        isToggled={isSuccessfulRequestModalToggled}
        handlePress={() => {
          setIsChanged(false);
          setIsSuccessfulRequestModalToggled(false);
        }}
        title={'Изменения сохранены'}
        text={''}/>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  title: {
    fontWeight: '700',
    fontSize: 22,
    alignSelf: 'center',
    paddingTop: 30,
  },
  iconContainer: {
    alignSelf: 'center',
    justifyContent: 'center',
    paddingTop: 20,
  },
  icon: {
    width: 80,
    height: 80,
    borderRadius: 40,
    resizeMode: 'cover',
    alignSelf: 'center',
  },
  textButtonContainer: {
    justifyContent: 'center',
    paddingTop: 10,
  },
  splitter: {
    backgroundColor: '#D7D8D9',
    height: 1,
  },
  splitterContainer: {
    paddingTop: 20,
  },
  sectionTitle: {
    fontWeight: '700',
    fontSize: 17,
    paddingTop: 10,
  },
  sectionDescription: {
    paddingTop: 10,
    color: '#181818',
    fontSize: 16,
    fontWeight: '400',
  },
});
