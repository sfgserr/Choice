import {ClientImageViewScreenProps} from '../types/NavigationTypes.ts';
import ImageViewScreen from './ImageViewScreen.tsx';
import React from 'react';

export default function ClientImageViewScreen({route, navigation}: ClientImageViewScreenProps) {
  return <ImageViewScreen uri={route.params.uri} navigation={navigation}/>;
}
