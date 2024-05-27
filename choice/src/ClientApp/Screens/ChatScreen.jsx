import React from 'react';
import {
    View,
    Dimensions,
    TouchableOpacity,
    Text,
    Image,
    RefreshControl,
    TextInput,
    DeviceEventEmitter
} from 'react-native';
import { Icon } from 'react-native-elements';
import { useIsFocused } from '@react-navigation/native';
import chatService from '../services/chatService';
import env from '../env';
import { FlatList } from 'react-native-gesture-handler';
import userStore from '../services/userStore';
import Message from '../Components/Message';
import dateHelper from '../helpers/dateHelper';
import DatePicker from 'react-native-date-picker';
import { Modalize } from 'react-native-modalize';
import styles from '../Styles';
import orderingService from '../services/orderingService';

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
    const [mockId, setMockId] = React.useState(-1);
    const [text, setText] = React.useState('');
    const enrollmentDateRef = React.useRef(null);
    const [enrollmentDate, setEnrollmentDate] = React.useState(new Date());
    const { width, height } = Dimensions.get('screen');
    const [id, setId] = React.useState(-1);

    const isFocused = useIsFocused();

    const handleMessage = (message) => {
        if (isFocused) {
            setMessages(prev => {
                prev.push(message);
                return [...prev];
            })
        }
    }

    const handleChangedMessage = (message) => {
        if (isFocused) {
            setMessages(prev => {
                let index = prev.findIndex(m => m.id == message.id);

                prev[index] = message;

                return [...prev];
            });
        }
    }

    React.useEffect(() => {
        DeviceEventEmitter.addListener('messageReceived', handleMessage);
        DeviceEventEmitter.addListener('messageChanged', handleChangedMessage);

        return () => DeviceEventEmitter.removeAllListeners('messageReceived');
    }, [handleMessage]);

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
            <Modalize
                ref={enrollmentDateRef}
                adjustToContentHeight
                childrenStyle={{height: '70%'}}>
                <View
                    style={{
                        flex: 1, 
                        justifyContent: 'center',
                        paddingHorizontal: 20
                    }}>
                    <View
                        style={{
                            flexDirection: 'row', 
                            justifyContent: 'space-between',
                            paddingTop: 20
                        }}>
                        <Text></Text>
                        <Text
                            style={{
                                fontSize: 21,
                                fontWeight: '600',
                                color: 'black'
                            }}>
                            Выберите дату и время записи
                        </Text>
                        <TouchableOpacity
                            style={{
                                borderRadius: 360,
                                backgroundColor: '#eff1f2',
                            }}
                            onPress={() => {
                                enrollmentDateRef.current?.close();
                            }}>
                            <Icon
                                name='close'
                                type='material'
                                size={27}
                                color='#818C99'/>
                        </TouchableOpacity>
                    </View>
                    <View style={{paddingTop: 20}}>
                        <DatePicker
                            date={enrollmentDate}
                            mode="datetime"
                            style={{alignSelf: 'center'}}
                            onDateChange={setEnrollmentDate}/>
                    </View>
                    <View style={{paddingTop: 20}}>
                        <TouchableOpacity 
                            style={[styles.button]}
                            onPress={async () => {
                                let order = await orderingService.changeOrderEnrollmentDate(id, dateHelper.convertFullDateToJson(enrollmentDate));
                                setMessages(prev => {
                                    let index = prev.findIndex(m => {
                                        if (m.type == 3) {
                                            return JSON.parse(m.body).OrderId == id
                                        }

                                        return false;
                                    });
                                    JSON.parse(prev[index].body).IsActive = false;
                                    prev.push({
                                        id: mockId,
                                        body: JSON.stringify({
                                            OrderId: order.id,
                                            OrderRequestId: order.orderRequestId,
                                            Price: order.price,
                                            Prepayment: order.prepayment,
                                            Deadline: order.deadline,
                                            IsEnrolled: order.isEnrolled,
                                            EnrollmentTime: order.enrollmentDate,
                                            Status: order.status,
                                            IsActive: true,
                                            IsDateConfirmed: order.isDateConfirmed,
                                            UserChangedEnrollmentDate: order.userChangedEnrollmentDateGuid
                                        }),
                                        senderId: userStore.get().guid,
                                        receiverId: prev[index].receiverId != userStore.get().guid ? prev[index].receiverId : prev[index].senderId,
                                        type: 3,
                                        creationTime: dateHelper.convertFullDateToJson(new Date())
                                    });
                                    setMockId(prev => prev-1);
                                    return [...prev];
                                });
                                enrollmentDateRef.current?.close();
                            }}>
                            <Text style={[styles.buttonText]}>
                                Выбрать
                            </Text>
                        </TouchableOpacity>
                    </View>
                </View>
            </Modalize>
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
                data={[...messages].reverse()}
                style={{paddingTop: 10}}
                inverted
                renderItem={({item, index}) => {
                    return (
                        <View style={{paddingHorizontal: 10, paddingTop: 2.5, paddingBottom: 2.5}}>
                            {
                                index == messages.length-1 || dateHelper.getDateFromString(messages[index].creationTime) != dateHelper.getDateFromString(messages[index+1].creationTime) ?
                                <>
                                    <View style={{paddingTop: 2.5}}>
                                        <View
                                            style={{
                                                borderRadius: 20,
                                                backgroundColor: '#DBE3FF82',
                                                padding: 7,
                                                alignSelf: 'center'
                                            }}>
                                            <Text
                                                style={{
                                                    fontSize: 11,
                                                    fontWeight: '400',
                                                    color: '#6B89AC',
                                                    alignSelf: 'center'
                                                }}>
                                                {dateHelper.getDateFromString(item.creationTime)}
                                            </Text>    
                                        </View>
                                    </View>        
                                </>
                                :
                                <>
                                </>
                            }
                            <Message 
                                message={item}
                                userId={userStore.get().guid}
                                changeDate={(id) => {
                                    setId(id);
                                    enrollmentDateRef.current?.open()
                                }}/>    
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
                            let message = await chatService.sendMessage(text, chat.guid);

                            setMessages(prev => {
                                prev.push(message);
                                return [...prev];
                            });
                            
                            setText('');
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