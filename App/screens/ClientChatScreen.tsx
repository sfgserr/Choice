import {ClientChatScreenProps} from '../types/NavigationTypes.ts';
import ChatScreen from './ChatScreen.tsx';
import React from 'react';

export default function ClientChatScreen({route, navigation}: ClientChatScreenProps) {
  return <ChatScreen id={route.params.id} navigation={navigation} onGoBack={route.params.onGoBack}/>;
}
