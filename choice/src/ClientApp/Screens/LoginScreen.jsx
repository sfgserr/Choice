import React from "react";
import LoginByEmailScreen from "./LoginByEmailScreen";
import LoginByPhoneScreen from "./LoginByPhoneScreen";
import {
  Image,
  SafeAreaView,
  View,
  Text,
  findNodeHandle,
  Animated,
  FlatList,
  Dimensions,
  TouchableOpacity
} from 'react-native';
import Tabs from "../Components/Tabs";

const screens = {
  loginByEmail: { screen: LoginByEmailScreen, title: 'E-mail' },
  loginByPhone: { screen: LoginByPhoneScreen, title: 'Телефон' }
};

const {width, height} = Dimensions.get('screen');  
const data = Object.keys(screens).map((i) => ({
    key: i,
    title: screens[i].title,
    screen: screens[i].screen,
    ref: React.createRef()
}));

export default function LoginScreen({ navigation }) {
    const scrollX = React.useRef(new Animated.Value(0)).current;
    const ref = React.useRef();
    const onItemPress = React.useCallback(itemIndex => {
        ref?.current?.scrollToOffset({
            offset: itemIndex * width
        });
    });
    return (
        <SafeAreaView style={{flex:1, flexDirection: 'column', justifyContent: 'center', backgroundColor: 'white'}}>
            <View style={{alignSelf: 'center', paddingTop: 20}}>
                <Image style={{width: 150, height: 150, resizeMode: 'contain', alignSelf: 'center'}}
                       source={require('../assets/choice-logo.png')}/>
                <Text style={{fontSize: 20, color: '#313131', fontWeight: '600', alignSelf: 'center', paddingTop: 20}}>ВЫБОР</Text>
                <Text style={{fontSize: 16, paddingTop: 10}}>Приложение для выбора</Text>
                <Text style={{fontSize: 16, alignSelf: 'center'}}>лучших условий</Text>
            </View>
            <View style={{flexDirection: 'row', paddingHorizontal: 20, paddingTop: 40}}>
                <Text style={{fontSize: 24, fontWeight: '700', color: '#313131'}}>Авторизация</Text>
                <View style={{flex: 1, justifyContent: 'flex-end', flexDirection: 'row'}}>
                    <TouchableOpacity style={{alignSelf: 'center'}}>
                        <Text style={{color: '#2D81E0', fontSize: 16, fontWeight: '400'}}>Создать аккаунт</Text>
                    </TouchableOpacity>
                </View>
            </View>
            <Animated.FlatList data={data}
                               ref={ref}
                               keyExtractor={(item) => item.key}
                               horizontal
                               pagingEnabled
                               style={{paddingTop: 30}}
                               bounces={false}
                               onScroll={Animated.event(
                                [{nativeEvent: {contentOffset: {x: scrollX}}}],
                                { useNativeDriver: false }
                               )}
                               showsHorizontalScrollIndicator={false}
                               renderItem={({item}) => {
                               return <View style={{width, paddingTop: 40}}>
                                 <item.screen navigation={navigation}/>
                               </View>
                            }}/>
            <Tabs scrollX={scrollX} data={data} onItemPress={onItemPress}/>
        </SafeAreaView>
    )
}