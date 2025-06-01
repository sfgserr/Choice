import {Alert, Image, KeyboardAvoidingView, StyleSheet, Text, View} from 'react-native';
import React from 'react';
import {CreateCategoryScreenProps} from '../types/NavigationTypes.ts';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {ImageBoxObject, MinioBlob} from '../components/ImageBox.tsx';
import TextButton from '../components/buttons/TextButton.tsx';
import TextInputTitle from '../components/TextInputTitle.tsx';
import BorderedTextInput from '../components/inputs/BorderedTextInput.tsx';
import {launchImageLibrary} from 'react-native-image-picker';
import {StyledButton} from '../components/buttons/StyledButton.tsx';
import {useDependency} from '../services/Hooks.ts';
import {FileValidationService} from '../services/object/FileValidationService.ts';
import {CategoryService} from '../services/domain/CategoryService.ts';
import {ObjectStorageService} from '../services/object/ObjectStorageService.ts';
import {GestureHandlerRootView} from "react-native-gesture-handler";
import {SafeAreaView} from "react-native-safe-area-context";

export default function CreateCategoryScreen({route, navigation}: CreateCategoryScreenProps) {
  const fileValidationService = useDependency<FileValidationService>('FileValidationService');
  const categoryService = useDependency<CategoryService>('CategoryService');
  const objectStorageService = useDependency<ObjectStorageService>('ObjectStorageService');

  const [uri, setUri] = React.useState<ImageBoxObject>(MinioBlob.createDefault());
  const [title, setTitle] = React.useState<string>('');

  const changeIconUri = React.useCallback(async () => {
    const response = await launchImageLibrary({mediaType: 'photo'});

    if (response.assets != undefined && response.assets.length > 0) {
      const validationResponse = await fileValidationService.getContentAndValidate(response.assets[0].uri!);

      if (validationResponse.object != null) {
        setUri(validationResponse.object);
      }
    }
  }, []);

  const isDisable = () => {
    return uri.getObjectName() == '' || title == '';
  };

  const createCategory = async () => {
    const response = await categoryService.createCategory(title, uri.getObjectName());

    let errorMsg = response.error;

    if (response.result == 'successful') {
      const isUpload = await objectStorageService.upload(uri as MinioBlob);

      if (isUpload) {
        navigation.goBack();
        return;
      }

      errorMsg = 'Ошибка загрузки иконки';
    }

    Alert.alert('Ошибка', errorMsg, [{text: 'Ок'}]);
  };

  return (
    <GestureHandlerRootView>
      <SafeAreaView style={{flex: 1}}>
        <View style={styles.container}>
          <View style={styles.controlsContainer}>
            <View style={{alignSelf: 'center'}}>
              <NavigateBackButton navigation={navigation} onGoBack={() => {}}/>
            </View>
          </View>
          <Text style={styles.title}>Категория</Text>
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
              isReadonly={false}/>
          </View>
          <KeyboardAvoidingView
            style={{
              position: 'absolute',
              bottom: 20,
              width: '90%',
              alignSelf: 'center',
            }}
            behavior={'height'}>
            <StyledButton
              content={'Создать категорию'}
              top={0}
              bottom={0}
              isDisabled={isDisable()}
              pressed={createCategory}
              type={'default'}/>
          </KeyboardAvoidingView>
        </View>
      </SafeAreaView>
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
    position: 'absolute',
    top: 25,
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
