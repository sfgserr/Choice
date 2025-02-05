import ChatsScreen from '../../ChatsScreen.tsx';
import {ClientChatsScreenProps} from '../../../types/NavigationTypes.ts';
import React from 'react';

export default function ClientChatsScreen({navigation}: ClientChatsScreenProps) {
    return <ChatsScreen navigation={navigation}/>;
}
