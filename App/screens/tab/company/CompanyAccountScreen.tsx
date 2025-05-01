import * as React from 'react';
import {
  ActivityIndicator,
  FlatList,
  Image, RefreshControl,
  ScrollView,
  StyleSheet,
  Text,
  TextInput,
  View,
} from 'react-native';
import { Pressable } from 'react-native-gesture-handler';
import {AuthContext} from '../../../contexts/authorized/Context.tsx';
import {AccountScreenProps} from '../../../types/NavigationTypes.ts';
import {useDependency} from '../../../services/Hooks.ts';
import {UserService} from '../../../services/domain/UserService.ts';
import TextButton from '../../../components/buttons/TextButton.tsx';
import ChangeIconUriModal from '../../../components/modals/ChangeIconUriModal.tsx';
import TextInputTitle from '../../../components/TextInputTitle.tsx';
import {SetStateAction, useCallback, useContext, useEffect, useMemo, useRef, useState} from 'react';
import {useIsFocused} from '@react-navigation/native';
import SuccessfulRequestModal from '../../../components/modals/SuccessfulRequestModal.tsx';
import SocialMediaModal from '../../../components/modals/SocialMediaModal.tsx';
import SocialMediaItem from '../../../components/listItems/SocialMediaItem.tsx';
import Styles from '../../../constants/Styles.tsx';
import PickCategoriesBottomSheet from '../../../components/bottomSheets/PickCategoriesBottomSheet.tsx';
import BottomSheet from '@gorhom/bottom-sheet';
import {Category} from '../../../types/DomainTypes.ts';
import {CategoryService} from '../../../services/domain/CategoryService.ts';
import ImageBox, {ImageBoxObject, MinioBlob, UploadedBlob} from '../../../components/ImageBox.tsx';
import {CompanyService} from '../../../services/domain/CompanyService.ts';
import {ObjectStorageService} from '../../../services/object/ObjectStorageService.ts';
import LongRunningOperationIndicator from '../../../components/LongRunningOperationIndicator.tsx';
import {GestureStyledButton} from '../../../components/buttons/GestureStyledButton.tsx';
import GestureBorderedTextInput from '../../../components/inputs/GestureBordererdTextInput.tsx';

type Form = {
  id: string
  name: string
  email: string
  phoneNumber: string
  city: string
  street: string
  description: string
  iconUri: string
  socialMedias: SocialMedia[]
  photoUris: string[]
  categories: number[]
  isPrepaymentAvailable: boolean
}

type SocialMedia = {
  platform: string
  url: string
}

const Option = ({selected, title, onPress, top}: {
  selected: boolean
  title: string
  onPress: () => void
  top: number}) => (
  <View style={[styles.optionContainer, {paddingTop: top}]}>
    <Pressable
      style={[
        styles.optionButton, {
          borderColor: selected ? '#2688EB' : '#B8C1CC',
        }]}
      onPress={onPress}
      disabled={selected}>
      {selected ? (<View style={styles.optionSelected}/>) : (<></>)}
    </Pressable>
    <Text style={styles.optionTitle}>{title}</Text>
  </View>
);

export default function CompanyAccountScreen({route, navigation}: AccountScreenProps) {
  const userService = useDependency<UserService>('UserService');
  const companyService = useDependency<CompanyService>('CompanyService');
  const categoryService = useDependency<CategoryService>('CategoryService');
  const objectStorageService = useDependency<ObjectStorageService>('ObjectStorageService');

  const isFocused = useIsFocused();

  const { signOut } = useContext(AuthContext);

  const [form, setForm] = useState<Form | null>(null);
  const [isChanged, setIsChanged] = useState<boolean>(false);

  const [isChangeIconUriModalToggled, setIsChangeIconUriModalToggled] = useState(false);
  const [isSuccessfulRequestModalToggled, setIsSuccessfulRequestModalToggled] = useState(false);

  const [currentIndex, setCurrentIndex] = useState(0);
  const [isSocialMediaModalToggled, setIsSocialMediaModalToggled] = useState(false);

  const [categories, setCategories] = useState<{category: Category, selected: boolean}[]>([]);

  const categoriesTitle = useMemo(() =>
    categories
      .filter(c => c.selected)
      .map(c => c.category.title)
      .join(', '),
    [categories]);

  const [photoUris, setPhotoUris] = useState<ImageBoxObject[]>([]);

  const [isRefreshingOnSave, setIsRefreshingOnSave] = useState(false);
  const [refreshing, setRefreshing] = useState(false);

  const setPhoto = (object: SetStateAction<ImageBoxObject[]>) => {
    setPhotoUris(object);
    setIsChanged(true);
  };

  const ref = useRef<BottomSheet>(null);

  const select = (index: number) => {
    setCategories(prev => {
      prev[index].selected = !prev[index].selected;

      return [...prev];
    });
    setIsChanged(true);
  };

  const handlePress = () => setIsSocialMediaModalToggled(prev => !prev);

  const onPress = (index: number, val: boolean) => {
    if (val) {
      setCurrentIndex(index);
      handlePress();
    }
    else {
      setSocialMedias(prev => {
        prev[index].uri = '';
        return [...prev];
      });
      setIsChanged(true);
    }
  };

  const [socialMedias, setSocialMedias] = useState<any[]>([]);

  useEffect(() => {
    if (form != null) {
      const init = [
        {
          title: 'Instagram',
          icon: require('../../../assets/images/instagram.png'),
          uri: '',
          onPress: (val: boolean) => {},
        },
        {
          title: 'Facebook',
          icon: require('../../../assets/images/facebook.png'),
          uri: '',
          onPress: (val: boolean) => {},
        },
        {
          title: 'VK',
          icon: require('../../../assets/images/vk.png'),
          uri: '',
          onPress: (val: boolean) => {},
        },
        {
          title: 'Telegram',
          icon: require('../../../assets/images/tg.png'),
          uri: '',
          onPress: (val: boolean) => {},
        },
      ];

      for (let i = 0; i < init.length; i++) {
        let socialMedia = form.socialMedias.find(s => s.platform == init[i].title);

        init[i].uri = socialMedia != undefined ? socialMedia.url : '';
        init[i].onPress = (val: boolean) => onPress(i, val);
      }

      setSocialMedias(init);
    }
  }, [form, isFocused]);

  const toggle = useCallback(() => setIsChangeIconUriModalToggled(prev => !prev), []);

  const setIcon = useCallback((objectName: string) => {
    setForm(prev => ({...prev, iconUri: objectName}));
  }, []);

  const set = useCallback((func: SetStateAction<Form | null>) => {
    setForm(func);

    setIsChanged(true);
  }, []);

  const saveChanges = useCallback(async () => {
    if (form != null) {
      setIsRefreshingOnSave(true);

      const c = categories.filter(c => c.selected).map(c => c.category.categoryId);
      const s = socialMedias.filter(s => s.uri != '').map(s => s.uri);

      const response = await companyService.changeData(
        form.name,
        form.phoneNumber,
        form.email,
        form.city,
        form.street,
        form.description,
        c,
        photoUris.map(p => p.getObjectName()),
        s,
        form.isPrepaymentAvailable);

      if (response.result == 'successful') {
        for (let i = 0; i < photoUris.length; i++) {
          if (photoUris[i].getObjectName() != '' && !photoUris[i].isUpload) {
            await objectStorageService.upload(photoUris[i] as MinioBlob);
          }
        }

        setIsSuccessfulRequestModalToggled(true);
      }

      setIsRefreshingOnSave(false);
    }
  }, [form, socialMedias, categories]);

  const isDisable = () => {
    return form?.name == '' ||
      form?.email == '' ||
      form?.phoneNumber == '' ||
      form?.city == '' ||
      form?.street == '' ||
      socialMedias.every(c => c.uri == '') ||
      categoriesTitle == '' ||
      photoUris.every(c => c.getObjectName() == '');
  };

  const getUser = React.useCallback(async () => {
    const user = await userService.getUserWithoutCache();

    if (user != null) {
      setForm(user);
      setPhotoUris(user.photoUris.map(p => new UploadedBlob(p)));

      const response = await categoryService.getCategories();

      if (response.result == 'successful') {
        setCategories(response.content!.map(c => ({
          category: c,
          selected: user.categories.findIndex(id => id == c.categoryId) != -1,
        })));
      }
    }
  }, []);

  const refresh = React.useCallback(async () => {
    setRefreshing(true);

    await getUser();

    setRefreshing(false);
  }, []);

  const onOptionPressed = () => set(prev => ({...prev, isPrepaymentAvailable: !prev.isPrepaymentAvailable}));

  useEffect(() => {
    getUser();
    setIsChanged(false);
  }, [isFocused]);

  return (
    <View style={styles.container}>
      {form == null ? (
        <ActivityIndicator size={'large'} color={'#2D81E0'}/>
      ) : (
        <ScrollView
          showsVerticalScrollIndicator={false}
          refreshControl={<RefreshControl refreshing={refreshing} onRefresh={refresh}/>}>
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
            <GestureBorderedTextInput
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
            <GestureBorderedTextInput
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
            <GestureBorderedTextInput
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
            <GestureBorderedTextInput
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
            <GestureBorderedTextInput
              value={form?.street}
              onChanged={(text: string) => set(prev => ({...prev, street: text}))}
              placeholder={'Улица'}
              isError={false}
              isBig={false}
              keyboard={'default'}/>
            <View style={styles.splitterContainer}>
              <View style={styles.splitter}/>
            </View>
            <Text style={styles.sectionTitle}>Социальные сети</Text>
            <FlatList
              data={socialMedias}
              style={styles.flatList}
              renderItem={item => (
                <SocialMediaItem item={item.item}/>
              )}/>
            <Text style={styles.sectionTitle}>О работе</Text>
            <TextInputTitle
              s={'Описание'}
              top={20}
              bottom={5}/>
            <GestureBorderedTextInput
              value={form?.description}
              onChanged={(text: string) => set(prev => ({...prev, description: text}))}
              placeholder={'Введите описание компании'}
              isError={false}
              isBig={true}
              keyboard={'default'}/>
            <TextInputTitle
              s={'Виды деятельности'}
              top={20}
              bottom={5}/>
            <View style={styles.borderedInput}>
              <TextInput
                style={Styles.borderedTextInput}
                value={categoriesTitle == '' ? 'Выбрать деятельность' : categoriesTitle}
                readOnly/>
              <View style={styles.chevronDown}>
                <Pressable
                  onPress={() => ref.current?.expand()}>
                  <Image
                    style={styles.image}
                    source={require('../../../assets/images/chevron-down.png')}
                  />
                </Pressable>
              </View>
            </View>
            <TextInputTitle
              s={'Добавьте фотографии'}
              top={20}
              bottom={10}/>
            <View style={styles.photoUrisContainer}>
              {photoUris.map((u, i) => (
                <ImageBox
                  key={i}
                  object={photoUris[i]}
                  setPhoto={setPhoto}
                  index={i}
                  readonly={false}/>
              ))}
            </View>
            <TextInputTitle
              s={'Опции'}
              top={20}
              bottom={5}/>
            <Option
              selected={form.isPrepaymentAvailable}
              title={'Работа с предоплатой'}
              onPress={onOptionPressed}
              top={0}/>
            <Option
              selected={!form.isPrepaymentAvailable}
              title={'Работа без предоплатой'}
              onPress={onOptionPressed}
              top={10}/>
            <GestureStyledButton
              content={'Изменить пароль'}
              top={20}
              bottom={0}
              isDisabled={false}
              pressed={() => navigation.navigate('ChangePassword')}
              type={'reversed'}/>
            <GestureStyledButton
              content={'Выйти из акканта'}
              top={20}
              bottom={0}
              isDisabled={false}
              pressed={signOut}
              type={'warn'}/>
            {isChanged && (
              <GestureStyledButton
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
      <PickCategoriesBottomSheet
        ref={ref}
        close={() => ref.current?.close()}
        categories={categories}
        select={select}/>
      <LongRunningOperationIndicator isRefreshing={isRefreshingOnSave}/>
      {socialMedias.length > 0 && (
        <SocialMediaModal
          isToggled={isSocialMediaModalToggled}
          handlePress={handlePress}
          title={socialMedias[currentIndex].title}
          onChange={(val) => setSocialMedias(prev => {
            prev[currentIndex].uri = val;
            setIsChanged(true);
            return [...prev];
          })}/>
      )}
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
  flatList: {
    paddingTop: 10,
  },
  borderedInput: {
    ...Styles.borderedTextInputView,
    ...Styles.borderedTextInputHeight,
    ...Styles.borderedTextInputViewColor,
    ...Styles.borderedTextInputUnfocused,
  },
  chevronDown: {
    alignSelf: 'center',
    paddingRight: 10,
  },
  image: {
    resizeMode: 'contain',
    width: 15,
    height: 15,
  },
  photoUrisContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    flexWrap: 'wrap',
    rowGap: 20,
  },
  optionContainer: {
    flexDirection: 'row',
  },
  optionButton: {
    alignSelf: 'center',
    width: 20,
    height: 20,
    borderRadius: 10,
    borderWidth: 2,
    justifyContent: 'center',
  },
  optionSelected: {
    alignSelf: 'center',
    width: 12,
    height: 12,
    borderRadius: 6,
    backgroundColor: '#2688EB',
  },
  optionTitle: {
    color: 'black',
    fontWeight: '400',
    fontSize: 15,
    alignSelf: 'center',
    paddingLeft: 10,
  },
});
