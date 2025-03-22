import * as React from 'react';
import {ActivityIndicator, FlatList, Image, ScrollView, StyleSheet, Text, TextInput, View} from 'react-native';
import {useDependency} from '../services/Hooks.ts';
import TextButton from '../components/buttons/TextButton.tsx';
import TextInputTitle from '../components/TextInputTitle.tsx';
import GestureBorderedTextInput from '../components/inputs/GestureBordererdTextInput.tsx';
import SuccessfulRequestModal from '../components/modals/SuccessfulRequestModal.tsx';
import {EditCompanyScreenProps} from '../types/NavigationTypes.ts';
import {AdminService} from '../services/domain/AdminService.ts';
import {ObjectStorageService} from '../services/object/ObjectStorageService.ts';
import ImageBox, {ImageBoxObject, MinioBlob, UploadedBlob} from '../components/ImageBox.tsx';
import {launchImageLibrary} from 'react-native-image-picker';
import {FileValidationService} from '../services/object/FileValidationService.ts';
import {GestureHandlerRootView, TouchableOpacity} from 'react-native-gesture-handler';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {useEffect, useMemo, useRef, useState} from 'react';
import SocialMediaItem from '../components/listItems/SocialMediaItem.tsx';
import Styles from '../constants/Styles.tsx';
import BottomSheet from '@gorhom/bottom-sheet';
import {Category} from '../types/DomainTypes.ts';
import PickCategoriesBottomSheet from '../components/bottomSheets/PickCategoriesBottomSheet.tsx';
import LongRunningOperationIndicator from '../components/LongRunningOperationIndicator.tsx';
import SocialMediaModal from '../components/modals/SocialMediaModal.tsx';
import {CategoryService} from '../services/domain/CategoryService.ts';

type Form = {
  id: string
  name: string
  email: string
  phoneNumber: string
  city: string
  street: string
  description: string
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
    <TouchableOpacity
      style={[
        styles.optionButton, {
          borderColor: selected ? '#2688EB' : '#B8C1CC',
        }]}
      onPress={onPress}
      disabled={selected}>
      {selected ? (<View style={styles.optionSelected}/>) : (<></>)}
    </TouchableOpacity>
    <Text style={styles.optionTitle}>{title}</Text>
  </View>
);

export default function EditCompanyScreen({route, navigation}: EditCompanyScreenProps) {
  const adminService = useDependency<AdminService>('AdminService');
  const objectStorageService = useDependency<ObjectStorageService>('ObjectStorageService');
  const fileValidationService = useDependency<FileValidationService>('FileValidationService');
  const categoryService = useDependency<CategoryService>('CategoryService');

  const [form, setForm] = React.useState<Form | null>(null);
  const [iconUri, setIconUri] = React.useState<ImageBoxObject>(MinioBlob.createDefault());

  const [readonly, setReadonly] = React.useState(true);

  const [isSuccessfulRequestModalToggled, setIsSuccessfulRequestModalToggled] = React.useState(false);

  const [socialMedias, setSocialMedias] = useState<any[]>([]);
  const [currentIndex, setCurrentIndex] = useState(0);
  const [isSocialMediaModalToggled, setIsSocialMediaModalToggled] = useState(false);
  const [photoUris, setPhotoUris] = useState<ImageBoxObject[]>([]);
  const [categories, setCategories] = useState<{category: Category, selected: boolean}[]>([]);

  const [isRefreshing, setIsRefreshing] = React.useState(false);

  const categoriesTitle = useMemo(() =>
      categories
        .filter(c => c.selected)
        .map(c => c.category.title)
        .join(', '),
    [categories]);

  const ref = useRef<BottomSheet>(null);

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
    }
  };

  const onOptionPressed = () => setForm(prev => ({...prev!, isPrepaymentAvailable: !prev!.isPrepaymentAvailable}));

  useEffect(() => {
    if (form != null) {
      const init = [
        {
          title: 'Instagram',
          icon: require('../assets/images/instagram.png'),
          uri: '',
          onPress: (val: boolean) => {},
        },
        {
          title: 'Facebook',
          icon: require('../assets/images/facebook.png'),
          uri: '',
          onPress: (val: boolean) => {},
        },
        {
          title: 'VK',
          icon: require('../assets/images/vk.png'),
          uri: '',
          onPress: (val: boolean) => {},
        },
        {
          title: 'Telegram',
          icon: require('../assets/images/tg.png'),
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
  }, [form]);

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
      setIsRefreshing(true);

      const c = categories.filter(c => c.selected).map(c => c.category.categoryId);
      const s = socialMedias.filter(s => s.uri != '').map(s => s.uri);

      const response = await adminService.editCompany(
        form.id,
        iconUri.getObjectName(),
        form.name,
        form.description,
        form.email,
        form.phoneNumber,
        form.city,
        form.street,
        s,
        c,
        photoUris.map(p => p.getObjectName()),
        form.isPrepaymentAvailable);

      if (response.result == 'successful') {
        if (!iconUri.isUpload) {
          await objectStorageService.upload(iconUri as MinioBlob);
        }

        for (let i = 0; i < photoUris.length; i++) {
          if (photoUris[i].getObjectName() != '' && !photoUris[i].isUpload) {
            await objectStorageService.upload(photoUris[i] as MinioBlob);
          }
        }

        setIsSuccessfulRequestModalToggled(true);
      }

      setIsRefreshing(false);
    }

    setReadonly(prev => !prev);
  }, [form, iconUri, readonly]);

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

  const select = (index: number) => {
    setCategories(prev => {
      prev[index].selected = !prev[index].selected;

      return [...prev];
    });
  };

  React.useEffect(() => {
    const getUser = async () => {
      const response = await adminService.getCompany(route.params.companyId);

      if (response.result == 'successful') {
        setForm(response.content!);
        setPhotoUris(response.content!.photoUris.map(p => new UploadedBlob(p)));
        setIconUri(new UploadedBlob(response.content.iconUri));

        const categoriesResponse = await categoryService.getCategories();

        if (categoriesResponse.result == 'successful') {
          setCategories(categoriesResponse.content!.map(c => ({
            category: c,
            selected: response.content!.categoryIds.findIndex(id => id == c.categoryId) != -1,
          })));
        }
      }
    };

    getUser();
  }, []);

  return (
    <GestureHandlerRootView style={styles.container}>
      {form == null ? (
        <ActivityIndicator size={'large'} color={'#2D81E0'}/>
      ) : (
        <ScrollView showsVerticalScrollIndicator={false}>
          <View style={styles.controlsContainer}>
            <View style={{alignSelf: 'center'}}>
              <NavigateBackButton navigation={navigation} onGoBack={() => {}}/>
            </View>
            <Text style={styles.title}>{readonly ? 'Компания' : 'Изменить компанию'}</Text>
            <View style={{alignSelf: 'center'}}>
              <TouchableOpacity
                disabled={(!readonly && isDisable())}
                onPress={saveChanges}>
                <Image
                  style={[styles.editIcon, {
                    opacity: (!readonly && !isDisable()) || readonly ? 1 : 0.5,
                  }]}
                  source={readonly ? require('../assets/images/edit.png') : require('../assets/images/ok.png')}/>
              </TouchableOpacity>
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
              onChanged={(text: string) => setForm(prev => ({...prev!, name: text}))}
              placeholder={'Введите название'}
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
              onChanged={(text: string) => setForm(prev => ({...prev!, description: text}))}
              placeholder={'Введите описание компании'}
              isError={false}
              isBig={true}
              keyboard={'default'}
              isReadonly={readonly}/>
            <TextInputTitle
              s={'Виды деятельности'}
              top={20}
              bottom={5}/>
            <View style={styles.borderedInput}>
              <TextInput
                style={Styles.borderedTextInput}
                value={categoriesTitle == '' ? 'Выбрать деятельность' : categoriesTitle}
                readOnly/>
              {!readonly && (
                <View style={styles.chevronDown}>
                  <TouchableOpacity
                    onPress={() => ref.current?.expand()}>
                    <Image
                      style={styles.image}
                      source={require('../assets/images/chevron-down.png')}
                    />
                  </TouchableOpacity>
                </View>
              )}
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
                  setPhoto={setPhotoUris}
                  index={i}
                  readonly={readonly}/>
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
            <View style={{paddingTop: 20}}/>
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
      <PickCategoriesBottomSheet
        ref={ref}
        close={() => ref.current?.close()}
        categories={categories}
        select={select}/>
      <LongRunningOperationIndicator isRefreshing={isRefreshing}/>
      {socialMedias.length > 0 && (
        <SocialMediaModal
          isToggled={isSocialMediaModalToggled}
          handlePress={handlePress}
          title={socialMedias[currentIndex].title}
          onChange={(val) => setSocialMedias(prev => {
            prev[currentIndex].uri = val;
            return [...prev];
          })}/>
      )}
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
  chevronDown: {
    alignSelf: 'center',
    paddingRight: 10,
  },
  borderedInput: {
    ...Styles.borderedTextInputView,
    ...Styles.borderedTextInputHeight,
    ...Styles.borderedTextInputViewColor,
    ...Styles.borderedTextInputUnfocused,
  },
  photoUrisContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    flexWrap: 'wrap',
    rowGap: 20,
  },
  image: {
    resizeMode: 'contain',
    width: 15,
    height: 15,
  },
  optionContainer: {
    flexDirection: 'row',
  },
});
