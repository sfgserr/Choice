import {DeviceEventEmitter, StyleSheet, Text} from 'react-native';
import {
  View,
  Dimensions
} from 'react-native';
import YaMap, {Animation} from 'react-native-yamap';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {MapScreenProps} from '../types/NavigationTypes.ts';
import {StyledButton} from '../components/buttons/StyledButton.tsx';
import React from 'react';
import {CompanyMapMarker, Message} from '../types/DomainTypes.ts';
import {OrderRequest} from '../types/DomainTypes.ts';
import OrderRequestModal from '../components/modals/OrderRequestModal.tsx';
import CustomMarker from '../components/CustomMarker.tsx';
import LongRunningOperationIndicator from '../components/LongRunningOperationIndicator.tsx';
import {useDependency} from '../services/Hooks.ts';
import {CompanyService} from '../services/domain/CompanyService.ts';
import CompanyPageBottomSheet from '../components/bottomSheets/CompanyPageBottomSheet.tsx';
import BottomSheet from '@gorhom/bottom-sheet';
import {GestureHandlerRootView} from 'react-native-gesture-handler';
import {UserService} from '../services/domain/UserService.ts';

const d = Dimensions.get('screen');

export default function MapScreen({route, navigation}: MapScreenProps) {
  const companyService = useDependency<CompanyService>('CompanyService');
  const userService = useDependency<UserService>('UserService');

  const map = React.createRef<YaMap>();

  const [companies, setCompanies] = React.useState<{marker: CompanyMapMarker, responseId: string}[]>([]);
  const [orderRequest, setOrderRequest] = React.useState<OrderRequest>(null);
  const [isToggled, setIsToggled] = React.useState(false);

  const [companyId, setCompanyId] = React.useState('');

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
        setCompanies(companies.content.map(c => ({marker: c, responseId: ''})));
      }
    }
    getCompanies();
  }, []);

  React.useEffect(() => {
    DeviceEventEmitter.addListener('messageSent', (message: Message) => {
      console.log(message);
      if (orderRequest != null && message.enrollmentDate == null && message.type == 'Order') {
        setCompanies(prev => {
          const index = prev.findIndex(c => c.marker.id == message.fromUserId);

          prev[index].responseId = message.orderResponseId!;

          map.current?.fitMarkers(prev.filter(c => c.responseId != '').map(c => ({
            lat: +c.marker.latitude,
            lon: +c.marker.longitude,
          })));

          return [...prev];
        });
      }
    });

    return () => DeviceEventEmitter.removeAllListeners('messageSent');
  }, [orderRequest]);

  const onOrderRequestCreated = async (orderRequest: OrderRequest) => {
    setOrderRequest(orderRequest);
    await new Promise(f => setTimeout(f, 1000));
    setIsToggled(true);
  };

  const onCreateOrderRequestButtonPressed = async () => {
    navigation.navigate('CreateOrderRequest', {
      categories: route.params.categories,
      categoryIndex: route.params.categoryId,
      onGoBack: onOrderRequestCreated
    });
  };

  const ref = React.useRef<BottomSheet>(null);

  const onMarkerPressed = React.useCallback((companyId: string) => {
    setCompanyId(companyId);
    ref.current?.expand();
  }, []);

  const onClose = React.useCallback(() => {
    ref.current?.close();
    setCompanyId('');
  }, []);

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
                <CustomMarker
                  company={company}
                  index={index}
                  key={index}
                  onPress={onMarkerPressed}/>
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
        close={onClose}
        ref={ref}
        navigateToChat={() => navigation.navigate('Chat', {id: companyId})}/>
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
