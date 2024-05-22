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

export default function ChatScreen({ navigation }) {
    const [chats, setChats] = React.useState([]);
    const [refreshing, setRefreshing] = React.useState(false);

    const isFocused = useIsFocused();

    const onRefresh = React.useCallback(async () => {
        setRefreshing(true);

        await chatStore.retrieveData();
        setChats(chatStore.getChats());

        setRefreshing(false);
    }, []);

    DeviceEventEmitter.addListener('orderCreated', async message => {
        if (chats.length == 0) {
            let chat = await chatService.getChat(message.senderId);

            setChats(prev => {
                prev.push(chat);
                return [...prev];
            });
        }
        else {
            let index = chats.findIndex(c => c.guid == message.senderId);
            setChats(prev => {
                prev[index].messages.push(message);
                return [...prev];
            });
        }
    });

    React.useEffect(() => {
        isFocused && onRefresh();
    }, [isFocused]);

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
                        <View>
                            <Chat chat={item}/>
                        </View>
                    )
                }}/>
        </View>
    );
}