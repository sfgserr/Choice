import React from "react";
import styles from "../Styles.jsx";
import authService from "../services/authService.js";
import categoryService from "../services/categoryService.js";
import { Icon } from "react-native-elements";
import {
  SafeAreaView,
  Text,
  View,
  TextInput,
  TouchableOpacity
} from 'react-native';
import blobService from "../services/blobService.js";

export default function LoginByEmailScreen({navigation}) {
    const [email, setEmail] = React.useState('');
    const [password, setPassword] = React.useState('');

    const [hidePassword, setHidePassword] = React.useState(true);
    const [disabled, setDisabled] = React.useState(true);

    const onPress = () => {
        setHidePassword(!hidePassword);
    }

    const installImages = async (categories) => {
        await categories.forEach(async c => {
            await blobService.getImage(c.iconUri);
        })
    }

    const login = async () => {
        let result = await authService.loginByEmail(email, password);
        
        if (result) {
            let categories = await categoryService.getCategories();
            await installImages(categories);
            navigation.navigate('Category', { categories: categories });
        }
    }

    const onEmailChanged = (email) => {
        if (email == '' || password == '') { 
            setDisabled(true);
        }
        else {
            setDisabled(false);
        }

        setEmail(email);
    }

    const onPasswordChanged = (password) => {
        if (email == '' || password == '') { 
            setDisabled(true);
        }
        else {
            setDisabled(false);
        }

        setPassword(password);
    }

    return (
        <SafeAreaView>
             <View style={{paddingHorizontal: 20}}>
                <Text style={{color: '#6D7885', fontWeight: '400', fontSize: 14, paddingBottom: 5}}>E-mail</Text>
                <View style={styles.textInput}>
                    <TextInput placeholder="E-mail" value={email} onChangeText={onEmailChanged} style={styles.textInputFont}/>
                </View>

                <Text style={{color: '#6D7885', fontWeight: '400', fontSize: 14, paddingBottom: 5, paddingTop: 30}}>Пароль</Text>
                <View style={[styles.textInput, {flexDirection: 'row'}]}>
                    <TextInput placeholder="Пароль" value={password} onChangeText={onPasswordChanged} secureTextEntry={hidePassword} style={[styles.textInputFont, {flex: 3}]}/>
                    <View style={{flex: 1, flexDirection: 'row', justifyContent: 'flex-end'}}>
                        <TouchableOpacity style={{alignSelf: 'center'}} onPress={onPress}>
                            <Icon type='material-community'
                                  color='gray'
                                  name={hidePassword ? 'eye' : 'eye-off'}/>
                        </TouchableOpacity>
                    </View>
                </View>
                <View style={{paddingTop: 30}}>
                    <TouchableOpacity onPress={!disabled && login} disabled={disabled} style={[styles.button, {backgroundColor: disabled ? '#ABCDf3' : '#2D81E0'}]}>
                        <Text style={[styles.buttonText, {alignSelf: 'center'}]}>Войти</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </SafeAreaView>
    );
}