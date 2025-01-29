import ChatScreen from './ChatScreen.tsx';
import {CompanyChatScreenProps} from '../types/NavigationTypes.ts';

export default function CompanyChatScreen({route, navigation}: CompanyChatScreenProps) {
  return <ChatScreen id={route.params.id} navigation={navigation}/>
}
