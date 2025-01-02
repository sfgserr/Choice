import {CompanyMapMarker} from '../types/DomainTypes';
import {Marker} from 'react-native-yamap';
import React from 'react';
import {Image} from 'react-native';

export default function CustomMarker({ company, index }: { company: CompanyMapMarker; index: number }) {
  const [markerKey, setMarkerKey] = React.useState(index);
  const [isEnd, setIsEnd] = React.useState(false);

  return (
    <Marker
      key={markerKey}
      point={{ lat: +company.latitude, lon: +company.longitude }}
    >
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
    </Marker>
  )
}
