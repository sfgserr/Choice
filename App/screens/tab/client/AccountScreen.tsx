import * as React from 'react';
import {
  ActivityIndicator,
  Image,
  KeyboardAvoidingView,
  RefreshControl,
  ScrollView,
  StyleSheet,
  Text,
  View
} from 'react-native';
import {AuthContext} from '../../../contexts/authorized/Context.tsx';
import {AccountScreenProps} from '../../../types/NavigationTypes.ts';
import {useDependency} from '../../../services/Hooks.ts';
import {UserService} from '../../../services/domain/UserService.ts';
import TextButton from '../../../components/buttons/TextButton.tsx';
import ChangeIconUriModal from '../../../components/modals/ChangeIconUriModal.tsx';
import TextInputTitle from '../../../components/TextInputTitle.tsx';
import {SetStateAction } from 'react';
import {useIsFocused} from '@react-navigation/native';
import SuccessfulRequestModal from '../../../components/modals/SuccessfulRequestModal.tsx';
import {ClientService} from '../../../services/domain/ClientService.ts';
import {GestureStyledButton} from '../../../components/buttons/GestureStyledButton.tsx';
import GestureBorderedTextInput from '../../../components/inputs/GestureBordererdTextInput.tsx';
import {PaymentService} from '../../../services/domain/PaymentService.tsx';
import {Pressable} from 'react-native-gesture-handler';
import {Icon} from '@rneui/base';
import PayModal from '../../../components/modals/PayModal.tsx';
import {useSafeAreaInsets} from "react-native-safe-area-context";
import PhoneBox from "../../../components/inputs/PhoneBox.tsx";
import KeyboardAvoidingScrollView from "../../../components/KeyboardAvoidingScrollView.tsx";

type Form = {
  id: string
  name: string
  surname: string
  email: string
  phoneNumber: string
  city: string
  street: string
  iconUri: string
}

export default function AccountScreen({route, navigation}: AccountScreenProps) {
  const userService = useDependency<UserService>('UserService');
  const clientService = useDependency<ClientService>('ClientService');
  const paymentService = useDependency<PaymentService>('PaymentService');

  const { signOut } = React.useContext(AuthContext);

  const [form, setForm] = React.useState<Form | null>(null);
  const [isChanged, setIsChanged] = React.useState<boolean>(false);
  const [balance, setBalance] = React.useState<string>('');

  const [isChangeIconUriModalToggled, setIsChangeIconUriModalToggled] = React.useState(false);
  const [isSuccessfulRequestModalToggled, setIsSuccessfulRequestModalToggled] = React.useState(false);
  const [isPayModalToggled, setIsPayModalToggled] = React.useState(false);

  const [isRefreshing, setIsRefreshing] = React.useState(false);

  const insets = useSafeAreaInsets();

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
        `${form.name} ${form.surname}`,
        form.email,
        form.phoneNumber,
        form.city,
        form.street);

      if (response.result == 'successful') {
        setIsSuccessfulRequestModalToggled(true);
      }
    }
  }, [form]);

  const isDisabled = () => {
    return form?.name == '' ||
      form?.surname == '' ||
      form?.email == '' ||
      form?.phoneNumber == '' ||
      form?.city == '' ||
      form?.street == '';
  };

  const isFocused = useIsFocused();

  const refresh = React.useCallback(async () => {
    setIsRefreshing(true);

    await getUser();
    await getBalance();

    setIsRefreshing(false);
  }, []);

  const getUser = React.useCallback(async () => {
    const user = await userService.getUserWithoutCache();
    if (user != null) {
      const initials = user.name.split(' ');

      setForm({...user, name: initials[0], surname: initials[1]});
    }
  }, []);

  const getBalance = React.useCallback(async () => {
    const response = await paymentService.getWallet();

    if (response.result == 'successful') {
      setBalance(((response.content / 100).toFixed(2)));
    }
  }, []);

  React.useEffect(() => {
    getUser();
    getBalance();
    setIsChanged(false);
  }, [isFocused]);

  return (
    <View style={[styles.container, { paddingTop: insets.top }]}>
      {form == null || balance == '' ? (
        <ActivityIndicator size={'large'} color={'#2D81E0'} />
      ) : (
        <KeyboardAvoidingScrollView
          scrollable={false}
          tabs
          focused={isFocused}>
          <ScrollView
            showsVerticalScrollIndicator={false}
            refreshControl={<RefreshControl refreshing={isRefreshing} onRefresh={refresh} />}>
            <Text style={styles.title}>Аккаунт</Text>
            <View style={styles.iconContainer}>
              <Image
                style={styles.icon}
                source={{
                  uri: `${process.env.MINIO_URL}/app-files/${form.iconUri}`,
                }}
              />
            </View>
            <View style={styles.textButtonContainer}>
              <TextButton text={'Изменить фото'} onPress={toggle} />
            </View>
            <View style={{paddingTop: 15, paddingHorizontal: 15}}>
              <Text style={{fontSize: 15, fontWeight: '600', color: 'black'}}>Баланс:</Text>
              <View style={{flexDirection: 'row', alignItems: 'center'}}>
                <Text style={{fontSize: 30, fontWeight: '700', color: 'black'}}>{`${balance} \u20bd`}</Text>
                <Pressable onPress={() => setIsPayModalToggled(prev => !prev)}>
                  <Icon
                    type={'material'}
                    name={'add'}
                    color={'#2688EB'}/>
                </Pressable>
              </View>
            </View>
            <View style={{paddingHorizontal: 15}}>
              <TextInputTitle s={'Имя'} top={20} bottom={5} />
              <GestureBorderedTextInput
                value={form?.name}
                onChanged={(text: string) => set(prev => ({...prev, name: text}))}
                placeholder={'Введите имя'}
                isError={false}
                isBig={false}
                keyboard={'default'}
              />
              <TextInputTitle s={'Фамилия'} top={20} bottom={5} />
              <GestureBorderedTextInput
                value={form?.surname}
                onChanged={(text: string) =>
                  set(prev => ({...prev, surname: text}))
                }
                placeholder={'Введите фамилию'}
                isError={false}
                isBig={false}
                keyboard={'default'}
              />
              <TextInputTitle s={'E-mail'} top={20} bottom={5} />
              <GestureBorderedTextInput
                value={form?.email}
                onChanged={(text: string) =>
                  set(prev => ({...prev, email: text}))
                }
                placeholder={'Введите e-mail'}
                isError={false}
                isBig={false}
                keyboard={'default'}
              />
              <TextInputTitle s={'Номер телефона'} top={20} bottom={5} />
              <PhoneBox
                value={form?.phoneNumber}
                onChanged={(text: string) =>
                  set(prev => ({...prev, phoneNumber: text}))
                }
                isError={false}
                isReadonly={false}
              />
              <TextInputTitle s={'Город'} top={20} bottom={5} />
              <GestureBorderedTextInput
                value={form?.city}
                onChanged={(text: string) => set(prev => ({...prev, city: text}))}
                placeholder={'Город'}
                isError={false}
                isBig={false}
                keyboard={'default'}
              />
              <TextInputTitle s={'Улица'} top={20} bottom={5} />
              <GestureBorderedTextInput
                value={form?.street}
                onChanged={(text: string) =>
                  set(prev => ({...prev, street: text}))
                }
                placeholder={'Улица'}
                isError={false}
                isBig={false}
                keyboard={'default'}
              />
              <GestureStyledButton
                content={'Изменить пароль'}
                top={20}
                bottom={0}
                isDisabled={false}
                pressed={() => navigation.navigate('ChangePassword')}
                type={'reversed'}
              />
              <GestureStyledButton
                content={'Выйти из аккаунта'}
                top={20}
                bottom={10}
                isDisabled={false}
                pressed={signOut}
                type={'warn'}
              />
              {isChanged && (
                <GestureStyledButton
                  content={'Сохранить изменения'}
                  top={20}
                  bottom={0}
                  isDisabled={isDisabled()}
                  pressed={saveChanges}
                />
              )}
            </View>
          </ScrollView>
        </KeyboardAvoidingScrollView>
      )}
      <ChangeIconUriModal
        isToggled={isChangeIconUriModalToggled}
        handlePress={toggle}
        setIcon={setIcon}
      />
      <SuccessfulRequestModal
        isToggled={isSuccessfulRequestModalToggled}
        handlePress={() => {
          setIsChanged(false);
          setIsSuccessfulRequestModalToggled(false);
        }}
        title={'Изменения сохранены'}
        text={''}
      />
      <PayModal
        isToggled={isPayModalToggled}
        handlePress={() => setIsPayModalToggled(prev => !prev)}/>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  title: {
    fontWeight: '600',
    fontSize: 22,
    alignSelf: 'center',
    color: 'black',
    paddingTop: 20,
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
});
