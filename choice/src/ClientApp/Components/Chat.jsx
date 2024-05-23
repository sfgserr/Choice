import React from "react";
import {
    View,
    Dimensions,
    Image,
    Text,
    TouchableOpacity,
} from 'react-native';
import env from "../env";
import { Icon } from "react-native-elements";
import dateHelper from "../helpers/dateHelper";

const Chat = ({navigation, chat}) => {
    const { width, height } = Dimensions.get('screen');

    return (
        <TouchableOpacity
            style={{
                backgroundColor: 'white',
                flexDirection: 'row', 
                paddingHorizontal: 10,
            }}
            onPress={() => navigation.navigate('Chat', { chatId: chat.guid })}>
            <Image
                style={{
                    width: 50,
                    height: 50,
                    borderRadius: 360
                }}
                source={{uri: `${env.api_url}/api/objects/${chat.iconUri}`}}/>

            <View
                style={{flexDirection: 'column', paddingLeft: 10, flex: 1}}>
                <View
                    style={{
                        flexDirection: 'row',
                        justifyContent: 'space-between'
                    }}>
                    <View
                        style={{
                            flexDirection: 'row',
                            justifyContent: 'flex-start'
                        }}>
                        <Text
                            style={{
                                color: 'black',
                                fontSize: 16,
                                fontWeight: '500',
                                alignSelf: 'flex-start'
                            }}>
                            {chat.name}
                        </Text>
                    </View>
                    <View
                        style={{flexDirection: 'row', justifyContent: 'flex-end'}}>
                        <Icon
                            name="done-all"
                            type="material"
                            size={20}
                            color={chat.isRead ? '#21C004' : '#BBBBBB'}
                            style={{alignSelf: 'flex-end', paddingRight: 5}}/>
                        <Text
                            style={{
                                color: '#8E8E93',
                                fontWeight: '400',
                                fontSize: 14
                            }}>
                            {dateHelper.getTimeFromString(chat.messages[chat.messages.length-1].creationTime)}
                        </Text> 
                    </View>   
                </View>
                <Text
                    style={{
                        color: '#8E8E93',
                        fontSize: 15,
                        fontWeight: '400'
                    }}>
                    {chat.messages[chat.messages.length-1].type == 1 ? chat.messages[chat.messages.length-1].body : chat.messages[chat.messages.length-1].type == 2 ? 'Фото' : 'Заказ'}
                </Text>
            </View>    
        </TouchableOpacity>
    )
}

export default Chat;