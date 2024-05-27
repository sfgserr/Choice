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
import orderingService from '../services/orderingService';

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
                                {JSON.parse(message.body).UserChangedEnrollmentDate != null && JSON.parse(message.body).IsActive ? 
                                    (JSON.parse(message.body).UserChangedEnrollmentDate == userStore.get().guid ? 
                                        'Вы изменили дату и время записи' : userStore.getUserType() == 1 ? 
                                            'Компания предлагает изменить дату записи' : 
                                                'Клиент предлагает изменить дату записи ') : 
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
                                            {`${JSON.parse(message.body).IsActive ? dateHelper.formatDate(JSON.parse(message.body).EnrollmentTime) : dateHelper.formatDate(JSON.parse(message.body).PastEnrollmentTime)}`}        
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
                                {
                                    JSON.parse(message.body).IsEnrolled ?
                                    <>
                                        <View style={{paddingBottom: 10}}>
                                            <View
                                                style={{
                                                    height: height/20,
                                                    borderRadius: 10,
                                                    backgroundColor: '#6DC876',
                                                    justifyContent: 'space-between'
                                                }}>
                                                <Icon
                                                    type="materials"
                                                    name="celebration"
                                                    color='white'
                                                    size={20}/>

                                                <Text>
                                                    {`Клиент записан на ${dateHelper.formatDate(JSON.parse(message.body).EnrollmentTime)}`}
                                                </Text>
                                            </View>
                                        </View>
                                    </>
                                    :
                                    <>
                                    </>
                                }
                                {
                                    JSON.parse(message.body).IsActive && userStore.getUserType() == 2 && JSON.parse(message.body).UserChangedEnrollmentDate != null && JSON.parse(message.body).UserChangedEnrollmentDate != userStore.get().guid ?
                                    <>
                                        <View style={{paddingBottom: 5}}>
                                            <TouchableOpacity
                                                style={{
                                                    height: height/20,
                                                    borderRadius: 10,
                                                    backgroundColor: '#001C3D0D',
                                                    justifyContent: 'center'
                                                }}
                                                onPress={async () => {
                                                    let order = await orderingService.confirmDate(JSON.parse(message.body).OrderId);

                                                    message.body = JSON.stringify({
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
                                                    });
                                                }}>
                                                <Text
                                                    style={[
                                                        styles.buttonText, {
                                                            color: '#2688EB', 
                                                            fontSize: 15
                                                        }]}>
                                                    {`Подтвердить дату записи на ${dateHelper.getMonthAndDayFromString(JSON.parse(message.body).EnrollmentTime)} в ${dateHelper.getTimeFromString(JSON.parse(message.body).EnrollmentTime)}`}
                                                </Text>
                                            </TouchableOpacity>
                                        </View>
                                    </>
                                    :
                                    <>
                                    </>
                                }
                                <TouchableOpacity
                                    style={{
                                        height: height/20,
                                        borderRadius: 10,
                                        backgroundColor: JSON.parse(message.body).IsActive && (userStore.getUserType() == 1 ? JSON.parse(message.body).IsDateConfirmed : true) ? '#001C3D0D' : '#fafafb',
                                        justifyContent: 'center'
                                    }}
                                    disabled={!JSON.parse(message.body).IsActive}
                                    onPress={JSON.parse(message.body).IsActive && (() => changeDate(JSON.parse(message.body).OrderId))}>
                                    <Text
                                        style={[
                                            styles.buttonText, {
                                                color: JSON.parse(message.body).IsActive && (userStore.getUserType() == 1 ? JSON.parse(message.body).IsDateConfirmed : true) ? '#2688EB' : '#a8cff7', 
                                                fontSize: 15
                                            }]}>
                                        {JSON.parse(message.body).UserChangedEnrollmentDate != null && JSON.parse(message.body).IsActive ? 'Предложить другую дату и время' : 'Изменить дату и время записи'}
                                    </Text>
                                </TouchableOpacity>
                                {
                                    userStore.getUserType() == 1 && JSON.parse(message.body).IsActive && JSON.parse(message.body).IsDateConfirmed ?
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