import * as React from 'react';
import {ActivityIndicator, Image, ScrollView, StyleSheet, Text, View} from 'react-native';
import {useIsFocused} from '@react-navigation/native';
import {useDependency} from '../services/Hooks.ts';
import TextButton from '../components/buttons/TextButton.tsx';
import TextInputTitle from '../components/TextInputTitle.tsx';
import GestureBorderedTextInput from '../components/inputs/GestureBordererdTextInput.tsx';
import SuccessfulRequestModal from '../components/modals/SuccessfulRequestModal.tsx';
import {EditClientScreenProps} from '../types/NavigationTypes.ts';
import {AdminService} from '../services/domain/AdminService.ts';
import {ObjectStorageService} from '../services/object/ObjectStorageService.ts';
import {ImageBoxObject, MinioBlob, UploadedBlob} from '../components/ImageBox.tsx';
import {launchImageLibrary} from 'react-native-image-picker';
import {FileValidationService} from '../services/object/FileValidationService.ts';
import {GestureHandlerRootView, Pressable} from 'react-native-gesture-handler';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {GestureStyledButton} from '../components/buttons/GestureStyledButton.tsx';
import {SafeAreaView} from "react-native-safe-area-context";

type Form = {
  id: string
  name: string
  surname: string
  email: string
  phoneNumber: string
  city: string
  street: string
  averageGrade: number
}

export default function EditClientScreen({route, navigation}: EditClientScreenProps) {
  const userService = useDependency<AdminService>('AdminService');
  const objectStorageService = useDependency<ObjectStorageService>('ObjectStorageService');
  const fileValidationService = useDependency<FileValidationService>('FileValidationService');

  const [form, setForm] = React.useState<Form | null>(null);
  const [iconUri, setIconUri] = React.useState<ImageBoxObject>(MinioBlob.createDefault());

  const [readonly, setReadonly] = React.useState(true);

  const [isSuccessfulRequestModalToggled, setIsSuccessfulRequestModalToggled] = React.useState(false);

  const changeIconUri = React.useCallback(async () => {
    if (!readonly) {
      const response = await launchImageLibrary({mediaType: 'photo'});

      if (response.assets != undefined && response.assets.length > 0) {
        const validationResult = await fileValidationService.getContentAndValidate(response.assets[0].uri!);

        if (validationResult.object != null) {
          setIconUri(validationResult.object);
        }
      }
    }
  }, [readonly]);

  const saveChanges = React.useCallback(async () => {
    if (!readonly && form != null) {
      const response = await userService.editClient(
        route.params.clientId,
        iconUri.getObjectName(),
        `${form.name} ${form.surname}`,
        form.phoneNumber,
        form.email,
        form.city,
        form.street);

      if (response.result == 'successful') {

        if (!iconUri.isUpload) {
          await objectStorageService.upload(iconUri as MinioBlob);
        }

        setIsSuccessfulRequestModalToggled(true);
      }
    }

    setReadonly(prev => !prev);
  }, [form, iconUri, readonly]);

  const deleteClient = React.useCallback(async () => {
    if (form != null) {
      const response = await userService.deleteClient(form.id);

      if (response.result == 'successful') {
        navigation.goBack();
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

  React.useEffect(() => {
    const getUser = async () => {
      const response = await userService.getClient(route.params.clientId);
      if (response.result == 'successful') {
        const initials = response.content.name.split(' ');

        setForm({...response.content, name: initials[0], surname: initials[1]});
        setIconUri(new UploadedBlob(response.content.iconUri));
      }
    };

    getUser();
  }, [isFocused]);

  return (
    <GestureHandlerRootView style={styles.container}>
      <SafeAreaView style={{flex: 1}}>
        {form == null ? (
          <ActivityIndicator size={'large'} color={'#2D81E0'}/>
        ) : (
          <ScrollView showsVerticalScrollIndicator={false}>
            <View style={styles.controlsContainer}>
              <View style={{alignSelf: 'center'}}>
                <NavigateBackButton navigation={navigation} onGoBack={() => {}}/>
              </View>
              <Text style={styles.title}>{readonly ? 'Клиент' : 'Изменить клиента'}</Text>
              <View style={{alignSelf: 'center'}}>
                <Pressable
                  disabled={(!readonly && isDisabled())}
                  onPress={saveChanges}>
                  <Image
                    style={[styles.editIcon, {
                      opacity: (!readonly && !isDisabled()) || readonly ? 1 : 0.5,
                    }]}
                    source={readonly ? require('../assets/images/edit.png') : require('../assets/images/ok.png')}/>
                </Pressable>
              </View>
            </View>
            <View style={styles.iconContainer}>
              <Image
                style={styles.icon}
                source={{
                  uri: iconUri.getUri(),
                }}/>
            </View>
            <View style={styles.textButtonContainer}>
              <TextButton
                text={'Изменить фото'}
                onPress={changeIconUri}/>
            </View>
            <View style={{paddingHorizontal: 15}}>
              <TextInputTitle
                s={'Имя'}
                top={20}
                bottom={5}/>
              <GestureBorderedTextInput
                value={form?.name}
                onChanged={(text: string) => setForm(prev => ({...prev!, name: text}))}
                placeholder={'Введите имя'}
                isError={false}
                isBig={false}
                keyboard={'default'}
                isReadonly={readonly}/>
              <TextInputTitle
                s={'Фамилия'}
                top={20}
                bottom={5}/>
              <GestureBorderedTextInput
                value={form?.surname}
                onChanged={(text: string) => setForm(prev => ({...prev!, surname: text}))}
                placeholder={'Введите фамилию'}
                isError={false}
                isBig={false}
                keyboard={'default'}
                isReadonly={readonly}/>
              <TextInputTitle
                s={'E-mail'}
                top={20}
                bottom={5}/>
              <GestureBorderedTextInput
                value={form?.email}
                onChanged={(text: string) => setForm(prev => ({...prev!, email: text}))}
                placeholder={'Введите e-mail'}
                isError={false}
                isBig={false}
                keyboard={'default'}
                isReadonly={readonly}/>
              <TextInputTitle
                s={'Номер телефона'}
                top={20}
                bottom={5}/>
              <GestureBorderedTextInput
                value={form?.phoneNumber}
                onChanged={(text: string) => setForm(prev => ({...prev!, phoneNumber: text}))}
                placeholder={'Введите номер телефона'}
                isError={false}
                isBig={false}
                keyboard={'phone-pad'}
                isReadonly={readonly}/>
              <TextInputTitle
                s={'Город'}
                top={20}
                bottom={5}/>
              <GestureBorderedTextInput
                value={form?.city}
                onChanged={(text: string) => setForm(prev => ({...prev!, city: text}))}
                placeholder={'Город'}
                isError={false}
                isBig={false}
                keyboard={'default'}
                isReadonly={readonly}/>
              <TextInputTitle
                s={'Улица'}
                top={20}
                bottom={5}/>
              <GestureBorderedTextInput
                value={form?.street}
                onChanged={(text: string) => setForm(prev => ({...prev!, street: text}))}
                placeholder={'Улица'}
                isError={false}
                isBig={false}
                keyboard={'default'}
                isReadonly={readonly}/>
              <GestureStyledButton
                content={'Отзывы'}
                top={30}
                bottom={10}
                isDisabled={false}
                pressed={() => navigation.navigate('ClientReviews', {clientId: route.params.clientId})}
                type={'reversed'}/>
              <GestureStyledButton
                content={'Заблокировать клиента'}
                top={10}
                bottom={10}
                isDisabled={false}
                pressed={deleteClient}
                type={'warn'}/>
            </View>
          </ScrollView>
        )}
        <SuccessfulRequestModal
          isToggled={isSuccessfulRequestModalToggled}
          handlePress={() => {
            setIsSuccessfulRequestModalToggled(false);
            navigation.goBack();
          }}
          title={'Изменения сохранены'}
          text={''}/>
      </SafeAreaView>
    </GestureHandlerRootView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  editIcon: {
    width: 20,
    height: 20,
    resizeMode: 'contain',
  },
  controlsContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingHorizontal: 15,
    paddingTop: 30,
  },
  title: {
    fontWeight: '700',
    fontSize: 22,
    alignSelf: 'center',
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
