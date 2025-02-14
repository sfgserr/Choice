import {CompanyImageViewScreenProps} from '../types/NavigationTypes.ts';
import ImageViewScreen from './ImageViewScreen.tsx';
import React from 'react';

export default function CompanyImageViewScreen({route, navigation}: CompanyImageViewScreenProps) {
  return <ImageViewScreen uri={route.params.uri} navigation={navigation}/>;
}
