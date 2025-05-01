import {ActivityIndicator, DeviceEventEmitter, Image, StyleSheet, Text} from 'react-native';
import {
  View,
  Dimensions
} from 'react-native';
import YaMap, {Animation, Point} from 'react-native-yamap';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {MapScreenProps} from '../types/NavigationTypes.ts';
import {StyledButton} from '../components/buttons/StyledButton.tsx';
import React from 'react';
import {CompanyMapMarker, Message, OrderResponse} from '../types/DomainTypes.ts';
import {OrderRequest} from '../types/DomainTypes.ts';
import OrderRequestModal from '../components/modals/OrderRequestModal.tsx';
import CustomMarker from '../components/CustomMarker.tsx';
import LongRunningOperationIndicator from '../components/LongRunningOperationIndicator.tsx';
import {useDependency} from '../services/Hooks.ts';
import {CompanyService} from '../services/domain/CompanyService.ts';
import CompanyPageBottomSheet from '../components/bottomSheets/CompanyPageBottomSheet.tsx';
import BottomSheet from '@gorhom/bottom-sheet';
import {GestureHandlerRootView, Pressable} from 'react-native-gesture-handler';
import {OrderResponseService} from '../services/domain/OrderResponseService.ts';
import {UserService} from '../services/domain/UserService.ts';
import {DateUtils} from '../utils/DateUtils.ts';
import TextButton from '../components/buttons/TextButton.tsx';

const d = Dimensions.get('screen');

const OrderResponseCard = ({responseId, company, hide, details}: {
  responseId: string,
  company: CompanyMapMarker,
  hide: () => void,
  details: (companyId: string, responseId: string) => void}) => {
  const orderResponseService = useDependency<OrderResponseService>('OrderResponseService');

  const [order, setOrder] = React.useState<OrderResponse | null>(null);

  const info = React.useMemo(() => {
    if (order != null) {
      return [
        {
          value: `${order.price} рублей`,
          title: 'Стоимость',
          icon: require('../assets/images/rub.png'),
          color: 'black',
          crossOut: false,
          display: order.price > 0,
        },
        {
          value: DateUtils.secondsToDate(order.deadline),
          title: 'Время выполнения работы',
          icon: require('../assets/images/deadline.png'),
          color: 'black',
          crossOut: false,
          display: order.deadline > 0,
        },
        {
          value: order.enrollmentDate != null ? DateUtils.formatDate(order.enrollmentDate as Date) : null,
          title:'Дата и время записи',
          icon: require('../assets/images/enrollment.png'),
          color: 'black',
          crossOut: false,
          display: order.enrollmentDate != null,
        },
        {
          value: `${order.prepayment} рублей`,
          title: 'Предоплата',
          icon: require('../assets/images/prepayment.png'),
          color: 'black',
          crossOut: false,
          display: order.prepayment > 0,
        },
      ];
    }

    return [];
  }, [order]);

  React.useEffect(() => {
    const getOrder = async () => {
      const response = await orderResponseService.get(responseId);

      if (response.result == 'successful') {
        setOrder(response.content);
      }
    };

    getOrder();
  }, [responseId]);

  return (
    <View
      style={{
        width: '90%',
        position: 'absolute',
        backgroundColor: 'white',
        borderRadius: 15,
        paddingVertical: 10,
        top: d.height * 0.086 + 10,
        alignSelf: 'center',
        paddingHorizontal: 10,
      }}>
      {order == null ? (
        <ActivityIndicator size={'large'} color={'#2D81E0'} />
      ) : (
        <>
          <View
            style={{
              flexDirection: 'row',
              justifyContent: 'space-between',
              alignItems: 'center',
            }}>
            <View
              style={{
                flexDirection: 'row',
                alignItems: 'center',
              }}>
              <Text
                style={{
                  fontSize: 15,
                  fontWeight: '500',
                  color: '#979797',
                  paddingLeft: 5,
                }}>
                {`⭐ ${company.averageGrade}`}
              </Text>
            </View>
            <Pressable onPress={hide}>
              <View style={{width: 8, height: 2, backgroundColor: '#979797'}}/>
            </Pressable>
          </View>
          <View style={{paddingTop: 10}}>
            {info.map((i, key) => (
              <>
                {i.display && (
                  <View style={{paddingTop: 10}} key={key}>
                    <View
                      style={{
                        flexDirection: 'row',
                        paddingLeft: 5,
                        alignItems: 'center',
                        justifyContent: 'space-between',
                      }}>
                      <View
                        style={{flexDirection: 'row', alignItems: 'center'}}>
                        <Image
                          source={i.icon}
                          style={{
                            width: 15,
                            height: 15,
                            resizeMode: 'contain',
                          }}
                          tintColor={i.color}
                        />
                        <Text
                          style={[
                            {
                              fontSize: 14,
                              fontWeight: '400',
                              color: '#ADCBEB',
                              paddingLeft: 5,
                            },
                            {
                              color: i.color,
                            },
                          ]}>
                          {i.title}
                        </Text>
                      </View>
                      <Text
                        style={[
                          {
                            color: 'black',
                            fontWeight: '500',
                            fontSize: 14,
                          },
                        ]}>
                        {`${i.value}`}
                      </Text>
                      {i.crossOut && (
                        <View
                          style={{
                            position: 'absolute',
                            width: '100%',
                            height: 1,
                            backgroundColor: 'black',
                          }}
                        />
                      )}
                    </View>
                  </View>
                )}
              </>
            ))}
          </View>
          <View style={{alignSelf: 'flex-start', paddingLeft: 10, paddingTop: 5}}>
            <TextButton text={'Подробнее'} onPress={() => details(company.id, responseId)}/>
          </View>
        </>
      )}
    </View>
  );
}

export default function MapScreen({route, navigation}: MapScreenProps) {
  const companyService = useDependency<CompanyService>('CompanyService');
  const userService = useDependency<UserService>('UserService');

  const map = React.createRef<YaMap>();

  const [companies, setCompanies] = React.useState<{marker: CompanyMapMarker, responseId: string, index: number}[]>([]);
  const [orderRequest, setOrderRequest] = React.useState<OrderRequest | null>(null);
  const [isToggled, setIsToggled] = React.useState(false);

  const [companyId, setCompanyId] = React.useState('');
  const [responseId, setResponseId] = React.useState('');

  const [responsedCompany, setResponsedCompany] = React.useState<CompanyMapMarker>(null);
  const [lastResponse, setLastResponse] = React.useState<string>('');

  React.useEffect(() => {
    async function getCompanies() {
      let companies = await companyService.getCompanies(
        route.params.categories[route.params.categoryId].categoryId);
      if (companies.result == 'successful') {
        const user = await userService.getUser();
        const userMap = companies.content.find(c => c.id == user.id);

        map.current?.setCenter(
          {lat: +userMap.latitude, lon: +userMap.longitude},
          15,
          Animation.SMOOTH);

        setCompanies(companies.content.map((c, index) => {
          const a = {marker: c, responseId: '', index};

          return a;
        }));
      }
    }
    getCompanies();
  }, []);

  React.useEffect(() => {
    const moveMarkers = (message: Message) => {
      if (orderRequest != null && message.enrollmentDate == null && message.type == 'Order') {
        setCompanies(prev => {
          const index = prev.findIndex(c => c.marker.id == message.fromUserId);
          if (index === -1) return prev;

          prev[index].responseId = message.orderResponseId!;
          prev[index].index++;

          setResponsedCompany(prev[index].marker);
          setLastResponse(prev[index].responseId);

          const responsedCompanies: Point[] = prev.filter(c => c.responseId !== '')
            .map(c => ({
              lat: +c.marker.latitude,
              lon: +c.marker.longitude,
            }));

          if (responsedCompanies.length == 1) {
            map.current?.setCenter(responsedCompanies[0], 15, 0, 0, 0.5, Animation.SMOOTH);
          } else {
            map.current?.fitMarkers(responsedCompanies);
          }

          return [...prev];
        });
      }
    };

    DeviceEventEmitter.addListener('messageSent', moveMarkers);

    return () => DeviceEventEmitter.removeAllListeners('messageSent');
  }, [orderRequest, map]);

  const onOrderRequestCreated = async (orderRequest: OrderRequest) => {
    setOrderRequest(orderRequest);
    await new Promise(f => setTimeout(f, 1000));
    setIsToggled(true);
  };

  const onCreateOrderRequestButtonPressed = async () => {
    navigation.navigate('CreateOrderRequest', {
      categories: route.params.categories,
      categoryIndex: route.params.categoryId,
      onGoBack: onOrderRequestCreated,
    });
  };

  const ref = React.useRef<BottomSheet>(null);

  const onMarkerPressed = React.useCallback((companyId: string, responseId: string) => {
    setCompanyId(companyId);
    setResponseId(responseId);
    setIsToggled(false);
    ref.current?.expand();
  }, []);

  const onClose = React.useCallback(() => {
    ref.current?.close();
    setCompanyId('');
  }, []);

  const onGoBack = React.useCallback(() => {}, []);

  return (
    <GestureHandlerRootView>
      <YaMap
        ref={map}
        initialRegion={{
          lat: 50,
          lon: 50,
          zoom: 8,
          tilt: 100,
        }}
        style={styles.map}>
        {companies.length > 0 && (
          <>
            {companies.map((company, index) => {
              return (
                <>
                  <CustomMarker
                    company={company}
                    key={index}
                    onPress={onMarkerPressed}/>
                </>
              )
            })}
          </>
        )}
      </YaMap>
      <View style={styles.topTab}>
        <View style={styles.navigateBackButtonContainer}>
          <NavigateBackButton navigation={navigation} />
        </View>
        <Text style={styles.categoryTitleContainer}>{route.params.categories[route.params.categoryId].title}</Text>
      </View>
      {lastResponse != '' && (
        <OrderResponseCard
          responseId={lastResponse}
          company={responsedCompany}
          hide={() => setLastResponse('')}
          details={onMarkerPressed}/>
      )}
      {orderRequest == null ? (
        <>
          <View style={styles.bottomTab}>
            <View style={styles.buttonContainer}>
              <StyledButton
                content={'Создать заказ'}
                top={10}
                bottom={0}
                isDisabled={false}
                pressed={onCreateOrderRequestButtonPressed}/>
            </View>
          </View>
        </>) : (
          <>
            <OrderRequestModal
              isToggled={isToggled}
              orderRequest={orderRequest}
              navigation={navigation}/>
            <LongRunningOperationIndicator isRefreshing={false}/>
          </>)}
      <CompanyPageBottomSheet
        companyId={companyId}
        responseId={responseId}
        close={onClose}
        ref={ref}
        navigateToChat={() => navigation.navigate('Chat', {id: companyId, onGoBack})}/>
    </GestureHandlerRootView>
  );
}

const styles = StyleSheet.create({
  map: {
    flex: 1
  },
  topTab: {
    position: 'absolute',
    height: d.height * 0.086,
    width: '100%',
    backgroundColor: 'white',
    top: 0,
    justifyContent: 'center',
    alignItems: 'baseline',
  },
  navigateBackButtonContainer: {
    flexDirection: 'row'
  },
  categoryTitleContainer: {
    color: 'black',
    fontSize: 21,
    fontWeight: '600',
    alignSelf: 'center',
    position: 'absolute',
  },
  bottomTab: {
    position: 'absolute',
    height: d.height * 0.086,
    width: '100%',
    backgroundColor: 'white',
    bottom: 0,
    justifyContent: 'center',
    paddingHorizontal: 10
  },
  buttonContainer: {
    flex: 1
  },
});
