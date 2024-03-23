import React from "react";
import {
  SafeAreaView,
  TextInput,
  View,
  Dimensions
} from 'react-native';

const {width, height} = Dimensions.get('screen');

export default function LoginByPhoneScreen() {
    return (
        <SafeAreaView style={{alignContent: 'center'}}>
            <View style={{paddingHorizontal: 20}}>
                <TextInput style={{backgroundColor: '#f2f3f5', borderRadius: 10, borderColor: '#d5d5d7', borderWidth: 1, height: height/18}}/>
            </View>
        </SafeAreaView>
    );
}