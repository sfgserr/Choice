import ChatsScreen from '../../ChatsScreen.tsx';
import {CompanyChatsScreenProps} from '../../../types/NavigationTypes.ts';
import React from 'react';

export default function CompanyChatsScreen({navigation}: CompanyChatsScreenProps) {
    return <ChatsScreen navigation={navigation}/>;
}
