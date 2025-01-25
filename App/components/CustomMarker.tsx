import {CompanyMapMarker} from '../types/DomainTypes';
import {Marker} from 'react-native-yamap';
import React from 'react';
import {Image, View} from 'react-native';

export default function CustomMarker({ company, index, onPress }: {
  company: CompanyMapMarker
  index: number
  onPress: (companyId: string) => void}) {
  const [markerKey, setMarkerKey] = React.useState(index);
  const [isEnd, setIsEnd] = React.useState(false);

  return (
    <Marker
      key={markerKey}
      point={{ lat: +company.latitude, lon: +company.longitude }}
      onPress={() => onPress(company.id)}>
      <View>
        <Image
          source={{
            uri: `${process.env.MINIO_URL}/app-files/${company.iconUri}`,
          }}
          onLoadEnd={() => {
            if (!isEnd) {
              setMarkerKey(markerKey + 1);
              setIsEnd(true);
            }
          }}
          style={{
            resizeMode: 'contain',
            borderRadius: 15,
            width: 30,
            height: 30,
            overflow: 'hidden',
            borderWidth: 2,
            borderColor: 'white',
          }}
        />
      </View>
    </Marker>
  )
}
