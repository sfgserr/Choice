import React from "react";
import {
    View,
    TouchableOpacity,
    Text,
    TextInput,
    Dimensions,
    ScrollView,
    Image
} from 'react-native';
import { Icon } from "react-native-elements";
import styles from "../Styles";
import * as ImagePicker from 'react-native-image-picker';
import ImageBox from "../Components/ImageBox";

const OrderRequestCreationScreen = ({ navigation, route }) => {
    const { category } = route.params;
    const { width, height } = Dimensions.get('screen');
    const [fisrtImageUri, setFirstImageUri] = React.useState('');
    const [secondImageUri, setSecondImageUri] = React.useState('');
    const [thirdImageUri, setThirdImageUri] = React.useState('');

    const [categories, setCategories] = React.useState(category.title);
    const [description, setDescription] = React.useState('');
    const [toKnowPrice, setToKnowPrice] = React.useState(false);
    const [toKnowDeadLine, setToKnowDeadLine] = React.useState(false);
    const [toKnowEnrollmentTime, setToKnowEnrollmentTime] = React.useState(false);

    return (
        <ScrollView 
            style={{
                flex:1, 
                backgroundColor: 'white'
            }}
            showsVerticalScrollIndicator={false}>
            <View style={{flexDirection: 'row', justifyContent: 'space-between', paddingTop: 30}}>
                <TouchableOpacity 
                    style={{alignSelf: 'center'}}
                    onPress={() => navigation.goBack()}>
                    <Icon name='chevron-left'
                          type='material'
                          color={'#2688EB'}
                          size={40}/>
                </TouchableOpacity>
                <Text style={{alignSelf: 'center', color: 'black', fontWeight: '600', fontSize: 21}}>Создание заказа</Text>
                <Text></Text>
            </View>
            <View style={{paddingHorizontal: 20}}>
                <View style={{paddingTop: 20}}>
                    <Text style={{fontSize: 14, fontWeight: '400', color: '#6D7885', paddingBottom: 10}}>Категория услуг</Text>
                    <View style={[styles.textInput, {flexDirection: 'row'}]}>
                        <Text style={[styles.textInputFont, {alignSelf: 'center', flex: 3}]}>{categories}</Text>
                        <View style={{flex: 1, flexDirection: 'row', justifyContent: 'flex-end'}}>
                            <TouchableOpacity style={{alignSelf: 'center'}}>
                                <Icon type='material'
                                    color='gray'
                                    name='expand-more'/>
                            </TouchableOpacity>
                        </View>
                    </View>
                </View>
                <View style={{paddingTop: 30}}>
                    <Text style={{fontSize: 14, fontWeight: '400', color: '#6D7885', paddingBottom: 10}}>Описание задачи</Text>
                    <View style={[styles.textInput, {height: height/7}]}>
                        <TextInput style={[styles.textInputFont, {top:0}]}
                                   value={description}
                                   onChange={(value) => setDescription(value)}
                                   multiline={true}
                                   placeholder="Введите подробности задачи, в чем вам нужна помощь и какой вы ожидаете результат"/>
                    </View>
                </View>
                <View style={{paddingTop: 10}}>
                    <TouchableOpacity style={{backgroundColor: '#F2F3F5', height: height/18, borderRadius: 10, justifyContent: 'center'}}>
                        <View style={{flexDirection: 'row', justifyContent: 'center'}}>
                            <Icon name='mic'
                                 type='material'
                                 color='#3F8AE0'/>
                            <Text style={{color: '#2688EB', fontSize: 17, fontWeight: '500', alignSelf: 'center'}}>Записать голосом</Text>
                        </View>
                    </TouchableOpacity>
                </View>
                <View style={{paddingTop:30}}>
                    <Text style={{fontSize: 14, fontWeight: '400', color: '#6D7885', paddingBottom: 10}}>Что узнать у продавца</Text>
                    <View style={{flexDirection: 'row'}}>
                        <TouchableOpacity style={{width: 20, height: 20, borderColor: '#B8C1CC', borderWidth: toKnowPrice ? 0 : 2, backgroundColor: toKnowPrice ? '#2688EB' : 'white', borderRadius: 4, justifyContent: 'center'}}
                                          onPress={() => setToKnowPrice(!toKnowPrice)}>
                            <Icon name='done'
                                  type='material'
                                  color={'white'}
                                  size={15}/>
                        </TouchableOpacity>
                        <Text style={{fontSize: 15, fontWeight: '400', color: 'black', paddingLeft: 10}}>Узнать стоимость</Text>
                    </View>
                    <View style={{flexDirection: 'row', paddingTop: 10}}>
                        <TouchableOpacity style={{width: 20, height: 20, borderColor: '#B8C1CC', borderWidth: toKnowDeadLine ? 0 : 2, backgroundColor: toKnowDeadLine ? '#2688EB' : 'white', borderRadius: 4, justifyContent: 'center'}}
                                          onPress={() => setToKnowDeadLine(!toKnowDeadLine)}>
                            <Icon name='done'
                                  type='material'
                                  color={'white'}
                                  size={15}/>
                        </TouchableOpacity>
                        <Text style={{fontSize: 15, fontWeight: '400', color: 'black', paddingLeft: 10}}>Узнать время выполнения работ</Text>
                    </View>
                    <View style={{flexDirection: 'row', paddingTop: 10}}>
                        <TouchableOpacity style={{width: 20, height: 20, borderColor: '#B8C1CC', borderWidth: toKnowEnrollmentTime ? 0 : 2, backgroundColor: toKnowEnrollmentTime ? '#2688EB' : 'white', borderRadius: 4, justifyContent: 'center'}}
                                          onPress={() => setToKnowEnrollmentTime(!toKnowEnrollmentTime)}>
                            <Icon name='done'
                                  type='material'
                                  color={'white'}
                                  size={15}/>
                        </TouchableOpacity>
                        <Text style={{fontSize: 15, fontWeight: '400', color: 'black', paddingLeft: 10}}>Узнать время записи</Text>
                    </View>
                </View>
                <View style={{paddingTop: 30}}>
                    <Text style={{fontSize: 14, fontWeight: '400', color: '#6D7885', paddingBottom: 10}}>Приложите файлы к заказу</Text>
                    <View style={{flexDirection: 'row', justifyContent: 'space-between'}}>
                        <ImageBox handleState={(state) => setFirstImageUri(state)}/>
                        <ImageBox handleState={(state) => setSecondImageUri(state)}/>
                        <ImageBox handleState={(state) => setThirdImageUri(state)}/>
                    </View>
                </View>
                <View 
                    style={{paddingTop: 30}}>
                        <Text 
                            style={{
                                fontSize: 14, 
                                fontWeight: '400', 
                                color: '#6D7885', 
                                paddingBottom: 10
                            }}>
                            Радиус поиска
                        </Text>
                </View>
            </View>
        </ScrollView>
    )
}

export default OrderRequestCreationScreen;