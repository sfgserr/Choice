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

const CompanyRequestCreationScreen = ({navigation, route}) => {
    const { orderRequest } = route.params;
    const [price, setPrice] = React.useState('');

    return (
        <ScrollView
            style={{flex: 1, backgroundColor: 'white'}}
            showsVerticalScrollIndicator={false}>
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
            </View>
        </ScrollView>
    )
}

export default CompanyRequestCreationScreen;