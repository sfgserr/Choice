import {Alert, Image, StyleSheet, Text, TouchableOpacity, View} from 'react-native';
import React from 'react';
import {EditCategoryScreenProps} from '../types/NavigationTypes.ts';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {ImageBoxObject, MinioBlob, UploadedBlob} from '../components/ImageBox.tsx';
import TextButton from '../components/buttons/TextButton.tsx';
import TextInputTitle from '../components/TextInputTitle.tsx';
import BorderedTextInput from '../components/inputs/BorderedTextInput.tsx';
import {launchImageLibrary} from 'react-native-image-picker';
import {useDependency} from '../services/Hooks.ts';
import {FileValidationService} from '../services/object/FileValidationService.ts';
import {CategoryService} from '../services/domain/CategoryService.ts';
import {ObjectStorageService} from '../services/object/ObjectStorageService.ts';
import {GestureHandlerRootView} from "react-native-gesture-handler";

export default function EditCategoryScreen({route, navigation}: EditCategoryScreenProps) {
  const fileValidationService = useDependency<FileValidationService>('FileValidationService');
  const categoryService = useDependency<CategoryService>('CategoryService');
  const objectStorageService = useDependency<ObjectStorageService>('ObjectStorageService');

  const [uri, setUri] = React.useState<ImageBoxObject>(new UploadedBlob(route.params.category.iconUri));
  const [title, setTitle] = React.useState<string>(route.params.category.title);

  const [readonly, setReadonly] = React.useState(true);

  const changeIconUri = React.useCallback(async () => {
    if (!readonly) {
      const response = await launchImageLibrary({mediaType: 'photo'});

      if (response.assets != undefined && response.assets.length > 0) {
        const result = await fileValidationService.getContentAndValidate(response.assets[0].uri!);

        if (result.object != null) setUri(result.object);
      }
    }
  }, [readonly]);

  const save = async () => {
    if (!readonly) {
      const response = await categoryService.changeCategory(
        route.params.category.categoryId,
        title,
        uri.getObjectName());

      if (response.result != 'successful') {
        Alert.alert('Ошибка', response.error, [{text: 'Ок'}]);
        return;
      }

      if (!uri.isUpload) {
        const isUpload = await objectStorageService.upload(uri as MinioBlob);

        if (!isUpload) {
          Alert.alert('Ошибка', 'Ошибка загрузки иконки', [{text: 'Ок'}]);
          return;
        }
      }

      navigation.goBack();
    }

    setReadonly(prev => !prev);
  };

  return (
    <GestureHandlerRootView>
      <View style={styles.container}>
        <View style={styles.controlsContainer}>
          <View style={{alignSelf: 'center'}}>
            <NavigateBackButton navigation={navigation} onGoBack={() => {}}/>
          </View>
          <Text style={styles.title}>Категория</Text>
          <View style={{alignSelf: 'center'}}>
            <TouchableOpacity
              disabled={(readonly && route.params.category.categoryId <= 7) || (!readonly && title == '')}
              onPress={save}>
              <Image
                style={[styles.editIcon, {
                  opacity: (readonly && route.params.category.categoryId > 7) || (!readonly && title != '') ? 1 : 0.5
                }]}
                source={readonly ? require('../assets/images/edit.png') : require('../assets/images/ok.png')}/>
            </TouchableOpacity>
          </View>
        </View>
        <View style={styles.iconContainer}>
          <Image
            source={{uri: uri.getUri()}}
            style={styles.icon}/>
        </View>
        <View style={styles.textButtonContainer}>
          <TextButton
            text={'Изменить иконку'}
            onPress={changeIconUri}/>
        </View>
        <View style={styles.infoContainer}>
          <Image
            style={{
              width: 20,
              height: 20,
              resizeMode: 'contain',
              alignSelf: 'center',
            }}
            source={require('../assets/images/warn.png')}/>
          <Text style={styles.info}>
            {'Иконки SVG/PNG на прозрачном фоне.\nЦвет заливки - белый. Стиль - Outline'}
          </Text>
        </View>
        <View style={{paddingHorizontal: 15}}>
          <TextInputTitle s={'Название'} top={40} bottom={5}/>
          <BorderedTextInput
            value={title}
            onChanged={setTitle}
            placeholder={'Введите название'}
            isError={false}
            isBig={false}
            keyboard={'default'}
            isReadonly={readonly}/>
        </View>
      </View>
    </GestureHandlerRootView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
  },
  controlsContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingHorizontal: 15,
    paddingTop: 30,
  },
  title: {
    color: 'black',
    fontWeight: '700',
    alignSelf: 'center',
    fontSize: 21,
  },
  editIcon: {
    width: 20,
    height: 20,
    resizeMode: 'contain',
  },
  iconContainer: {
    width: 100,
    height: 100,
    borderRadius: 50,
    backgroundColor: '#47A4F9',
    justifyContent: 'center',
    alignItems: 'center',
    alignSelf: 'center',
    marginTop: 30,
  },
  icon: {
    width: 40,
    height: 40,
    resizeMode: 'contain',
  },
  textButtonContainer: {
    alignItems: 'baseline',
    justifyContent: 'center',
    alignSelf: 'center',
    paddingTop: 30,
  },
  infoContainer: {
    paddingHorizontal: 15,
    paddingVertical: 10,
    shadowColor: 'black',
    shadowOpacity: 1,
    elevation: 5,
    shadowOffset: {
      width: 5,
      height: 5,
    },
    flexDirection: 'row',
    borderRadius: 15,
    backgroundColor: 'white',
    justifyContent: 'center',
    marginTop: 20,
    width: '75%',
    alignSelf: 'center',
  },
  info: {
    fontSize: 14,
    fontWeight: '400',
    color: '#6D7885',
    paddingLeft: 20,
  },
});
