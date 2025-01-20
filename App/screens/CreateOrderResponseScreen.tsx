import {
  Image,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import {CreateOrderResponseScreenProps} from '../types/NavigationTypes.ts';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import OrderRequestRadiusItem from '../components/listItems/OrderRequestRadiusItem.tsx';
import React from 'react';
import {CompanyOrderRequest} from '../types/DomainTypes.ts';
import TextInputTitle from '../components/TextInputTitle.tsx';
import BorderedTextInput from '../components/inputs/BorderedTextInput.tsx';
import Styles from '../constants/Styles.tsx';
import {StyledButton} from '../components/buttons/StyledButton.tsx';
import {useDependency} from '../services/Hooks.ts';
import {OrderRequestService} from '../services/domain/OrderRequestService.ts';

export default function CreateOrderResponseScreen({route, navigation}: CreateOrderResponseScreenProps) {
  const orderRequestService = useDependency<OrderRequestService>('OrderRequestService');

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
      let response = await orderRequestService.getOrderRequestAsCompany(
        route.params.orderRequest.id);

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
    <View style={styles.container}>
      <View style={styles.navigateBackButtonContainer}>
        <NavigateBackButton
          navigation={navigation}/>
      </View>
      <Text style={styles.title}>Ответить на заказ</Text>
      <View style={styles.orderRequestItemContainer}>
        <OrderRequestRadiusItem
          orderRequest={route.params.orderRequest}
          categories={route.params.categories}
          navigation={navigation}
          preview/>
      </View>
      <Text style={styles.subTitle}>
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
              <View style={styles.borderedTextInput}>
                <TextInput
                  value={deadlinesIndex == -1 ? '' : deadlines[deadlinesIndex].title}
                  placeholder={'Выберите время выполнения работ'}
                  style={Styles.borderedTextInput}
                  readOnly/>
                <TouchableOpacity style={styles.chevronDownButton}>
                  <Image
                    style={styles.image}
                    source={require('../assets/images/chevron-down.png')}/>
                </TouchableOpacity>
              </View>
            </>)}
          {orderRequest.toKnowEnrollmentDate && (
            <>
              <View
                style={styles.horizontalSpread}>
                <View style={styles.dateInputContainer}>
                  <TextInputTitle
                    s={'Дата записи'}
                    top={0}
                    bottom={5}/>
                  <View style={styles.borderedTextInput}>
                    <TextInput
                      value={deadlinesIndex == -1 ? '' : deadlines[deadlinesIndex].title}
                      placeholder={'Выберите дату записи'}
                      style={Styles.borderedTextInput}
                      readOnly/>
                    <TouchableOpacity style={styles.chevronDownButton}>
                      <Image
                        style={styles.image}
                        source={require('../assets/images/chevron-down.png')}/>
                    </TouchableOpacity>
                  </View>
                </View>
                <View style={styles.timeInputContainer}>
                  <TextInputTitle
                    s={'Время записи'}
                    top={0}
                    bottom={5}/>
                  <View style={styles.borderedTextInput}>
                    <TextInput
                      value={deadlinesIndex == -1 ? '' : deadlines[deadlinesIndex].title}
                      placeholder={'Выберите время записи'}
                      style={Styles.borderedTextInput}
                      readOnly/>
                    <TouchableOpacity style={styles.chevronDownButton}>
                      <Image
                        style={styles.image}
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

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
    paddingHorizontal: 10
  },
  navigateBackButtonContainer: {
    flexDirection: 'row',
    justifyContent: 'flex-start',
    paddingTop: 20,
    alignItems: 'center',
  },
  title: {
    position: 'absolute',
    alignSelf: 'center',
    top: 15,
    fontSize: 21,
    fontWeight: '700',
  },
  orderRequestItemContainer: {
    paddingTop: 20
  },
  subTitle: {
    fontSize: 17,
    fontWeight: '700',
    color: 'black',
    paddingTop: 20,
  },
  borderedTextInput: {
    ...Styles.borderedTextInputView,
    ...Styles.borderedTextInputHeight,
    ...Styles.borderedTextInputViewColor,
    ...Styles.borderedTextInputUnfocused
  },
  chevronDownButton: {
    alignSelf: 'center',
    paddingRight: 10
  },
  image: {
    width: 15,
    height: 15,
    resizeMode: 'contain'
  },
  horizontalSpread: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingTop: 20
  },
  dateInputContainer: {
    flex: 1,
    paddingRight: 3
  },
  timeInputContainer: {
    flex: 1,
    paddingLeft: 3
  }
});
