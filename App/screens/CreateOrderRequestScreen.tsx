import React from 'react';
import {
  Text,
  View,
  Dimensions,
  TextInput,
  TouchableOpacity,
  Image, ScrollView,
} from 'react-native';
import NavigateBackButton from '../components/NavigateBackButton.tsx';
import {CreateOrderRequestScreenProps} from '../types/NavigationTypes.ts';
import TextInputTitle from '../components/TextInputTitle.tsx';
import Styles from '../constants/Styles.tsx';
import {Category} from '../types/DomainTypes.ts';
import Checkbox from '../components/Checkbox.tsx';
import ImageBox from '../components/ImageBox.tsx';
import {launchImageLibrary} from 'react-native-image-picker';
import {Slider} from '@miblanchard/react-native-slider';
import {StyledButton} from '../components/StyledButton.tsx';
import {useSharedValue} from 'react-native-reanimated';
import BottomSheet from '../components/BottomSheet.tsx';

export default function CreateOrderRequestScreen({route, navigation}: CreateOrderRequestScreenProps) {
  const d = Dimensions.get('screen');
  const [categories, setCategories] = React.useState<Category[]>(route.params.categories);
  const [description, setDescription] = React.useState('');
  const [toKnowPrice, setToKnowPrice] = React.useState(false);
  const [toKnowDeadline, setToKnowDeadline] = React.useState(false);
  const [toKnowEnrollmentDate, setToKnowEnrollmentDate] = React.useState(false);
  const [photos, setPhotos] = React.useState<string[]>(['', '', '']);
  const [radius, setRadius] = React.useState<number>(5);

  const isOpen = useSharedValue(false);

  const toggleSheet = () => {
    isOpen.value = !isOpen.value;
  }

  const onImageBoxPressed = async (index: number) => {
    let response = await launchImageLibrary({mediaType: 'photo'});

    setPhotos(prev => {
      if (response.assets == undefined)
        return prev;

      prev[index] = response.assets[0].uri;
      return [...prev];
    })
  };

  const onRemoveImagePressed = (index: number) => {
    setPhotos(prev => {
      prev[index] = '';
      return [...prev];
    });
  }

  const data= [{
      title: 'Узнать стоимость',
      checked: toKnowPrice,
      pressed: () => setToKnowPrice(p => !p)
    }, {
      title: 'Узнать время выполнения работ',
      checked: toKnowDeadline,
      pressed: () => setToKnowDeadline(p => !p)
    }, {
      title: 'Узнать время записи',
      checked: toKnowEnrollmentDate,
      pressed: () => setToKnowEnrollmentDate(p => !p)
    },
  ];

  return (
    <ScrollView
      style={{
        flex: 1,
        backgroundColor: 'white',
      }}
      showsVerticalScrollIndicator={false}>
      <View
        style={{
          height: d.height * 0.086,
          width: '100%',
          backgroundColor: 'white',
          justifyContent: 'center',
          alignItems: 'baseline',
        }}>
        <View style={{flexDirection: 'row'}}>
          <NavigateBackButton navigation={navigation} />
        </View>
        <Text
          style={{
            color: 'black',
            fontSize: 21,
            fontWeight: '600',
            alignSelf: 'center',
            position: 'absolute',
          }}>
          Создание заказа
        </Text>
      </View>
      <View
        style={{
          flex: 1,
          paddingHorizontal: 15,
        }}>
        <TextInputTitle s={'Категория услуг'} top={20} bottom={5} />
        <View
          style={[
            Styles.borderedTextInputView,
            Styles.borderedTextInputHeight,
            Styles.borderedTextInputViewColor,
            Styles.borderedTextInputUnfocused,
          ]}>
          <TextInput
            style={Styles.borderedTextInput}
            value={route.params.categories[route.params.categoryIndex].title}
            readOnly
          />
          <TouchableOpacity
            style={{alignSelf: 'center', paddingRight: 10}}
            onPress={toggleSheet}>
            <Image
              style={{
                resizeMode: 'contain',
                width: 15,
                height: 15,
              }}
              source={require('../assets/images/chevron-down.png')}
            />
          </TouchableOpacity>
        </View>
        <TextInputTitle s={'Описание задачи'} top={20} bottom={5} />
        <View
          style={[
            Styles.borderedTextInputView,
            Styles.borderedTextInputBigHeight,
            Styles.borderedTextInputViewColor,
            Styles.borderedTextInputUnfocused,
          ]}>
          <TextInput
            style={Styles.borderedTextInput}
            value={description}
            placeholder={
              'Введите подробности задачи, в чем вам нужна помощь и какой вы ожидаете результат'
            }
            onChangeText={setDescription}
            multiline
          />
        </View>
        <View
          style={{
            paddingTop: 10,
          }}>
          <TouchableOpacity
            style={[
              Styles.borderedTextInputView,
              Styles.borderedTextInputViewColor,
              Styles.borderedTextInputHeight,
            ]}>
            <View
              style={{
                flexDirection: 'row',
                justifyContent: 'center',
              }}>
              <Image
                source={require('../assets/images/micro.png')}
                style={{
                  resizeMode: 'contain',
                  width: 20,
                  height: 20,
                  alignSelf: 'center',
                }}
              />
              <Text
                style={{
                  color: '#2688EB',
                  fontWeight: '500',
                  fontSize: 17,
                  alignSelf: 'center',
                }}>
                Записать голосом
              </Text>
            </View>
          </TouchableOpacity>
        </View>
        <TextInputTitle s={'Что узнать у продавца'} top={20} bottom={5} />
        {data.map((item, index) => {
          return (
            <View
              key={index}
              style={{
                flexDirection: 'row',
                paddingTop: 10
              }}>
              <Checkbox
                checked={item.checked}
                pressed={item.pressed}
              />
              <Text
                style={{
                  alignSelf: 'center',
                  fontWeight: '400',
                  fontSize: 15,
                  paddingLeft: 10,
                }}>
                {item.title}
              </Text>
            </View>
          )
        })}
        <TextInputTitle
          s={'Приложите файлы или фото к заказу'}
          top={20}
          bottom={5}/>
        <View
          style={{
            flexDirection: 'row',
            justifyContent: 'space-between'
          }}>
          <ImageBox
            uri={photos[0]}
            onPress={async () => await onImageBoxPressed(0)}
            onRemovePress={() => onRemoveImagePressed(0)}/>
          <ImageBox
            uri={photos[1]}
            onPress={async () => await onImageBoxPressed(1)}
            onRemovePress={() => onRemoveImagePressed(1)}/>
          <ImageBox
            uri={photos[2]}
            onPress={async () => await onImageBoxPressed(2)}
            onRemovePress={() => onRemoveImagePressed(2)}/>
        </View>
        <View
          style={{
            flexDirection: 'row',
            paddingTop: 20,
            justifyContent: 'space-between'
          }}>
          <Text
            style={Styles.title}>
            Радиус поиска
          </Text>
          <Text
            style={{
              fontWeight: '600',
              fontSize: 14,
              color: 'black'
            }}>
            {`${Math.floor(radius)} км`}
          </Text>
        </View>
        <View style={{paddingTop: 10}}>
          <Slider
            minimumValue={5}
            maximumValue={25}
            value={radius}
            onValueChange={(value) => setRadius(value[0])}
            thumbTintColor={'white'}
            minimumTrackTintColor={'#007AFF'}
            maximumTrackTintColor={'#e4e4e6'}
            thumbStyle={{
              shadowColor: 'red',
              elevation: 1,
              shadowRadius: 50,
              shadowOpacity: 0.5,
              shadowOffset: {
                width: 0,
                height: 2
              }
            }}/>
        </View>
        <View
          style={{
            flexDirection: 'row',
            justifyContent: 'space-between',
            paddingTop: 10
          }}>
          <Text style={Styles.title}>от 5 км</Text>
          <Text style={Styles.title}>до 25 км</Text>
        </View>
      </View>
      <View style={{paddingHorizontal: 15, paddingTop: 30, paddingBottom: 5}}>
        <StyledButton
          content={'Создать заказ'}
          top={0}
          bottom={0}
          isDisabled={false}
          pressed={() => {}}/>
      </View>
      <BottomSheet
        isOpen={isOpen}
        toggleSheet={toggleSheet}/>
    </ScrollView>
  );
}
