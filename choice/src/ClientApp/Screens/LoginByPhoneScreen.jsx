import React from "react";
import styles from '../Styles.jsx';
import {
  SafeAreaView,
  TextInput,
  Text,
  View
} from 'react-native';

export default function LoginByPhoneScreen() {
    return (
        <SafeAreaView>
            <View style={{paddingHorizontal: 20}}>
                <Text style={{color: '#6D7885', fontWeight: '400', fontSize: 14, paddingBottom: 5}}>Телефон</Text>
                <TextInput style={styles.textInput}/>
            </View>
        </SafeAreaView>
    );
}