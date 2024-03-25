import React from "react";
import styles from '../Styles.jsx';
import {
  SafeAreaView,
  TextInput,
  Text,
  View,
  TouchableOpacity
} from 'react-native';
import authService from "../services/authService.js";

export default function LoginByPhoneScreen() {
    const [codeSent, setCodeSent] = React.useState(false);
    const [phone, setPhone] = React.useState('');
    const [disabled, setDisabled] = React.useState(true);
    const [code, setCode] = React.useState('');
    const [loginDisabled, setLoginDisabled] = React.useState(true);

    const onSendCodePressed = async () => {
        let result = await authService.loginByPhone(phone);
        setCodeSent(result);
    }

    const onVerifyCodePressed = async () => {
        await authService.verifyCode(phone, code);
    }

    const onCodeChanged = (code) => {
        if (code == '') {
            setLoginDisabled(true);
        }
        else {
            setLoginDisabled(false);
        }

        setCode(code);
    }

    const onPhoneChanged = (phone) => {
        if (phone == '') {
            setDisabled(true);
        }
        else {
            setDisabled(false);
        }

        setPhone(phone);
    }

    return (
        <SafeAreaView>
            {codeSent ?  
                <View style={{paddingHorizontal: 20}}>
                    <Text style={{color: '#6D7885', fontWeight: '400', fontSize: 14, paddingBottom: 5}}>Код</Text>
                    <View style={styles.textInput}>
                        <TextInput onChangeText={onCodeChanged} value={code} placeholder="Введите код из смс" style={styles.textInputFont}/>
                    </View>
                    <View style={{paddingTop: 30}}>
                        <TouchableOpacity onPress={onVerifyCodePressed} disabled={loginDisabled} style={[styles.button, {backgroundColor: loginDisabled ? '#ABCDf3' : '#2D81E0'}]}>
                            <Text style={[styles.buttonText, {alignSelf: 'center'}]}>Войти</Text>
                        </TouchableOpacity>
                    </View>
                </View>
                : 
                <View style={[{paddingHorizontal: 20}]}>
                    <Text style={{color: '#6D7885', fontWeight: '400', fontSize: 14, paddingBottom: 5}}>Телефон</Text>
                    <View style={styles.textInput}>
                        <TextInput onChangeText={onPhoneChanged} value={phone} placeholder="+7 (000) 000-00-00" style={styles.textInputFont}/>
                    </View>
                    <View style={{paddingTop: 30}}>
                        <TouchableOpacity onPress={onSendCodePressed} disabled={disabled} style={[styles.button, {backgroundColor: disabled ? '#ABCDf3' : '#2D81E0'}]}>
                            <Text style={[styles.buttonText, {alignSelf: 'center'}]}>Отправить код</Text>
                        </TouchableOpacity>
                    </View>
                </View>
            }
        </SafeAreaView>
    );
}