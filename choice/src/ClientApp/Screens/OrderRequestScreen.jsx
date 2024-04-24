import React, { useRef } from "react";
import {
    View,
    TouchableOpacity,
    Text,
    TextInput,
    Dimensions,
    ScrollView,
    Image,
    Modal,
    Switch,
    DeviceEventEmitter
} from 'react-native';
import { Icon } from "react-native-elements";
import { Slider } from "react-native-awesome-slider";
import styles from "../Styles";
import ImageBox from "../Components/ImageBox";
import { useSharedValue } from "react-native-reanimated";
import categoryStore from "../services/categoryStore";
import { Modalize } from "react-native-modalize";
import { FlatList } from "react-native-gesture-handler";
import Category from "../Components/Category";
import arrayHelper from "../helpers/arrayHelper";
import clientService from "../services/clientService";
import { opacity } from "react-native-reanimated/lib/typescript/reanimated2/Colors";
import dateHelper from "../helpers/dateHelper";

const OrderRequestScreen = ({navigation, route}) => {
    const { orderRequest } = route.params;

    const categories = categoryStore.getCategories();

    const category = categories[categories.findIndex(c => orderRequest.categoryId == c.id)];

    const [selectedCategory, setSelectedCategory] = React.useState({title: category.title, track: true, id: category.id});

    const modalRef = useRef(null);
    const progress = useSharedValue(orderRequest.searchRadius/1000);
    const min = useSharedValue(5);
    const max = useSharedValue(25);

    const { width, height } = Dimensions.get('screen');

    const [disabled, setDisabled] = React.useState(false);

    const [description, setDescription] = React.useState(orderRequest.description);
    const [toKnowPrice, setToKnowPrice] = React.useState(orderRequest.toKnowPrice);
    const [radius, setRadius] = React.useState(orderRequest.searchRadius/1000);
    const [toKnowDeadline, setToKnowDeadline] = React.useState(orderRequest.toKnowDeadline);
    const [toKnowEnrollmentDate, setToKnowEnrollmentDate] = React.useState(orderRequest.toKnowEnrollmentDate);
    const [fisrtImageUri, setFirstImageUri] = React.useState(orderRequest.photoUris[0]);
    const [secondImageUri, setSecondImageUri] = React.useState(orderRequest.photoUris[1]);
    const [thirdImageUri, setThirdImageUri] = React.useState(orderRequest.photoUris[2]);
    const date = dateHelper.formatDate(orderRequest.creationDate);

    const updateDisabled = (state) => {
        setDisabled((state.description == '' || 
            (!state.toKnowPrice && !state.toKnowDeadline && !state.toKnowEnrollmentDate)));
    }

    const updateState = (state) => {
        orderRequest.description = state.description;
        orderRequest.photoUris = state.photoUris;
        orderRequest.toKnowPrice = state.toKnowPrice;
        orderRequest.toKnowDeadline = state.toKnowDeadline;
        orderRequest.toKnowEnrollmentDate = state.toKnowEnrollmentDate;
        orderRequest.categoryId = state.categoryId;
        orderRequest.searchRadius = state.searchRadius;
    }

    const selectCategory = (newCategory) => {
        if (newCategory.track) {
            setSelectedCategory(newCategory);
            updateDisabled({
                description,
                toKnowPrice,
                toKnowDeadline,
                toKnowEnrollmentDate,
                fisrtImageUri,
                secondImageUri,
                thirdImageUri
            });
        }
    }

    return (
        <ScrollView 
            style={{
                flex:1, 
                backgroundColor: 'white'
            }}
            showsVerticalScrollIndicator={false}>
            <Modalize 
                ref={modalRef}
                adjustToContentHeight={true}
                childrenStyle={{height: '100%'}}>

                <View
                    style={{
                        paddingHorizontal: 15,
                        paddingTop: 10
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
                            onPress={() => modalRef.current?.close()}
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
                    <FlatList
                        data={categories}
                        scrollEnabled={false}
                        style={{ paddingTop: 10 }} 
                        renderItem={({item}) => {
                        return (
                            <Category 
                                category={{
                                    title: item.title,
                                    track: selectedCategory.id == item.id,
                                    id: item.id
                                }}
                                selectCategory={(category) => selectCategory(category)}/>
                        );
                    }}/>
                </View>
            </Modalize>
            <View style={{flexDirection: 'row', justifyContent: 'space-between', paddingTop: 30}}>
                <TouchableOpacity 
                    style={{alignSelf: 'center'}}
                    onPress={() => navigation.goBack()}>
                    <Icon name='chevron-left'
                          type='material'
                          color={'#2688EB'}
                          size={40}/>
                </TouchableOpacity>
                <Text style={{alignSelf: 'center', color: 'black', fontWeight: '600', fontSize: 21}}>{`Заказ №${orderRequest.id}`}</Text>
                <Text></Text>
            </View>
            <View style={{paddingTop: 20, paddingHorizontal: 20}}>
                <View 
                    style={[
                        styles.textInput, 
                        { 
                            flexDirection: 'row', 
                            justifyContent: 'space-between' 
                        }
                    ]}>
                    <Text style={[styles.textInputFont, { alignSelf: 'center' }]}>
                        {dateHelper.formatDate(orderRequest.creationDate)}
                    </Text>
                    <View style={{paddingVertical: 3, justifyContent: 'center'}}>
                        <View
                            style={{
                                backgroundColor: '#6DC876',
                                borderRadius: 8,
                                justifyContent: 'center',
                                padding: 5
                            }}>
                            <Text 
                                style={{
                                    fontWeight: '500',
                                    fontSize: 14,
                                    color: 'white',
                                }}>
                                Активен
                            </Text>
                        </View>
                    </View>
                </View>
            </View>
            <View style={{paddingHorizontal: 20}}>
                <View style={{paddingTop: 20}}>
                    <Text style={{fontSize: 14, fontWeight: '400', color: '#6D7885', paddingBottom: 10}}>Категория услуг</Text>
                    <View style={[styles.textInput, {flexDirection: 'row'}]}>
                        <Text style={[styles.textInputFont, {alignSelf: 'center', flex: 3}]}>{selectedCategory.title}</Text>
                        <View style={{flex: 1, flexDirection: 'row', justifyContent: 'flex-end'}}>
                            <TouchableOpacity 
                                style={{alignSelf: 'center'}}
                                onPress={() => modalRef.current?.open()}>
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
                        <TextInput 
                                style={[styles.textInputFont, {top:0}]}
                                value={description}
                                onChangeText={(value) => { 
                                    setDescription(value);
                                    updateDisabled({
                                        description: value,
                                        toKnowPrice,
                                        toKnowDeadline,
                                        toKnowEnrollmentDate,
                                        fisrtImageUri,
                                        secondImageUri,
                                        thirdImageUri
                                    });
                                }}
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
                        <TouchableOpacity 
                            style={{width: 20, height: 20, borderColor: '#B8C1CC', borderWidth: toKnowPrice ? 0 : 2, backgroundColor: toKnowPrice ? '#2688EB' : 'white', borderRadius: 4, justifyContent: 'center'}}
                            onPress={() => { 
                                setToKnowPrice(!toKnowPrice);
                                updateDisabled({
                                    description,
                                    toKnowPrice: !toKnowPrice,
                                    toKnowDeadline,
                                    toKnowEnrollmentDate,
                                    fisrtImageUri,
                                    secondImageUri,
                                    thirdImageUri
                                });
                            }}>
                            <Icon name='done'
                                  type='material'
                                  color={'white'}
                                  size={15}/>
                        </TouchableOpacity>
                        <Text style={{fontSize: 15, fontWeight: '400', color: 'black', paddingLeft: 10}}>Узнать стоимость</Text>
                    </View>
                    <View style={{flexDirection: 'row', paddingTop: 10}}>
                        <TouchableOpacity 
                            style={{width: 20, height: 20, borderColor: '#B8C1CC', borderWidth: toKnowDeadline ? 0 : 2, backgroundColor: toKnowDeadline ? '#2688EB' : 'white', borderRadius: 4, justifyContent: 'center'}}
                            onPress={() => { 
                                setToKnowDeadline(!toKnowDeadline);
                                updateDisabled({
                                    description,
                                    toKnowPrice,
                                    toKnowDeadline: !toKnowDeadline,
                                    toKnowEnrollmentDate,
                                    fisrtImageUri,
                                    secondImageUri,
                                    thirdImageUri
                                });
                            }}>
                            <Icon name='done'
                                  type='material'
                                  color={'white'}
                                  size={15}/>
                        </TouchableOpacity>
                        <Text style={{fontSize: 15, fontWeight: '400', color: 'black', paddingLeft: 10}}>Узнать время выполнения работ</Text>
                    </View>
                    <View style={{flexDirection: 'row', paddingTop: 10}}>
                        <TouchableOpacity 
                            style={{width: 20, height: 20, borderColor: '#B8C1CC', borderWidth: toKnowEnrollmentDate ? 0 : 2, backgroundColor: toKnowEnrollmentDate ? '#2688EB' : 'white', borderRadius: 4, justifyContent: 'center'}}
                            onPress={() => { 
                                setToKnowEnrollmentDate(!toKnowEnrollmentDate);
                                updateDisabled({
                                    description,
                                    toKnowPrice,
                                    toKnowDeadline,
                                    toKnowEnrollmentDate: !toKnowEnrollmentDate,
                                    fisrtImageUri,
                                    secondImageUri,
                                    thirdImageUri
                                });
                            }}>
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
                        <ImageBox 
                            handleState={(state) => { 
                                setFirstImageUri(state);
                                updateDisabled({
                                    description,
                                    toKnowPrice,
                                    toKnowDeadline,
                                    toKnowEnrollmentDate,
                                    fisrtImageUri: state,
                                    secondImageUri,
                                    thirdImageUri
                                });
                            }}
                            uri={fisrtImageUri}/>
                        <ImageBox 
                            handleState={(state) => { 
                                setSecondImageUri(state);
                                updateDisabled({
                                    description,
                                    toKnowPrice,
                                    toKnowDeadline,
                                    toKnowEnrollmentDate,
                                    fisrtImageUri,
                                    secondImageUri: state,
                                    thirdImageUri
                                });
                            }}
                            uri={secondImageUri}/>
                        <ImageBox 
                            handleState={(state) => { 
                                setThirdImageUri(state);
                                updateDisabled({
                                    description,
                                    toKnowPrice,
                                    toKnowDeadline,
                                    toKnowEnrollmentDate,
                                    fisrtImageUri,
                                    secondImageUri,
                                    thirdImageUri: state
                                }); 
                            }}
                            uri={thirdImageUri}/>
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
                    <TouchableOpacity 
                        style={[styles.button, {backgroundColor: disabled ? '#ABCDf3' : '#2D81E0'}]}
                        disabled={disabled}
                        onPress={!disabled && (async () => {
                            let state = {
                                id: orderRequest.id,
                                description,
                                categoryId: selectedCategory.id,
                                photoUris: [fisrtImageUri, secondImageUri, thirdImageUri],
                                searchRadius: radius,
                                toKnowPrice,
                                toKnowDeadline,
                                toKnowEnrollmentDate
                            }

                            let request = await clientService.changeOrderRequest(state);

                            updateState(request);

                            navigation.navigate('Order');
                        })}>
                        <Text style={styles.buttonText}>Сохранить изменения</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </ScrollView>
    );
}

export default OrderRequestScreen;