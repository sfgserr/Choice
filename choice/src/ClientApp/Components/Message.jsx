import React from 'react'
import {
    View,
    Text,
    Dimensions,
    TouchableOpacity
} from 'react-native'
import { Icon } from 'react-native-elements';
import dateHelper from '../helpers/dateHelper';
import userStore from '../services/userStore';
import styles from '../Styles';
import DatePicker from 'react-native-date-picker';
import { Modalize } from 'react-native-modalize';

const Message = ({message, userId, changeDate}) => {
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
                message.type == '2' ?
                <>
                </> 
                :
                <>
                    <View style={{paddingTop: 5}}>
                        <View
                            style={{
                                paddingHorizontal: 15,
                                borderRadius: 10,
                                backgroundColor: 'white',
                                borderColor: '#B5CADD',
                                borderWidth: 1
                            }}>
                            <Text
                                style={{
                                    color: 'black',
                                    fontSize: 14,
                                    fontWeight: '600',
                                    paddingTop: 10
                                }}>
                                {JSON.parse(message.body).UserChangedEnrollmentDate != null ? 
                                    (JSON.parse(message.body).UserChangedEnrollmentDate == userStore.get().guid ? 
                                        'Вы изменили дату и время записи' : userStore.getUserType() == 1 ? 
                                            'Клиент предлагает изменить дату записи' : 
                                                'Компания предлагает изменить дату записи ') : 
                                                    userStore.getUserType() == 1 ? 
                                                    'Ответ компании на ваш запрос' : 
                                                        'Ваш ответ на заказ клиента'}
                            </Text>
                            {
                                JSON.parse(message.body).Price > 0 ?
                                <>
                                    <View
                                        style={{
                                            flexDirection: 'row',
                                            justifyContent: 'space-between'
                                        }}>
                                        <View 
                                            style={{
                                            flexDirection: 'row',
                                                paddingTop: 10
                                            }}>
                                            <Icon
                                                type='material'
                                                name='currency-ruble'
                                                size={15}
                                                color='#3B4147'
                                                style={{alignSelf: 'center'}}/>
                                            <Text
                                                style={{
                                                    fontSize: 14,
                                                    fontWeight: '400',
                                                    color: '#2E2424',
                                                    alignSelf: 'center',
                                                    paddingLeft: 5
                                                }}>
                                                Стоимость
                                            </Text>
                                        </View>
                                        <Text
                                            style={{
                                                color: 'black',
                                                fontSize: 14,
                                                fontWeight: '500',
                                                alignSelf: 'center'
                                            }}>
                                            {`${JSON.parse(message.body).Price} рублей`}        
                                        </Text>
                                    </View>
                                </>
                                :
                                <>
                                </>
                            }
                            {
                                JSON.parse(message.body).Deadline > 0 ?
                                <>
                                    <View
                                        style={{
                                            flexDirection: 'row',
                                            justifyContent: 'space-between'
                                        }}>
                                        <View 
                                            style={{
                                                flexDirection: 'row',
                                                paddingTop: 10
                                            }}>
                                            <Icon
                                                type='material'
                                                name='schedule'
                                                size={15}
                                                color='#3B4147'
                                                style={{alignSelf: 'center'}}/>
                                            <Text
                                                style={{
                                                    fontSize: 14,
                                                    fontWeight: '400',
                                                    color: '#2E2424',
                                                    alignSelf: 'center',
                                                    paddingLeft: 5
                                                }}>
                                                Время выполнения работы
                                            </Text>
                                        </View>
                                        <Text
                                            style={{
                                                color: 'black',
                                                fontSize: 14,
                                                fontWeight: '500',
                                                alignSelf: 'center'
                                            }}>
                                            {`${JSON.parse(message.body).Deadline} часов`}        
                                        </Text>
                                    </View>
                                </>
                                :
                                <>
                                </>
                            }
                            {
                                JSON.parse(message.body).EnrollmentTime != null ?
                                <>
                                    <View
                                        style={{
                                            flexDirection: 'row',
                                            justifyContent: 'space-between'
                                        }}>
                                        <View 
                                            style={{
                                                flexDirection: 'row',
                                                paddingTop: 10
                                            }}>
                                            <Icon
                                                type='material'
                                                name='calendar-today'
                                                size={15}
                                                color='#3B4147'
                                                style={{alignSelf: 'center'}}/>
                                            <Text
                                                style={{
                                                    fontSize: 14,
                                                    fontWeight: '400',
                                                    color: '#2E2424',
                                                    alignSelf: 'center',
                                                    paddingLeft: 5
                                                }}>
                                                Дата и время записи
                                            </Text>
                                        </View>
                                        <Text
                                            style={{
                                                color: 'black',
                                                fontSize: 14,
                                                fontWeight: '500',
                                                alignSelf: 'center'
                                            }}>
                                            {`${dateHelper.formatDate(JSON.parse(message.body).EnrollmentTime)}`}        
                                        </Text>
                                    </View>
                                </>
                                :
                                <>
                                </>
                            }
                            {
                                JSON.parse(message.body).Prepayment > 0 ?
                                <>
                                    <View
                                        style={{
                                            flexDirection: 'row',
                                            justifyContent: 'space-between'
                                        }}>
                                        <View 
                                            style={{
                                                flexDirection: 'row',
                                                paddingTop: 10
                                            }}>
                                            <Icon
                                                type='material'
                                                name='payments'
                                                size={15}
                                                color='#3B4147'
                                                style={{alignSelf: 'center'}}/>
                                            <Text
                                                style={{
                                                    fontSize: 14,
                                                    fontWeight: '400',
                                                    color: '#2E2424',
                                                    alignSelf: 'center',
                                                    paddingLeft: 5
                                                }}>
                                                Предоплата
                                            </Text>
                                        </View>
                                        <Text
                                            style={{
                                                color: 'black',
                                                fontSize: 14,
                                                fontWeight: '500',
                                                alignSelf: 'center'
                                            }}>
                                            {`${JSON.parse(message.body).Prepayment}`}        
                                        </Text>
                                    </View>
                                </>
                                :
                                <>
                                </>
                            }
                            <View style={{paddingTop: 10, paddingBottom: 10}}>
                                <TouchableOpacity
                                    style={{
                                        height: height/20,
                                        borderRadius: 10,
                                        backgroundColor: '#001C3D0D',
                                        justifyContent: 'center'
                                    }}
                                    onPress={() => changeDate(JSON.parse(message.body).OrderId)}>
                                    <Text
                                        style={[
                                            styles.buttonText, {
                                                color: '#2688EB', 
                                                fontSize: 15
                                            }]}>
                                        Изменить дату и время записи
                                    </Text>
                                </TouchableOpacity>
                                {
                                    userStore.getUserType() == 1 ?
                                    <>
                                        <View style={{paddingTop: 10}}>
                                            <TouchableOpacity
                                                style={[
                                                    styles.button, {
                                                        height: height/20
                                                    }
                                                ]}>
                                                <Text
                                                    style={[
                                                        styles.buttonText, {
                                                            fontSize: 15
                                                        }
                                                    ]}>
                                                    {JSON.parse(message.body).Prepayment > 0 ? 'Записаться и внести предоплату' : 'Записаться'}    
                                                </Text>
                                            </TouchableOpacity>
                                        </View>                                    
                                    </>
                                    :
                                    <>
                                    </>
                                }
                            </View>
                        </View>
                    </View>
                </>
            }    
        </View>
    )
}

export default Message;