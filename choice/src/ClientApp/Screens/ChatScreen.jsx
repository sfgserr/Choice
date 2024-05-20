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

export default function ChatScreen({ navigation }) {
    const [chats, setChats] = React.useState([]);
    const [isGotChats, setIsGotChats] = React.useState(false);
    const [refreshing, setRefreshing] = React.useState(false);

    const onRefresh = React.useCallback(async () => {
        setRefreshing(true);

        await chatStore.retrieveData();
        setChats(chatStore.getChats());

        setRefreshing(false);
    }, []);

    DeviceEventEmitter.addListener('orderCreated', async message => {
        if (isGotChats && chats.length == 0) {
            let chat = await chatService.getChat(message.senderId);

            setChats(prev => {
                prev.push(chat);
                return [...prev];
            });
        }
        else if (isGotChats) {
            let index = chats.findIndex(c => c.guid == message.senderId);
            chats[index].messages.push(prev => {
                prev.push(message);
                return [...prev];
            });
        }
    });

    React.useEffect(() => {
        async function getChats() {
            await chatStore.retrieveData();
            setChats(chatStore.getChats());
        }
        getChats();
        setIsGotChats(true);
    }, []);

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