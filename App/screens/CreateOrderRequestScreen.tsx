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

export default function CreateOrderRequestScreen({route, navigation}: CreateOrderRequestScreenProps) {
  const d = Dimensions.get('screen');
  const [categories, setCategories] = React.useState<Category[]>(route.params.categories);
  const [description, setDescription] = React.useState('');
  const [toKnowPrice, setToKnowPrice] = React.useState(false);
  const [toKnowDeadline, setToKnowDeadline] = React.useState(false);
  const [toKnowEnrollmentDate, setToKnowEnrollmentDate] = React.useState(false);

  const data= [
    {
      title: 'Узнать стоимость',
      checked: toKnowPrice,
      pressed: () => setToKnowPrice(p => !p)
    },
    {
      title: 'Узнать время выполнения работ',
      checked: toKnowDeadline,
      pressed: () => setToKnowDeadline(p => !p)
    },
    {
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
      }}>
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
          <TouchableOpacity style={{alignSelf: 'center', paddingRight: 10}}>
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
          <ImageBox/>
          <ImageBox/>
          <ImageBox/>
        </View>
      </View>
    </ScrollView>
  );
}
