import React from 'react';
import {
    View,
    Dimensions,
    TouchableOpacity,
    Text,
    Image,
    RefreshControl,
    TextInput
} from 'react-native';
import { Icon } from 'react-native-elements';
import { useIsFocused } from '@react-navigation/native';
import chatService from '../services/chatService';
import env from '../env';
import { FlatList } from 'react-native-gesture-handler';
import userStore from '../services/userStore';
import Message from '../Components/Message';

const ChatScreen = ({ navigation, route }) => {
    const { chatId } = route.params;
    const [refreshing, setRefreshing] = React.useState(false);
    const [chat, setChat] = React.useState({
        name: '',
        iconUri: '',
        guid: '',
        messages: []
    });
    const [messages, setMessages] = React.useState([]);
    const [text, setText] = React.useState('');

    const { width, height } = Dimensions.get('screen');

    const isFocused = useIsFocused();

    const onRefresh = React.useCallback(async () => {
        setRefreshing(true);

        let chat = await chatService.getChat(chatId);
        setChat(chat);

        setMessages(Object.keys(chat.messages).map((i) => ({
            id: chat.messages[i].id,
            receiverId: chat.messages[i].receiverId,
            senderId: chat.messages[i].senderId,
            creationTime: chat.messages[i].creationTime,
            body: chat.messages[i].body,
            type: chat.messages[i].type    
        })));

        await userStore.retrieveData(userStore.getUserType());

        setRefreshing(false);
    }, []);

    React.useEffect(() => {
        isFocused && onRefresh();
    }, [isFocused])

    return (
        <View
            style={{
                flex: 1, 
                justifyContent: 'center', 
                backgroundColor: '#F4F5FF'
            }}>
            <View
                style={{
                    width,
                    top: 0,
                    backgroundColor: 'white',
                    justifyContent: 'center'
                }}>
                <View
                    style={{
                        flexDirection: 'row',
                        justifyContent: 'space-between',
                        paddingTop: 20,
                        paddingHorizontal: 10
                    }}>
                    <TouchableOpacity 
                        style={{alignSelf: 'center'}}
                        onPress={() => navigation.goBack()}>
                        <Icon 
                            name='chevron-left'
                            type='material'
                            color={'#2688EB'}
                            size={40}/>
                    </TouchableOpacity>
                    <Text
                        style={{
                            fontSize: 17,
                            fontWeight: '500',
                            color: 'black',
                            alignSelf: 'center'
                        }}>
                        {chat.name}
                    </Text>
                    <Image
                        style={{
                            width: 40,
                            height: 40,
                            borderRadius: 360,
                            alignSelf: 'center'
                        }}
                        source={{uri: `${env.api_url}/api/objects/${chat.iconUri}`}}/>    
                </View>
            </View>
            <FlatList
                refreshControl={
                    <RefreshControl refreshing={refreshing} onRefresh={onRefresh}/>
                }
                data={messages}
                style={{paddingTop: 10}}
                renderItem={({item}) => {
                    return (
                        <View style={{paddingHorizontal: 10, paddingTop: 5}}>
                            <Message 
                                message={item}
                                userId={userStore.get().guid}/>    
                        </View>
                    )
                }}/>
            <View
                style={{
                    bottom: 0,
                    backgroundColor: 'white',

                }}>
                <View
                    style={{
                        flexDirection: 'row',
                        justifyContent: 'space-between',
                        paddingTop: 10,
                        paddingBottom: 20
                    }}>
                    <TouchableOpacity
                        style={{alignSelf: 'center'}}>
                        <Icon
                            type='material'
                            name='attach-file'
                            size={20}
                            color='#858E99'/>    
                    </TouchableOpacity>
                    <View
                        style={{paddingHorizontal: 5, flex: 1}}>
                        <View
                            style={{
                                flexDirection: 'row',
                                borderRadius: 15,
                                backgroundColor: 'white',
                                borderWidth: 1,
                                borderColor: '#D1D1D6',
                                paddingLeft: 10,
                                height: height/20,
                            }}>
                            <TextInput
                                style={{
                                    alignSelf: 'center',
                                    fontSize: 17,
                                    fontWeight: '400',
                                    color: 'black',
                                    flex: 1
                                }}
                                placeholder='Сообщение'
                                placeholderTextColor='#AEAEB2'
                                numberOfLines={1}
                                value={text}
                                onChangeText={(text) => setText(text)}/>    
                        </View>
                    </View>
                    <TouchableOpacity
                        style={{alignSelf: 'center'}}
                        disabled={text == ''}
                        onPress={text != '' && (async () => {
                            await chatService.sendMessage(text, chat.guid);
                        })}>
                        <Icon
                            type='material'
                            name='send'
                            size={20}
                            color='#858E99'/>    
                    </TouchableOpacity>
                </View>    
            </View>
        </View>
    );
}

export default ChatScreen;