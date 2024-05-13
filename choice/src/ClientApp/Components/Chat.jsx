import React from "react";
import {
    View,
    Dimensions,
    Image,
    Text
} from 'react-native';
import env from "../env";

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
                style={{flexDirection: 'column'}}>
                <Text
                    style={{
                        fontWeight: '500',
                        color: 'black',
                        fontSize: 16
                    }}>
                    {chat.name}
                </Text>
                
            </View>    
        </View>
    )
}