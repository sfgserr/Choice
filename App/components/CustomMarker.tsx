import {CompanyMapMarker, Message} from '../types/DomainTypes';
import {Marker} from 'react-native-yamap';
import React from 'react';
import {DeviceEventEmitter, Image, View} from 'react-native';

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
  const [responseId, setResponseId] = React.useState(company.responseId);

  console.log(company);

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
            if (!isEnd || responseId != '') {
              setMarkerKey(prev => prev + 1);
              setIsEnd(true);
              setResponseId('');
            }
          }}
          style={{
            resizeMode: 'contain',
            borderRadius: 15,
            width: 30,
            height: 30,
            overflow: 'hidden',
            borderWidth: company.responseId != '' ? 4 : 2,
            borderColor: company.responseId != '' ? '#6DC876' : 'white',
          }}
        />
      </View>
    </Marker>
  );
}
