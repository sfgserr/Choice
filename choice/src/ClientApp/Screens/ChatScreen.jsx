import React from 'react';
import {
    View,
    FlatList,
    Text,
    RefreshControl
} from 'react-native';
import Chat from '../Components/Chat';
import chatStore from '../services/chatStore';

export default function ChatScreen({ navigation }) {
    const [chats, setChats] = React.useState([]);
    const [refreshing, setRefreshing] = React.useState(false);

    const onRefresh = React.useCallback(async () => {
        setRefreshing(true);

        await chatStore.retrieveData();
        setChats(chatStore.getChats());

        setRefreshing(false);
    }, []);

    React.useEffect(() => {
        async function getChats() {
            await chatStore.retrieveData();
            setChats(chatStore.getChats());
        }
        getChats();
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