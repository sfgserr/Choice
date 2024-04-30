import React from "react";
import {
    View,
    Text,
    Dimensions
} from 'react-native';

const FillCompanyDataScreen = () => {
    const { width, height } = Dimensions.get('screen');

    return (
        <View
            style={{
                flex: 1,
                backgroundColor: 'white'
            }}>
            <Text
                style={{
                    color: 'black',
                    fontSize: 21,
                    fontWeight: '600',
                    paddingTop: 20,
                    alignSelf: 'center'
                }}>
                Карточкая компании
            </Text>
            <View style={{paddingHorizontal: 20}}>
                <View
                    style={{
                        paddingTop: 30,
                        justifyContent: 'space-between',
                        flexDirection: 'row'
                    }}>
                    <View
                        style={{
                            height: 4.5,
                            width: width/3.5,
                            borderRadius: 10,
                            backgroundColor: '#DFDFDF'
                        }}/> 
                    <View
                        style={{
                            height: 4.5,
                            width: width/3.5,
                            borderRadius: 10,
                            backgroundColor: '#DFDFDF'
                        }}/>
                    <View
                        style={{
                            height: 4.5,
                            width: width/3.5,
                            borderRadius: 20,
                            backgroundColor: '#DFDFDF'
                        }}/>   
                </View>
                <View style={{paddingTop: 15}}>
                    <View style={{height: .6, backgroundColor: 'black'}}/>
                </View>
            </View>
        </View>
    )
}

export default FillCompanyDataScreen;