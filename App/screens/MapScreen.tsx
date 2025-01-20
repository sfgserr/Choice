import {StyleSheet, Text} from 'react-native';
import {
  View,
  Dimensions
} from 'react-native';
import YaMap, {Animation} from 'react-native-yamap';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {MapScreenProps} from '../types/NavigationTypes.ts';
import {StyledButton} from '../components/buttons/StyledButton.tsx';
import React from 'react';
import {AuthContext} from '../AuthorizedContextProvider.tsx';
import {CompanyMapMarker} from '../types/DomainTypes.ts';
import {OrderRequest} from '../types/DomainTypes.ts';
import OrderRequestModal from '../components/modals/OrderRequestModal.tsx';
import CustomMarker from '../components/CustomMarker.tsx';
import LongRunningOperationIndicator from '../components/LongRunningOperationIndicator.tsx';
import {useDependency} from '../stores/DependencyInjection.ts';
import {CompanyService} from '../services/domain/CompanyService.ts';

const d = Dimensions.get('screen');

export default function MapScreen({route, navigation}: MapScreenProps) {
  const { changeState } = React.useContext(AuthContext);

  const companyService = useDependency<CompanyService>('CompanyService');

  const map = React.createRef<YaMap>();

  const [companies, setCompanies] = React.useState<CompanyMapMarker[]>([]);
  const [orderRequest, setOrderRequest] = React.useState<OrderRequest>(null);
  const [isToggled, setIsToggled] = React.useState(false);

  React.useEffect(() => {
    async function getCompanies() {
      let companies = await companyService.getCompanies(
        route.params.categories[route.params.categoryId].categoryId,
        changeState);
      if (companies.result == 'successful') {
        map.current?.setCenter(
          {lat: +companies.content[companies.content.length-1].latitude, lon: +companies.content[companies.content.length-1].longitude},
          15,
          Animation.SMOOTH);
        setCompanies(companies.content);
      }
    }
    getCompanies();
  }, []);

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
  }

  return (
    <>
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
                  key={index}/>
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
    </>
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
