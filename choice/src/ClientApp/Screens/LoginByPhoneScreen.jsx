import React from "react";
import styles from '../Styles.jsx';
import {
  SafeAreaView,
  TextInput,
  Text,
  View,
  TouchableOpacity
} from 'react-native';

export default function LoginByPhoneScreen() {
    const [phone, setPhone] = React.useState('');
    const [disabled, setDisabled] = React.useState(true);

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
            <View style={[{paddingHorizontal: 20}]}>
                <Text style={{color: '#6D7885', fontWeight: '400', fontSize: 14, paddingBottom: 5}}>Телефон</Text>
                <View style={styles.textInput}>
                    <TextInput onChangeText={onPhoneChanged} placeholder="+7 (000) 000-00-00" style={styles.textInputFont}/>
                </View>
                <View style={{paddingTop: 30}}>
                    <TouchableOpacity disabled={disabled} style={[styles.button, {backgroundColor: disabled ? '#ABCDf3' : '#2D81E0'}]}>
                        <Text style={[styles.buttonText, {alignSelf: 'center'}]}>Отправить код</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </SafeAreaView>
    );
}