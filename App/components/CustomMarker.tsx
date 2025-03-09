import {CompanyMapMarker, Message} from '../types/DomainTypes';
import {Marker} from 'react-native-yamap';
import React from 'react';
import {DeviceEventEmitter, Image, View} from 'react-native';

export default function CustomMarker({
  company,
  onPress,
}: {
  company: {marker: CompanyMapMarker, responseId: string, index: number};
  onPress: (companyId: string, responseId: string) => void;
}) {

  const [isLoadEnd, setIsLoadEnd] = React.useState(false);

  return (
    <Marker
      key={company.index}
      point={{lat: +company.marker.latitude, lon: +company.marker.longitude}}
      onPress={() => onPress(company.marker.id, company.responseId)}>
      <View>
        <Image
          source={{
            uri: `${process.env.MINIO_URL}/app-files/${company.marker.iconUri}`,
          }}
          onLoadEnd={() => {
            if (!isLoadEnd) {
              company.index++;
              setIsLoadEnd(true);
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
