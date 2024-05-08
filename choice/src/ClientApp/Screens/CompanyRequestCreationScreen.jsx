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

const CompanyRequestCreationScreen = ({navigation, route}) => {
    const { orderRequest } = route.params;
    const [price, setPrice] = React.useState('');
    const [deadLine, setDeadline] = React.useState(new Date());
    const [enrollmentDate, setEnrollmentDate] = React.useState(new Date());
    const [enrollmentTime, setEnrollmentTime] = React.useState(new Date());
    const [firstImageUri, setFirstImageUri] = React.useState('');
    const [secondImageUri, setSecondImageUri] = React.useState('');
    const [thirdImageUri, setThirdImageUri] = React.useState('');

    const [enrollmentDateModalOpen, setEnrollmentDateModalOpen] = React.useState(false);
    const [enrollmentTimeModalOpen, setEnrollmentTimeModalOpen] = React.useState(false);
    const [deadLineModalOpen, setDeadlineModalOpen] = React.useState(false);

    return (
        <ScrollView
            style={{flex: 1, backgroundColor: 'white'}}
            showsVerticalScrollIndicator={false}>
            <DatePicker
                modal
                open={enrollmentDateModalOpen}
                date={enrollmentDate}
                onConfirm={(date) => {
                    setEnrollmentDateModalOpen(false);
                    setEnrollmentDate(date);
                }}
                onCancel={() => {
                    setEnrollmentDateModalOpen(false);    
                }}/>
                <DatePicker
                    modal
                    open={enrollmentTimeModalOpen}
                    date={enrollmentTime}
                    onConfirm={(date) => {
                        setEnrollmentTimeModalOpen(false);
                        setEnrollmentTime(date);
                    }}
                    onCancel={() => {
                        setEnrollmentTimeModalOpen(false);    
                    }}/>
                <DatePicker
                    modal
                    open={enrollmentTimeModalOpen}
                    date={enrollmentTime}
                    onConfirm={(date) => {
                        setEnrollmentTimeModalOpen(false);
                        setEnrollmentTime(date);
                    }}
                    onCancel={() => {
                        setEnrollmentTimeModalOpen(false);    
                    }}/>
                <DatePicker
                    open={deadLineModalOpen}
                    modal
                    date={deadLine}
                    mode="time"
                    onConfirm={(date) => {
                        setDeadlineModalOpen(false);
                        setDeadline(date);
                    }}
                    onCancel={() => {
                        setDeadlineModalOpen(false);    
                    }}/>
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
                        <View style={{paddingBottom: 20}}>
                            <View style={[styles.textInput]}>
                                <TextInput 
                                    style={[styles.textInputFont]}
                                    placeholder="Введите стоимость в рублях" 
                                    value={price}
                                    onChangeText={(text) => {
                                        setPrice(text);
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
                                paddingBottom: 5
                            }}>
                            Время выполнения работы        
                        </Text>
                        <View style={{paddingBottom: 20}}>
                            <View 
                                style={[
                                    styles.textInput, { 
                                        flexDirection: 'row', 
                                        justifyContent: 'space-between' 
                                    }
                                ]}>
                                <Text
                                    style={{
                                        color: '#818C99',
                                        fontSize: 16,
                                        fontWeight: '400',
                                        alignSelf: 'center',
                                        flex: 2,
                                    }}>
                                    Время выполнения работы    
                                </Text>
                                <TouchableOpacity
                                    style={{alignSelf: 'center'}}
                                    onPress={() => setDeadlineModalOpen(true)}>
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
                                paddingBottom: 20
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
                                            color: '#818C99',
                                            fontSize: 16,
                                            fontWeight: '400',
                                            alignSelf: 'center',
                                            flex: 2,
                                        }}
                                        numberOfLines={1}>
                                        Выбрать    
                                    </Text>
                                    <TouchableOpacity
                                        style={{alignSelf: 'center'}}>
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
                                            color: '#818C99',
                                            fontSize: 16,
                                            fontWeight: '400',
                                            alignSelf: 'center',
                                            flex: 2,
                                        }}
                                        numberOfLines={1}>
                                        Выбрать    
                                    </Text>
                                    <TouchableOpacity
                                        style={{alignSelf: 'center'}}>
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
                <Text
                    style={{
                        color: '#6D7885', 
                        fontWeight: '400', 
                        fontSize: 14,
                        paddingBottom: 5
                    }}>
                    Приложите файлы к ответу    
                </Text>
                <View
                    style={{
                        flexDirection: 'row',
                        justifyContent: 'space-between'   
                    }}>
                    <ImageBox
                        uri={firstImageUri}
                        onUriChanged={(text) => setFirstImageUri(text)}/>

                    <ImageBox
                        uri={secondImageUri}
                        onUriChanged={(text) => setSecondImageUri(text)}/>

                    <ImageBox
                        uri={thirdImageUri}
                        onUriChanged={(text) => setThirdImageUri(text)}/>
                </View>
                <View style={{paddingTop: 20, paddingBottom: 10}}>
                    <TouchableOpacity
                        style={styles.button}>
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