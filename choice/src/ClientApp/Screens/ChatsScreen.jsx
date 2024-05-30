import React from 'react';
import {
    View,
    FlatList,
    Text,
    RefreshControl,
    DeviceEventEmitter
} from 'react-native';
import Chat from '../Components/Chat';
import chatStore from '../services/chatStore';
import chatService from '../services/chatService';
import { useIsFocused } from '@react-navigation/native';
import { measure } from 'react-native-reanimated';

export default function ChatsScreen({ navigation }) {
    const [chats, setChats] = React.useState([]);
    const [refreshing, setRefreshing] = React.useState(false);

    const isFocused = useIsFocused();

    const onRefresh = React.useCallback(async () => {
        setRefreshing(true);

        await chatStore.retrieveData();
        setChats(chatStore.getChats());

        setRefreshing(false);
    }, []);

    const handleMessage = async (message) => {
        if (isFocused) {
            let index = chats.findIndex(c => c.guid == message.senderId);
            if (index == -1) {
                let chat = await chatService.getChat(message.senderId);

                setChats(prev => {
                    prev.push(chat);
                    return [...prev];
                });
            } 
            else {
                setChats(prev => {
                    prev[index].messages.push(message);
                    return [...prev];
                });
            }
        }
    }

    const handleChatChanged = (user) => {
        
    }

    React.useEffect(() => {
        isFocused && onRefresh();
    }, [isFocused]);

    React.useEffect(() => {
        DeviceEventEmitter.addListener('messageReceived', handleMessage);
        DeviceEventEmitter.addListener('chatChanged', handleChatChanged);

        return () => {
            DeviceEventEmitter.removeAllListeners('messageReceived');
            DeviceEventEmitter.removeAllListeners('chatChanged');
        }
    }, [handleMessage,handleChatChanged]);

    return (
        <View style={{flex: 1, backgroundColor: 'white'}}>
            <Text
                style={{
                    fontWeight: '600',
                    fontSize: 21,
                    color: 'black',
                    paddingTop: 20,
                    alignSelf: 'center'
                }}>
                Чаты
            </Text>
            <FlatList
                data={chats}
                style={{paddingTop: 10}}
                refreshControl={
                    <RefreshControl refreshing={refreshing} onRefresh={onRefresh}/>
                }
                renderItem={({item}) => {
                    return (
                        <View style={{paddingBottom: 5}}>
                            <Chat chat={item} navigation={navigation}/>
                        </View>
                    )
                }}/>
        </View>
    );
}