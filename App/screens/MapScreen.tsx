import {Text} from 'react-native';
import {
  View,
  Dimensions
} from 'react-native';
import YaMap from 'react-native-yamap';
import NavigateBackButton from '../components/NavigateBackButton.tsx';
import {MapScreenProps} from '../types/NavigationTypes.ts';
import {StyledButton} from '../components/StyledButton.tsx';
import React from 'react';
import {AuthContext} from '../App.tsx';
import {CompanyMapMarker} from '../types/DomainTypes.ts';

export default function MapScreen({route, navigation}: MapScreenProps) {
  const { changeState } = React.useContext(AuthContext);

  const d = Dimensions.get('screen');
  const [companies, setCompanies] = React.useState<CompanyMapMarker[]>([]);

  React.useEffect(() => {
    async function getCompanies() {
      let companies = await route.params.companyService.getCompanies(
        route.params.category.id,
        changeState);

      setCompanies(companies);
    }
    getCompanies();
  }, []);

  return (
    <>
      <YaMap
        userLocationIcon={{
          uri: 'https://www.clipartmax.com/png/middle/180-1801760_pin-png.png',
        }}
        initialRegion={{
          lat: 50,
          lon: 50,
          zoom: 10,
          azimuth: 80,
          tilt: 100,
        }}
        style={{flex: 1}}>


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
          {route.params.category.title}
        </Text>
      </View>
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
            pressed={async () => {}}/>
        </View>
      </View>
    </>
  );
}
