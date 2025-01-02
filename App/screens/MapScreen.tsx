import {Image, Text} from 'react-native';
import {
  View,
  Dimensions
} from 'react-native';
import YaMap, {Animation, Marker} from 'react-native-yamap';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {MapScreenProps} from '../types/NavigationTypes.ts';
import {StyledButton} from '../components/buttons/StyledButton.tsx';
import React from 'react';
import {AuthContext} from '../App.tsx';
import {CompanyMapMarker} from '../types/DomainTypes.ts';
import {OrderRequest} from '../types/DomainTypes.ts';
import OrderRequestModal from '../components/modals/OrderRequestModal.tsx';

export default function MapScreen({route, navigation}: MapScreenProps) {
  const { changeState } = React.useContext(AuthContext);
  const map = React.createRef<YaMap>();

  const d = Dimensions.get('screen');
  const [companies, setCompanies] = React.useState<CompanyMapMarker[]>([]);
  const [orderRequest, setOrderRequest] = React.useState<OrderRequest>(null);
  const [isToggled, setIsToggled] = React.useState(false);

  React.useEffect(() => {
    async function getCompanies() {
      let companies = await route.params.companyService.getCompanies(
        route.params.categoryId,
        changeState);
      if (companies.result == 'successful') {

        map.current?.setCenter(
          {lat: +companies.content[0].latitude, lon: +companies.content[0].longitude},
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
        style={{flex: 1}}>
        {companies.length > 0 ? (
          <>
            {companies.map((company, index) => {
              return (
                <Marker
                  point={{lat: +company.latitude, lon: +company.longitude}}
                  key={index}>
                  <Image
                    source={{uri: `${process.env.MINIO_URL}/app-files/${company.iconUri}`}}
                    style={{
                      resizeMode: 'contain',
                      borderRadius: 15,
                      width: 30,
                      height: 30,
                      overflow: 'hidden',
                      borderWidth: 2,
                      borderColor: 'white'
                    }}/>
                </Marker>
              )
            })}
          </>
        ) : (<></>)}
      </YaMap>
      <View
        style={{
          position: 'absolute',
          height: d.height * 0.086,
          width: '100%',
          backgroundColor: 'white',
          top: 0,
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
          {route.params.categories[route.params.categoryId].title}
        </Text>
      </View>
      {orderRequest == null ? (
        <>
          <View
            style={{
              position: 'absolute',
              height: d.height * 0.086,
              width: '100%',
              backgroundColor: 'white',
              bottom: 0,
              justifyContent: 'center',
              paddingHorizontal: 10
            }}>
            <View style={{flex: 1}}>
              <StyledButton
                content={'Создать заказ'}
                top={10}
                bottom={0}
                isDisabled={false}
                pressed={async () => {
                  navigation.navigate('CreateOrderRequestScreen', {
                    categories: route.params.categories,
                    categoryIndex: route.params.categoryId,
                    onGoBack: onOrderRequestCreated
                  });
                }}/>
            </View>
          </View>
        </>) : (
          <>
            <OrderRequestModal
              isToggled={isToggled}
              orderRequest={orderRequest}/>
          </>)}
    </>
  );
}
