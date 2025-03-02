import {CompanyMapMarker} from '../types/DomainTypes';
import {Marker} from 'react-native-yamap';
import React from 'react';
import {Image, View} from 'react-native';

export default function CustomMarker({
  company,
  index,
  onPress,
}: {
  company: {marker: CompanyMapMarker, responseId: string};
  index: number;
  onPress: (companyId: string) => void;
}) {
  const [markerKey, setMarkerKey] = React.useState(index);
  const [isEnd, setIsEnd] = React.useState(false);

  return (
    <Marker
      key={markerKey}
      point={{lat: +company.marker.latitude, lon: +company.marker.longitude}}
      onPress={() => onPress(company.marker.id)}>
      <View>
        <Image
          source={{
            uri: `${process.env.MINIO_URL}/app-files/${company.marker.iconUri}`,
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
            borderColor: company.responseId != '' ? '#6DC876' : 'white',
          }}
        />
      </View>
    </Marker>
  );
}
