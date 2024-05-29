import React from "react";
import {
    TouchableOpacity,
    View,
    Text,
    TextInput,
    ScrollView
} from 'react-native';
import { Icon } from "react-native-elements";
import CompanyRequestCard from "../Components/CompanyRequestCard";
import styles from "../Styles";
import ImageBox from "../Components/ImageBox";
import DatePicker from "react-native-date-picker";
import { Modalize } from "react-native-modalize";
import dateHelper from "../helpers/dateHelper";
import userStore from "../services/userStore";
import orderingService from "../services/orderingService";

const CompanyRequestCreationScreen = ({navigation, route}) => {
    const { orderRequest } = route.params;
    const user = userStore.get();

    const [date, setDate] = React.useState(new Date());

    const enrollmentDateRef = React.useRef(null);
    const enrollmentTimeRef = React.useRef(null);
    const deadlineRef = React.useRef(null);

    const [price, setPrice] = React.useState('');
    const [deadline, setDeadline] = React.useState(new Date());
    const [enrollmentDate, setEnrollmentDate] = React.useState(new Date());
    const [enrollmentTime, setEnrollmentTime] = React.useState(new Date());
    const [prepayment, setPrepayment] = React.useState('');

    const [enrollmentDateString, setEnrollmentDateString] = React.useState('');
    const [enrollmentTimeString, setEnrollmentTimeString] = React.useState('');
    const [deadlineString, setDeadlinetString] = React.useState('')

    const [disable, setDisable] = React.useState(true);

    const updateDisable = (state) => {
        if (orderRequest.toKnowPrice && state.price == '') {
            setDisable(true);
            return;
        }

        if (orderRequest.toKnowEnrollmentDate && (state.enrollmentDateString == '' || state.enrollmentTimeString == '')) {
            setDisable(true);
            return;
        }

        if (orderRequest.toKnowDeadline && state.deadlineString == '') {
            setDisable(true);
            return;
        }

        if (user.prepaymentAvailable && state.prepayment == '') {
            setDisable(true);
            return;
        }

        setDisable(false);
    }

    return (
        <ScrollView
            style={{flex: 1, backgroundColor: 'white'}}
            showsVerticalScrollIndicator={false}>
            <Modalize
                ref={enrollmentDateRef}
                adjustToContentHeight
                childrenStyle={{height: '90%'}}>
                <View
                    style={{
                        flex: 1, 
                        justifyContent: 'center',
                        paddingHorizontal: 20,
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
                            Выберите дату записи
                        </Text>
                        <TouchableOpacity
                            style={{
                                borderRadius: 360,
                                backgroundColor: '#eff1f2',
                            }}
                            onPress={() => {
                                enrollmentDateRef.current?.close();
                                setDate(new Date());
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
                            date={date}
                            mode="date"
                            style={{alignSelf: 'center'}}
                            onDateChange={setDate}/>
                    </View>
                    <View>
                        <TouchableOpacity 
                            style={[styles.button]}
                            onPress={() => {
                                enrollmentDateRef.current?.close();
                                setEnrollmentDate(date);
                                setDate(new Date());
                                setEnrollmentDateString(dateHelper.convertDateToString(date));
                                updateDisable({
                                    enrollmentDateString: '.',
                                    enrollmentTimeString,
                                    price,
                                    deadlineString,
                                    prepayment
                                });
                            }}>
                            <Text style={[styles.buttonText]}>
                                Выбрать
                            </Text>
                        </TouchableOpacity>
                    </View>
                </View>
            </Modalize>
            <Modalize
                ref={enrollmentTimeRef}
                adjustToContentHeight
                childrenStyle={{height: '90%'}}>
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
                            Выберите время записи
                        </Text>
                        <TouchableOpacity
                            style={{
                                borderRadius: 360,
                                backgroundColor: '#eff1f2',
                            }}
                            onPress={() => {
                                enrollmentTimeRef.current?.close();
                                setDate(new Date());
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
                            date={date}
                            mode="time"
                            style={{alignSelf: 'center'}}
                            onDateChange={setDate}/>
                    </View>
                    <View style={{paddingTop: 20}}>
                        <TouchableOpacity 
                            style={[styles.button]}
                            onPress={() => {
                                enrollmentTimeRef.current?.close();
                                setEnrollmentTime(date);
                                setDate(new Date());
                                setEnrollmentTimeString(dateHelper.convertTimeToString(date));
                                updateDisable({
                                    enrollmentDateString,
                                    enrollmentTimeString: '.',
                                    price,
                                    deadlineString,
                                    prepayment
                                });
                            }}>
                            <Text style={[styles.buttonText]}>
                                Выбрать
                            </Text>
                        </TouchableOpacity>
                    </View>
                </View>
            </Modalize>
            <Modalize
                ref={deadlineRef}
                adjustToContentHeight
                childrenStyle={{height: '90%'}}>
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
                            Время выполнения работ
                        </Text>
                        <TouchableOpacity
                            style={{
                                borderRadius: 360,
                                backgroundColor: '#eff1f2',
                            }}
                            onPress={() => {
                                deadlineRef.current?.close();
                                setDate(new Date());
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
                            date={date}
                            style={{alignSelf: 'center'}}
                            mode="time"
                            onDateChange={setDate}/>
                    </View>
                    <View style={{paddingTop: 20}}>
                        <TouchableOpacity 
                            style={[styles.button]}
                            onPress={() => {
                                deadlineRef.current?.close();
                                setDeadline(date);
                                setDate(new Date());
                                setDeadlinetString(`${date.getHours()} часов`);
                                updateDisable({
                                    enrollmentDateString,
                                    enrollmentTimeString,
                                    price,
                                    deadlineString: '.',
                                    prepayment
                                });
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
                    flexDirection: 'row',
                    justifyContent: 'space-between',
                    paddingHorizontal: 10,
                    paddingTop: 20
                }}>
                <TouchableOpacity
                    style={{
                        alignSelf: 'center'
                    }}
                    onPress={() => navigation.goBack()}>
                    <Icon
                        type='material'
                        name='chevron-left'
                        color='#2688EB'
                        size={40}/>
                </TouchableOpacity>
                <Text
                    style={{
                        fontSize: 21,
                        fontWeight: '600',
                        color: 'black',
                        alignSelf: 'center'
                    }}>
                    Ответ на заказ
                </Text>
                <Text></Text>
            </View>
            <View style={{paddingTop: 20, paddingHorizontal: 15}}>
                <CompanyRequestCard
                    orderRequest={orderRequest}
                    button={false}/>        
            </View>
            <View style={{paddingTop: 20, paddingHorizontal: 20}}>
                <Text
                    style={{
                        color: 'black',
                        fontSize: 17,
                        fontWeight: '600'
                    }}> 
                    Клиент хочет узнать:
                </Text>
                {
                    orderRequest.toKnowPrice ? 
                    <>
                        <Text
                            style={{
                                color: '#6D7885', 
                                fontWeight: '400', 
                                fontSize: 14,
                                paddingTop: 20, 
                                paddingBottom: 5
                            }}>
                            Стоимость        
                        </Text>
                        <View>
                            <View style={[styles.textInput]}>
                                <TextInput 
                                    style={[styles.textInputFont]}
                                    keyboardType="numeric"
                                    placeholder="Введите стоимость в рублях" 
                                    value={price}
                                    onChangeText={(text) => {
                                        setPrice(text);
                                        updateDisable({
                                            enrollmentDateString,
                                            enrollmentTimeString,
                                            price: text,
                                            deadlineString,
                                            prepayment
                                        });
                                    }}/>
                            </View>
                        </View>    
                    </>
                    :
                    <></>
                }
                {
                    orderRequest.toKnowDeadline ? 
                    <>
                        <Text
                            style={{
                                color: '#6D7885', 
                                fontWeight: '400', 
                                fontSize: 14,
                                paddingTop: 20,
                                paddingBottom: 5
                            }}>
                            Время выполнения работы        
                        </Text>
                        <View>
                            <View 
                                style={[
                                    styles.textInput, { 
                                        flexDirection: 'row', 
                                        justifyContent: 'space-between' 
                                    }
                                ]}>
                                <Text
                                    style={{
                                        color: deadlineString == '' ? '#818C99' : 'black',
                                        fontSize: 16,
                                        fontWeight: '400',
                                        alignSelf: 'center',
                                        flex: 2,
                                    }}>
                                    {deadlineString == '' ? 'Время выполнения работы' : deadlineString}    
                                </Text>
                                <TouchableOpacity
                                    style={{alignSelf: 'center'}}
                                    onPress={() => deadlineRef.current?.open()}>
                                    <Icon
                                        color='gray'
                                        type='material'
                                        name='expand-more'/>
                                </TouchableOpacity>
                            </View>
                        </View>
                    </>
                    :
                    <></>
                }
                {
                    orderRequest.toKnowEnrollmentDate ? 
                    <>
                        <View
                            style={{
                                flexDirection: 'row',
                                justifyContent: 'space-between',
                                paddingTop: 20,
                            }}>
                            <View style={{flex: 1, paddingRight: 5}}>
                                <Text
                                    style={{
                                        color: '#6D7885', 
                                        fontWeight: '400', 
                                        fontSize: 14,
                                        paddingBottom: 5
                                    }}>
                                    Дата записи    
                                </Text>
                                <View
                                    style={[
                                        styles.textInput, { 
                                            flexDirection: 'row', 
                                            justifyContent: 'space-between' 
                                        }
                                    ]}>
                                    <Text
                                        style={{
                                            color: enrollmentDateString == '' ? '#818C99' : 'black',
                                            fontSize: 16,
                                            fontWeight: '400',
                                            alignSelf: 'center',
                                            flex: 2,
                                        }}
                                        numberOfLines={1}>
                                        {enrollmentDateString == '' ? 'Выбрать' : enrollmentDateString}    
                                    </Text>
                                    <TouchableOpacity
                                        style={{alignSelf: 'center'}}
                                        onPress={() => enrollmentDateRef.current?.open()}>
                                        <Icon
                                            color='gray'
                                            type='material'
                                            name='expand-more'/>
                                    </TouchableOpacity>    
                                </View>
                            </View>
                            <View style={{flex: 1, paddingLeft: 5}}>
                                <Text
                                    style={{
                                        color: '#6D7885', 
                                        fontWeight: '400', 
                                        fontSize: 14,
                                        paddingBottom: 5
                                    }}>
                                    Время записи    
                                </Text>
                                <View
                                    style={[
                                        styles.textInput, { 
                                            flexDirection: 'row', 
                                            justifyContent: 'space-between' 
                                        }
                                    ]}>
                                    <Text
                                        style={{
                                            color: enrollmentTimeString == '' ? '#818C99' : 'black',
                                            fontSize: 16,
                                            fontWeight: '400',
                                            alignSelf: 'center',
                                            flex: 2,
                                        }}
                                        numberOfLines={1}>
                                        {enrollmentTimeString == '' ? 'Выбрать' : enrollmentTimeString}    
                                    </Text>
                                    <TouchableOpacity
                                        style={{alignSelf: 'center'}}
                                        onPress={() => enrollmentTimeRef.current?.open()}>
                                        <Icon
                                            color='gray'
                                            type='material'
                                            name='expand-more'/>
                                    </TouchableOpacity>    
                                </View>
                            </View>    
                        </View>
                    </>
                    :
                    <></>
                }
                {
                    user.prepaymentAvailable ?
                    <>
                        <View style={{paddingTop: 20, justifyContent: 'center'}}>
                            <Text
                                style={{
                                    color: '#6D7885', 
                                    fontWeight: '400', 
                                    fontSize: 14,
                                    paddingBottom: 5
                                }}>
                                Предоплата    
                            </Text>
                            <View style={styles.textInput}>
                                <TextInput
                                    style={styles.textInputFont}
                                    keyboardType="numeric"
                                    placeholder="Введите предоплату в рублях"
                                    value={prepayment}
                                    onChangeText={(text) => {
                                        setPrepayment(text);
                                        updateDisable({
                                            enrollmentDateString,
                                            enrollmentTimeString,
                                            price,
                                            deadlineString,
                                            prepayment: text
                                        });
                                    }}/>
                            </View>
                        </View>
                    </>
                    :
                    <></>
                }
                <View style={{paddingTop: 20, paddingBottom: 10}}>
                    <TouchableOpacity
                        style={[
                            styles.button, {
                                backgroundColor: disable ? '#ABCDf3' : '#2D81E0'
                            }
                        ]}
                        onPress={async () => {
                            let order = {
                                receiverId: orderRequest.client.userId,
                                deadline: orderRequest.toKnowDeadline ? deadline.getHours() : 0,
                                price: orderRequest.toKnowPrice ? Number.parseInt(price) : 0,
                                prepayment: user.prepaymentAvailable ? Number.parseInt(price) : 0,
                                orderRequestId: orderRequest.id,
                                enrollmentTime: orderRequest.toKnowEnrollmentDate ? dateHelper.convertDateToJson(enrollmentDateString, enrollmentTimeString) : null
                            }

                            await orderingService.createOrder(order);

                            navigation.navigate('Chats');
                        }}>
                        <Text
                            style={styles.buttonText}>
                            Отправить
                        </Text>
                    </TouchableOpacity>
                </View>
            </View>
        </ScrollView>
    )
}

export default CompanyRequestCreationScreen;