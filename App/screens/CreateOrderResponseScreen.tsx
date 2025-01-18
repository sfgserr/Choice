import {Image, Text, TextInput, TouchableOpacity, View} from 'react-native';
import {CreateOrderResponseScreenProps} from '../types/NavigationTypes.ts';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import OrderRequestRadiusItem from '../components/listItems/OrderRequestRadiusItem.tsx';
import React from 'react';
import {CompanyOrderRequest} from '../types/DomainTypes.ts';
import {AuthContext} from '../App.tsx';
import TextInputTitle from '../components/TextInputTitle.tsx';
import BorderedTextInput from '../components/inputs/BorderedTextInput.tsx';
import Styles from '../constants/Styles.tsx';
import {StyledButton} from '../components/buttons/StyledButton.tsx';

export default function CreateOrderResponseScreen({route, navigation}: CreateOrderResponseScreenProps) {
  const { changeState } = React.useContext(AuthContext);

  const [orderRequest, setOrderRequest] = React.useState<CompanyOrderRequest | null>(null);

  const [price, setPrice] = React.useState<string>('');
  const [deadlinesIndex, setDeadlinesIndex] = React.useState(-1);

  const secondsInDay = 24 * 3600;
  const deadlines = [
    {
      title: 'День',
      seconds: secondsInDay
    },
    {
      title: 'Неделя',
      seconds: 7 * secondsInDay
    },
    {
      title: 'Месяц',
      seconds: 30 * secondsInDay
    },
    {
      title: '3 месяца',
      seconds: 90 * secondsInDay
    }
  ]

  React.useEffect(() => {
    async function getOrderRequest() {
      let response = await route.params.orderRequestService.getOrderRequestAsCompany(
        route.params.orderRequest.id,
        changeState);

      if (response.content != null) {
        setOrderRequest(response.content);
      }
      else {
        navigation.goBack();
      }
    }
    getOrderRequest();
  }, []);

  return (
    <View
      style={{
        flex: 1,
        backgroundColor: 'white',
        paddingHorizontal: 10
      }}>
      <View
        style={{
          flexDirection: 'row',
          justifyContent: 'flex-start',
          paddingTop: 20,
          alignItems: 'center',
        }}>
        <NavigateBackButton
          navigation={navigation}/>
      </View>
      <Text
        style={{
          position: 'absolute',
          alignSelf: 'center',
          top: 15,
          fontSize: 21,
          fontWeight: '700',
        }}>
        Ответить на заказ
      </Text>
      <View
        style={{
          paddingTop: 20
        }}>
        <OrderRequestRadiusItem
          orderRequest={route.params.orderRequest}
          categories={route.params.categories}
          navigation={navigation}
          preview/>
      </View>
      <Text
        style={{
          fontSize: 17,
          fontWeight: '700',
          color: 'black',
          paddingTop: 20,
        }}>
        Клиент хочет узнать:
      </Text>
      {orderRequest != null && (
        <>
          {orderRequest.toKnowPrice && (
            <>
              <TextInputTitle
                s={'Стоимость'}
                top={20}
                bottom={5}/>
              <BorderedTextInput
                value={price}
                onChanged={setPrice}
                placeholder={'Введите стоимость'}
                isError={false}
                isBig={false}
                keyboard={'number-pad'}/>
            </>)}
          {orderRequest.toKnowDeadline && (
            <>
              <TextInputTitle
                s={'Время выполнения работ'}
                top={20}
                bottom={5}/>
              <View
                style={{
                  ...Styles.borderedTextInputView,
                  ...Styles.borderedTextInputHeight,
                  ...Styles.borderedTextInputViewColor,
                  ...Styles.borderedTextInputUnfocused
                }}>
                <TextInput
                  value={deadlinesIndex == -1 ? '' : deadlines[deadlinesIndex].title}
                  placeholder={'Выберите время выполнения работ'}
                  style={Styles.borderedTextInput}
                  readOnly/>
                <TouchableOpacity
                  style={{
                    alignSelf: 'center',
                    paddingRight: 10
                  }}>
                  <Image
                    style={{
                      width: 15,
                      height: 15,
                      resizeMode: 'contain'
                    }}
                    source={require('../assets/images/chevron-down.png')}/>
                </TouchableOpacity>
              </View>
            </>)}
          {orderRequest.toKnowEnrollmentDate && (
            <>
              <View
                style={{
                  flexDirection: 'row',
                  justifyContent: 'space-between',
                  paddingTop: 20
                }}>
                <View style={{flex: 1, paddingRight: 3}}>
                  <TextInputTitle
                    s={'Дата записи'}
                    top={0}
                    bottom={5}/>
                  <View
                    style={{
                      ...Styles.borderedTextInputView,
                      ...Styles.borderedTextInputHeight,
                      ...Styles.borderedTextInputViewColor,
                      ...Styles.borderedTextInputUnfocused
                    }}>
                    <TextInput
                      value={deadlinesIndex == -1 ? '' : deadlines[deadlinesIndex].title}
                      placeholder={'Выберите дату записи'}
                      style={Styles.borderedTextInput}
                      readOnly/>
                    <TouchableOpacity
                      style={{
                        alignSelf: 'center',
                        paddingRight: 10
                      }}>
                      <Image
                        style={{
                          width: 15,
                          height: 15,
                          resizeMode: 'contain'
                        }}
                        source={require('../assets/images/chevron-down.png')}/>
                    </TouchableOpacity>
                  </View>
                </View>
                <View style={{flex: 1, paddingLeft: 3}}>
                  <TextInputTitle
                    s={'Время записи'}
                    top={0}
                    bottom={5}/>
                  <View
                    style={{
                      ...Styles.borderedTextInputView,
                      ...Styles.borderedTextInputHeight,
                      ...Styles.borderedTextInputViewColor,
                      ...Styles.borderedTextInputUnfocused
                    }}>
                    <TextInput
                      value={deadlinesIndex == -1 ? '' : deadlines[deadlinesIndex].title}
                      placeholder={'Выберите время записи'}
                      style={Styles.borderedTextInput}
                      readOnly/>
                    <TouchableOpacity
                      style={{
                        alignSelf: 'center',
                        paddingRight: 10
                      }}>
                      <Image
                        style={{
                          width: 15,
                          height: 15,
                          resizeMode: 'contain'
                        }}
                        source={require('../assets/images/chevron-down.png')}/>
                    </TouchableOpacity>
                  </View>
                </View>
              </View>
            </>)}
        </>)}
      <StyledButton
        content={'Ответить'}
        top={20}
        bottom={5}
        isDisabled={false}
        pressed={() => {}}/>
    </View>
  )
}
