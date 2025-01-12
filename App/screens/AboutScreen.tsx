import {
  Image, ScrollView,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import React from 'react';
import {AboutScreenProps} from '../types/NavigationTypes.ts';
import TextInputTitle from '../components/TextInputTitle.tsx';
import Styles from '../constants/Styles.tsx';
import ImageBox from '../components/ImageBox.tsx';
import {launchImageLibrary} from 'react-native-image-picker';
import {StyledButton} from '../components/buttons/StyledButton.tsx';
import BorderedTextInput from '../components/inputs/BorderedTextInput.tsx';

export default function AboutScreen({next, onChevronPressed, categoriesTitle}: AboutScreenProps) {
  const [photoUris, setPhotoUris] = React.useState<string[]>(['', '', '', '', '', '']);
  const [prepaymentAvailable, setPrepaymentAvailable] = React.useState(false);
  const [description, setDescription] = React.useState('');

  const onImageBoxPressed = async (index: number) => {
    let response = await launchImageLibrary({mediaType: 'photo'});

    setPhotoUris(prev => {
      if (response.assets == undefined)
        return prev;

      prev[index] = response.assets[0].uri;
      return [...prev];
    })
  };

  const onRemoveImagePressed = (index: number) => {
    setPhotoUris(prev => {
      prev[index] = '';
      return [...prev];
    });
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

  const onOptionPressed = () => setPrepaymentAvailable(prev => !prev);

  return (
    <ScrollView
      style={styles.container}
      showsVerticalScrollIndicator={false}>
      <Text style={styles.title}>О работе</Text>
      <TextInputTitle s={'Категория услуг'} top={20} bottom={5} />
      <View style={styles.borderedInput}>
        <TextInput
          style={Styles.borderedTextInput}
          value={categoriesTitle == '' ? 'Выбрать деятельность' : categoriesTitle}
          readOnly/>
        <TouchableOpacity style={styles.chevronDown}
          onPress={onChevronPressed}>
          <Image
            style={styles.image}
            source={require('../assets/images/chevron-down.png')}
          />
        </TouchableOpacity>
      </View>
      <TextInputTitle
        s={'Описание'}
        top={20}
        bottom={5}/>
      <BorderedTextInput
        value={description}
        onChanged={setDescription}
        placeholder={'Введите описание'}
        isError={false}
        isBig={true}/>
      <TextInputTitle
        s={'Добавьте фотографии'}
        top={20}
        bottom={10}/>
      <View style={styles.photoUrisContainer}>
        {photoUris.map((u, i) => (
          <ImageBox
            key={i}
            uri={photoUris[i]}
            onPress={async () => await onImageBoxPressed(i)}
            onRemovePress={() => onRemoveImagePressed(i)}
          />
        ))}
      </View>
      <TextInputTitle
        s={'Опции'}
        top={20}
        bottom={10}/>
      <Option
        selected={prepaymentAvailable}
        title={'Работа с предоплатой'}
        onPress={onOptionPressed}
        top={0}/>
      <Option
        selected={!prepaymentAvailable}
        title={'Работа без предоплатой'}
        onPress={onOptionPressed}
        top={10}/>
      <StyledButton
        content={'Сохранить'}
        top={20}
        bottom={10}
        isDisabled={photoUris.every(s => s == '') || categoriesTitle == '' || description == ''}
        pressed={() => next(description, photoUris, prepaymentAvailable)}/>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
    paddingTop: 10,
  },
  title: {
    fontWeight: '700',
    fontSize: 17,
    color: 'black',
  },
  borderedInput: {
    ...Styles.borderedTextInputView,
    ...Styles.borderedTextInputHeight,
    ...Styles.borderedTextInputViewColor,
    ...Styles.borderedTextInputUnfocused,
  },
  chevronDown: {
    alignSelf: 'center',
    paddingRight: 10
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
    rowGap: 10,
  },
  optionContainer: {
    flexDirection: 'row'
  },
  optionButton: {
    alignSelf: 'center',
    width: 20,
    height: 20,
    borderRadius: 10,
    borderWidth: 2,
    justifyContent: 'center'
  },
  optionSelected: {
    alignSelf: 'center',
    width: 12,
    height: 12,
    borderRadius: 6,
    backgroundColor: '#2688EB'
  },
  optionTitle: {
    color: 'black',
    fontWeight: '400',
    fontSize: 15,
    alignSelf: 'center',
    paddingLeft: 10
  }
});
