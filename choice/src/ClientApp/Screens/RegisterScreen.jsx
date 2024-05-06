import React from 'react';
import {
    View,
    Text,
    TextInput,
    Dimensions,
    ScrollView,
    Modal,
    TouchableOpacity
} from 'react-native';
import styles from '../Styles';
import { Icon } from 'react-native-elements';
import authService from '../services/authService';
import categoryStore from '../services/categoryStore';

const RegisterScreen = ({navigation, route}) => {
    const { type } = route.params;

    const { width, height } = Dimensions.get('screen');

    const [modalVisible, setModalVisible] = React.useState(false);

    const [hidePassword, setHidePassword] = React.useState(true);
    const [hideConfirmPassword, setHideConfirmPassword] = React.useState(true);

    const [firstName, setFirstName] = React.useState('');
    const [lastName, setLastName] = React.useState('');
    const [title, setTitle] = React.useState('');
    const [phone, setPhone] = React.useState('');
    const [email, setEmail] = React.useState('');
    const [address, setAddress] = React.useState('');
    const [password, setPassword] = React.useState('');
    const [confirmPassword, setConfirmPassword] = React.useState('');

    const [disable, setDisable] = React.useState(true);

    const updateState = (state) => {
        setDisable(state.includes(''));
    }

    return (
        <ScrollView
            style={{
                flex: 1,
                backgroundColor: 'white',
                paddingHorizontal: 20
            }}
            showsVerticalScrollIndicator={false}>
            <Modal
                visible={modalVisible}
                transparent={true}>
                <View
                    style={{
                        height,
                        width,
                        backgroundColor: 'rgba(0,0,0,0.5)',
                    }}>
                    <View
                        style={{
                            backgroundColor: 'white',
                            width: '90%',
                            borderRadius: 20,
                            alignSelf: 'center',
                            position: 'absolute',
                            bottom: height/14
                        }}>
                        <View 
                            style={{
                                flex: 1,
                                flexDirection: 'column'
                            }}>
                            <View 
                                style={{
                                    flexDirection: 'row',
                                    justifyContent: 'flex-end',
                                    paddingTop: 20,
                                    paddingHorizontal: 10
                                }}>
                                <TouchableOpacity
                                    onPress={() => {
                                        navigation.goBack();
                                    }}
                                    style={{
                                        borderRadius: 360,
                                        backgroundColor: '#eff1f2',
                                        alignSelf: 'flex-start'
                                    }}>
                                    <Icon 
                                        name='close'
                                        type='material'
                                        size={27}
                                        color='#818C99'/>
                                </TouchableOpacity>
                            </View>
                            <View
                                style={{
                                    justifyContent: 'center',
                                }}>
                                <Icon 
                                    name='thumb-up'
                                    type='material'
                                    color='#2D81E0'
                                    size={40}/>
                                <Text
                                    style={{
                                        color: 'black',
                                        fontWeight: '500',
                                        fontSize: 20,
                                        alignSelf: 'center',
                                        paddingTop: 10
                                        
                                    }}>
                                    {type == 'client' ? 'Аккаунт создан' : 'Аккаунт компании создан'}
                                </Text>
                                <Text 
                                    style={{
                                        paddingTop: 10,
                                        color: '#6D7885',
                                        fontSize: 14,
                                        fontWeight: '400',
                                        alignSelf: 'center'
                                    }}>
                                    {type == 'client' ? 'Теперь вы можете создавать заказы' : 'Заполните информацию о вашей компании'}    
                                </Text>
                                <View
                                    style={{
                                        paddingTop: 10,
                                        paddingBottom: 10,
                                        paddingHorizontal: 10
                                    }}>
                                    <TouchableOpacity 
                                        style={[styles.button, {borderRadius: 10}]}
                                        onPress={async () => {
                                            setModalVisible(false);
                                            if (type == 'client') {
                                                navigation.goBack();
                                            }
                                            else {
                                                await authService.loginByEmail(email, password);
                                                await categoryStore.retrieveData();
                                                
                                                navigation.navigate('FillCompanyData', {
                                                    email,
                                                    password
                                                });
                                            }
                                        }}>
                                        <Text style={styles.buttonText}>
                                            {type == 'client' ? 'Ок' : 'Заполнить информацию'}
                                        </Text>
                                    </TouchableOpacity>
                                </View>
                            </View>
                        </View>
                    </View>
                </View>
            </Modal>
            <Text
                style={{
                    color: '#313131',
                    fontWeight: '700',
                    fontSize: 24,
                    paddingTop: 30
                }}>
                {type == 'client' ? 'Регистрация клиента' : 'Регистрация компании'}
            </Text>
            <View
                style={{
                    paddingTop: 20
                }}>
                {
                    type == 'client' ?
                    <>
                        <Text
                            style={{
                                color: '#6D7885', 
                                fontWeight: '400', 
                                fontSize: 14, 
                                paddingBottom: 5
                            }}>
                            Имя        
                        </Text>
                        <View style={{paddingBottom: 20}}>
                            <View style={[styles.textInput]}>
                                <TextInput 
                                    style={[styles.textInputFont]}
                                    value={firstName}
                                    placeholder='Введите имя' 
                                    onChangeText={(text) => {
                                        setFirstName(text);
                                        updateState([
                                            text, 
                                            lastName, 
                                            email, 
                                            phone, 
                                            address, 
                                            password, 
                                            confirmPassword]);
                                    }}/>
                            </View>
                        </View>
                        <Text
                            style={{
                                color: '#6D7885', 
                                fontWeight: '400', 
                                fontSize: 14, 
                                paddingBottom: 5
                            }}>
                            Фамилия        
                        </Text>
                        <View style={{paddingBottom: 20}}>
                            <View style={[styles.textInput]}>
                                <TextInput 
                                    style={[styles.textInputFont]}
                                    placeholder='Введите фамилию' 
                                    onChangeText={(text) => {
                                        setLastName(text);
                                        updateState([
                                            firstName, 
                                            text, 
                                            email, 
                                            phone, 
                                            address, 
                                            password, 
                                            confirmPassword]);
                                    }}/>
                            </View>
                        </View>
                    </>
                    :
                    <>
                        <Text
                            style={{
                                color: '#6D7885', 
                                fontWeight: '400', 
                                fontSize: 14, 
                                paddingBottom: 5
                            }}>
                            Название        
                        </Text>
                        <View style={{paddingBottom: 20}}>
                            <View style={[styles.textInput]}>
                                <TextInput 
                                    style={[styles.textInputFont]}
                                    placeholder='Введите название компании'
                                    onChangeText={(text) => {
                                        setTitle(text);
                                        updateState([
                                            text,
                                            email, 
                                            phone, 
                                            address, 
                                            password, 
                                            confirmPassword]);
                                    }}/>
                            </View>
                        </View>    
                    </>
                }
                <Text
                    style={{
                        color: '#6D7885', 
                        fontWeight: '400', 
                        fontSize: 14, 
                        paddingBottom: 5
                    }}>
                    E-mail        
                </Text>
                <View style={{paddingBottom: 20}}>
                    <View style={[styles.textInput]}>
                        <TextInput 
                            style={[styles.textInputFont]}
                            placeholder='Введите E-mail' 
                            onChangeText={(text) => {
                                setEmail(text);
                                let state = [
                                    text,
                                    phone,
                                    address,
                                    password,
                                    confirmPassword
                                ]

                                if (type == 'client') {
                                    state.push(firstName);
                                    state.push(lastName)
                                }
                                else {
                                    state.push(title);
                                }

                                updateState(state);
                            }}/>
                    </View>
                </View>
                <Text
                    style={{
                        color: '#6D7885', 
                        fontWeight: '400', 
                        fontSize: 14, 
                        paddingBottom: 5
                    }}>
                    Телефон        
                </Text>
                <View style={{paddingBottom: 20}}>
                    <View style={[styles.textInput]}>
                        <TextInput 
                            style={[styles.textInputFont]}
                            keyboardType='phone-pad'
                            placeholder='+7 (999) 999-99-99' 
                            onChangeText={(text) => {
                                setPhone(text);
                                let state = [
                                    email,
                                    text,
                                    address,
                                    password,
                                    confirmPassword
                                ]

                                if (type == 'client') {
                                    state.push(firstName);
                                    state.push(lastName)
                                }
                                else {
                                    state.push(title);
                                }

                                updateState(state);
                            }}/>
                    </View>
                </View>
                <Text
                    style={{
                        color: '#6D7885', 
                        fontWeight: '400', 
                        fontSize: 14, 
                        paddingBottom: 5
                    }}>
                    Адрес        
                </Text>
                <View style={{paddingBottom: 20}}>
                    <View style={[styles.textInput, {height: height/7}]}>
                        <TextInput 
                            style={[styles.textInputFont]}
                            placeholder='Введите адрес' 
                            onChangeText={(text) => {
                                setAddress(text);
                                let state = [
                                    email,
                                    phone,
                                    text,
                                    password,
                                    confirmPassword
                                ]

                                if (type == 'client') {
                                    state.push(firstName);
                                    state.push(lastName)
                                }
                                else {
                                    state.push(title);
                                }

                                updateState(state);
                            }}/>
                    </View>
                </View>
                <Text
                    style={{
                        color: '#6D7885', 
                        fontWeight: '400', 
                        fontSize: 14, 
                        paddingBottom: 5
                    }}>
                    Пароль        
                </Text>
                <View style={{paddingBottom: 20}}>
                    <View style={[styles.textInput, {flexDirection: 'row'}]}>
                        <TextInput 
                            placeholder="Введите пароль" 
                            value={password}
                            secureTextEntry={hidePassword} 
                            onChangeText={(text) => {
                                setPassword(text);
                                let state = [
                                    email,
                                    phone,
                                    address,
                                    text,
                                    confirmPassword
                                ]

                                if (type == 'client') {
                                    state.push(firstName);
                                    state.push(lastName)
                                }
                                else {
                                    state.push(title);
                                }

                                updateState(state);
                            }} 
                            style={[styles.textInputFont, {flex: 3}]}/>
                        <View 
                            style={{
                                flex: 1, 
                                flexDirection: 'row', 
                                justifyContent: 'flex-end'
                            }}>
                            <TouchableOpacity 
                                style={{alignSelf: 'center'}} 
                                onPress={() => {setHidePassword(prev => !prev)}}>
                                <Icon 
                                    type='material-community'
                                    color='gray'
                                    name={hidePassword ? 'eye' : 'eye-off'}/>
                            </TouchableOpacity>
                        </View>
                    </View>
                </View>
                <Text
                    style={{
                        color: '#6D7885', 
                        fontWeight: '400', 
                        fontSize: 14, 
                        paddingBottom: 5
                    }}>
                    Повторите пароль        
                </Text>
                <View style={{paddingBottom: 20}}>
                    <View style={[styles.textInput, {flexDirection: 'row'}]}>
                        <TextInput 
                            placeholder="Введите пароль" 
                            value={confirmPassword}
                            secureTextEntry={hideConfirmPassword} 
                            onChangeText={(text) => {
                                setConfirmPassword(text);
                                let state = [
                                    email,
                                    phone,
                                    address,
                                    password,
                                    text
                                ]

                                if (type == 'client') {
                                    state.push(firstName);
                                    state.push(lastName)
                                }
                                else {
                                    state.push(title);
                                }

                                updateState(state);
                            }} 
                            style={[styles.textInputFont, {flex: 3}]}/>
                        <View 
                            style={{
                                flex: 1, 
                                flexDirection: 'row', 
                                justifyContent: 'flex-end'
                            }}>
                            <TouchableOpacity 
                                style={{alignSelf: 'center'}} 
                                onPress={() => {setHideConfirmPassword(prev => !prev)}}>
                                <Icon 
                                    type='material-community'
                                    color='gray'
                                    name={hideConfirmPassword ? 'eye' : 'eye-off'}/>
                            </TouchableOpacity>
                        </View>
                    </View>
                </View>
                <View>
                    <TouchableOpacity 
                        style={[styles.button, {backgroundColor: disable ? '#ABCDf3' : '#2D81E0'}]}
                        disabled={disable}
                        onPress={async () => {
                            let name = type == 'client' ? `${firstName}_${lastName}` : title;
                            let userType = type == 'client' ? 1 : 2;

                            if (!address.includes(',') || password != confirmPassword) {
                                return;
                            }

                            let addresses = address.split(',');

                            let result = await authService.register(
                                name, 
                                email, 
                                phone, 
                                addresses[1], 
                                addresses[0],
                                password,
                                userType);
                                
                            result && setModalVisible(true);
                        }}>
                        <Text style={styles.buttonText}>
                            Создать аккаунт        
                        </Text>
                    </TouchableOpacity>
                </View>
                <View style={{paddingBottom: 20}}>
                    <Text
                        style={{
                            fontSize: 16,
                            fontWeight: '400',
                            color: '#9C9C9C',
                            alignSelf: 'center',
                            paddingTop: 20
                        }}>
                        У вас есть аккаунт
                    </Text>
                    <TouchableOpacity
                        style={{
                            backgroundColor: 'transparent',
                            justifyContent: 'center'
                        }}
                        onPress={() => navigation.goBack()}>
                        <Text 
                            style={{
                                color: '#2D81E0',
                                fontSize: 16,
                                fontWeight: '400',
                                alignSelf: 'center'
                            }}>
                            Войти
                        </Text>
                    </TouchableOpacity>
                </View>
            </View>
        </ScrollView>
    )
}

export default RegisterScreen;