import React from 'react'
import {
    View,
    Text,
    Dimensions
} from 'react-native'
import { Icon } from 'react-native-elements';
import dateHelper from '../helpers/dateHelper';

const Message = ({message, userId}) => {
    const isUserReceiver = message.receiverId == userId;

    const { width, height } = Dimensions.get('screen');

    return (
        <View style={{flexDirection: 'column'}}>
            {
                message.type == '1' ?
                <>
                    <View
                        style={{
                            alignSelf: isUserReceiver ? 'flex-start' : 'flex-end',
                            backgroundColor: isUserReceiver ? 'white' : '#2D81E0',
                            borderTopLeftRadius: 15,
                            borderTopRightRadius: 15,
                            borderBottomLeftRadius: isUserReceiver ? 5 : 15,
                            borderBottomRightRadius: !isUserReceiver ? 5 : 15,
                            flexDirection: 'row',
                            paddingHorizontal: 10,
                            padding: 7,
                            justifyContent: 'space-between',
                            borderColor: '#B5CADD',
                            borderWidth: isUserReceiver ? 1 : 0
                        }}>
                        <Text
                            style={{
                                color: isUserReceiver ? 'black' : 'white',
                                fontSize: 15,
                                fontWeight: '400'
                            }}>
                            {message.body}    
                        </Text>
                        <View
                            style={{flexDirection: 'column', paddingLeft: 10}}>
                            <View
                                style={{flexDirection: 'row', flex:1}}>
                                <Text
                                    style={{
                                        fontWeight: '100',
                                        fontSize: 11,
                                        alignSelf: 'flex-end',
                                        color: isUserReceiver ? 'black' : 'white'
                                    }}>
                                    {dateHelper.getTimeFromString(message.creationTime)}
                                </Text>
                                {
                                    !isUserReceiver ? 
                                    <>
                                        <Icon
                                            type="material"
                                            name={message.isRead ? 'done-all' : 'check'}
                                            size={20}
                                            color={'white'}/>
                                    </>
                                    :
                                    <>
                                    </>
                                }    
                            </View>     
                        </View>
                    </View>
                </>
                :
                <>
                </> 
            }    
        </View>
    )
}

export default Message;