import React from "react";
import {
    View,
    Dimensions,
    Image,
    Text
} from 'react-native';
import env from "../env";
import { Icon } from "react-native-elements";
import dateHelper from "../helpers/dateHelper";

const Chat = ({navigation, chat}) => {
    const { width, height } = Dimensions.get('screen');

    return (
        <View
            style={{backgroundColor: 'white', width, flexDirection: 'row'}}>
            <Image
                style={{
                    width: 40,
                    height: 40
                }}
                source={{uri: `${env.api_url}/api/objects/${chat.iconUri}`}}/>

            <View
                style={{flexDirection: 'column', paddingLeft: 10}}>
                <View
                    style={{flexDirection: 'row'}}>
                    <Text
                        style={{
                            color: 'black',
                            fontSize: 16,
                            fontWeight: '500',
                            alignSelf: 'flex-start'
                        }}>
                        {chat.name}
                    </Text>
                    <Icon
                        name="done-all"
                        type="material"
                        color={chat.isRead ? '#21C004' : '#BBBBBB'}
                        style={{alignSelf: 'flex-end'}}/>
                    <Text
                        style={{
                            color: '#8E8E93',
                            fontWeight: '400',
                            fontSize: 14
                        }}>
                        {dateHelper.convertTimeToString(chat.sentTime)}
                    </Text>    
                </View>
            </View>    
        </View>
    )
}

export default Chat;