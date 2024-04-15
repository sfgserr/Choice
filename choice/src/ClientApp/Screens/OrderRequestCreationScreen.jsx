import React from "react";
import {
    View,
    TouchableOpacity,
    Text,
    TextInput,
    Dimensions,
    ScrollView,
    Image,
    Modal
} from 'react-native';
import { Icon } from "react-native-elements";
import { Slider } from "react-native-awesome-slider";
import styles from "../Styles";
import * as ImagePicker from 'react-native-image-picker';
import ImageBox from "../Components/ImageBox";
import { useSharedValue } from "react-native-reanimated";
import categoryStore from "../services/categoryStore";

const OrderRequestCreationScreen = ({ navigation, route }) => {
    const categories = categoryStore.getCategories();

    const { category } = route.params;
    const { width, height } = Dimensions.get('screen');
    const [fisrtImageUri, setFirstImageUri] = React.useState('');
    const [secondImageUri, setSecondImageUri] = React.useState('');
    const [thirdImageUri, setThirdImageUri] = React.useState('');

    const progress = useSharedValue(10);
    const min = useSharedValue(5);
    const max = useSharedValue(25);

    const [categoriesString, setCategoriesString] = React.useState(category.title);
    const [description, setDescription] = React.useState('');
    const [toKnowPrice, setToKnowPrice] = React.useState(false);
    const [radius, setRadius] = React.useState(10);
    const [toKnowDeadLine, setToKnowDeadLine] = React.useState(false);
    const [toKnowEnrollmentTime, setToKnowEnrollmentTime] = React.useState(false);

    return (
        <ScrollView 
            style={{
                flex:1, 
                backgroundColor: 'white'
            }}
            showsVerticalScrollIndicator={false}>
            <Modal 
                animationType="slide"
                transparent={true}>
                <View 
                    style={{
                        height: '50%',
                        marginTop: 'auto',
                        backgroundColor:'white',
                        borderTopLeftRadius: 10,
                        borderTopRightRadius: 10,
                        borderWidth: 1,
                        paddingHorizontal: 10
                    }}>        
                    <View
                        style={{
                            flexDirection: 'row',
                            justifyContent: 'space-between',
                            paddingTop: 10,
                        }}>
                        <Text></Text>
                        <Text 
                            style={{
                                color: 'black',
                                fontSize: 21,
                                fontWeight: '600',
                            }}>
                            Категория услуг
                        </Text>
                        <TouchableOpacity
                            style={{
                                borderRadius: 360,
                                backgroundColor: '#eff1f2',
                            }}>
                            <Icon 
                                name='close'
                                type='material'
                                size={27}
                                color='#818C99'/>
                        </TouchableOpacity>
                    </View>
                </View>
            </Modal>
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
                        <Text style={[styles.textInputFont, {alignSelf: 'center', flex: 3}]}>{categoriesString}</Text>
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
                    style={{
                        paddingTop: 30,
                    }}>
                    <View 
                        style={{
                            flexDirection: 'row',
                            justifyContent: 'space-between'
                        }}>
                        <Text 
                            style={{
                                fontSize: 14, 
                                fontWeight: '400', 
                                color: '#6D7885', 
                                paddingBottom: 20
                            }}>
                            Радиус поиска
                        </Text>
                        <Text
                            style={{
                                fontSize: 14,
                                color: 'black',
                                fontWeight: '600'
                            }}>
                            {`${radius} км`}
                        </Text>
                    </View>
                    <Slider minimumValue={min}
                            progress={progress}
                            maximumValue={max}
                            onValueChange={(value) => setRadius(Math.floor(value))}
                            theme={{
                                disableMinTrackTintColor: '#007AFF',
                                maximumTrackTintColor: '#78788033',
                                minimumTrackTintColor: '#007AFF',
                            }}
                            renderBubble={() => {}}
                            renderThumb={() => (
                                <View 
                                    style={{
                                        borderRadius: 360,
                                        backgroundColor: 'white',
                                        width: 25,
                                        height: 25,
                                        borderWidth: 1,
                                        borderColor: '#78788033'
                                    }}>

                                </View>
                            )}/>
                    <View
                        style={{
                            flexDirection: 'row',
                            justifyContent: 'space-between',
                            paddingTop: 20
                        }}>
                        <Text
                            style={{
                                fontSize: 14, 
                                fontWeight: '400', 
                                color: '#6D7885', 
                            }}>
                            от 5 км
                        </Text>
                        <Text
                            style={{
                                fontSize: 14, 
                                fontWeight: '400', 
                                color: '#6D7885', 
                            }}>
                            до 25 км
                        </Text>
                    </View>
                </View>
                <View 
                    style={{
                        paddingTop: 30,
                        paddingBottom: 10
                    }}>
                    <TouchableOpacity style={styles.button}>
                        <Text style={styles.buttonText}>Создать заказ</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </ScrollView>
    )
}

export default OrderRequestCreationScreen;