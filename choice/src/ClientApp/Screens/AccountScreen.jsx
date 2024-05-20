import React from 'react';
import {
    View,
    Text,
    Image,
    TouchableOpacity,
    TextInput,
    ScrollView,
    Dimensions,
    Modal
} from 'react-native';
import * as RNFS from 'react-native-fs';
import userStore from '../services/userStore';
import clientService from '../services/clientService';
import styles from '../Styles';
import blobService from '../services/blobService';
import * as ImagePicker from 'react-native-image-picker';
import { AuthContext } from '../App';
import { Icon } from 'react-native-elements';
import env from '../env';

export default function AccountScreen({ navigation }) {
    const user = userStore.get();

    const { signOut } = React.useContext(AuthContext);

    const { width, height } =  Dimensions.get('screen');

    const [iconUri, setIconUri] = React.useState(`${env.api_url}/api/objects/${user.iconUri}`);
    const [disable, setDisable] = React.useState(true);
    const [email, setEmail] = React.useState(user.email);
    const [name, setName] = React.useState(user.name);
    const [surname, setSurname] = React.useState(user.surname);
    const [phone, setPhone] = React.useState(user.phoneNumber);
    const [address, setAddress] = React.useState(`${user.city},${user.street}`);
    const [modalVisible, setModalVisible] = React.useState(false);
    
    const addImage = async () => {
        let response = await ImagePicker.launchImageLibrary();
        
        if (!response.didCancel) {
            let iconUri = await blobService.uploadImage(response.assets[0].uri);
            await clientService.changeIconUri(iconUri);
            setIconUri(response.assets[0].uri);
        }
    }

    const saveChanges = async () => {
        let addresses = address.split(',');

        let state = {
            name,
            surname,
            email,
            phoneNumber: phone,
            street: addresses[1],
            city: addresses[0]
        }

        await clientService.changeUserData(state);
        setDisable(true);
        setModalVisible(true);
    }

    const logout = async () => {
        await signOut();
    }

    return (
        <ScrollView 
            style={{flex: 1, backgroundColor: 'white'}}
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
                                        setModalVisible(false);
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
                                    Измение сохранены
                                </Text>
                                <View
                                    style={{
                                        paddingTop: 40,
                                        paddingBottom: 10,
                                        paddingHorizontal: 10
                                    }}>
                                    <TouchableOpacity 
                                        style={[styles.button, {borderRadius: 10}]}
                                        onPress={() => {
                                            setModalVisible(false);
                                        }}>
                                        <Text style={styles.buttonText}>Ок</Text>
                                    </TouchableOpacity>
                                </View>
                            </View>
                        </View>
                    </View>
                </View>
            </Modal>
            <View style={{justifyContent: 'center', paddingTop: 20}}>
                <Text 
                    style={{
                        fontSize: 21, 
                        fontWeight: '600', 
                        color: 'black', 
                        alignSelf: 'center'
                    }}>
                    Аккаунт
                </Text>
            </View>
            <View style={{paddingTop: 40}}>
                <Image 
                    source={{uri: iconUri}}
                    style={{
                        width: 70,
                        height: 70,
                        borderRadius: 360,
                        alignSelf: 'center',
                    }}/>
            </View>
            <View 
                style={{
                    paddingTop: 20,
                    alignSelf: 'center'
                }}>
                <TouchableOpacity
                    style={{
                        backgroundColor: 'transparent',
                    }}
                    onPress={addImage}>
                    <Text 
                        style={{
                            color: '#2D81E0',
                            fontSize: 15,
                            fontWeight: '500',
                        }}>
                        Изменить фото
                    </Text>
                </TouchableOpacity>
            </View>
            <View
                style={{
                    paddingTop: 30,
                    paddingHorizontal: 20
                }}>
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
                            value={name} 
                            onChangeText={(text) => {
                                setName(text);
                                setDisable(false);
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
                            value={surname} 
                            onChangeText={(text) => {
                                setSurname(text);
                                setDisable(false);
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
                    E-mail        
                </Text>
                <View style={{paddingBottom: 20}}>
                    <View style={[styles.textInput]}>
                        <TextInput 
                            style={[styles.textInputFont]} 
                            value={email} 
                            onChangeText={(text) => {
                                setEmail(text);
                                setDisable(false);
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
                            value={phone} 
                            onChangeText={(text) => {
                                setPhone(text);
                                setDisable(false);
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
                            value={address} 
                            onChangeText={(text) => {
                                setAddress(text);
                                setDisable(false);
                            }}/>
                    </View>
                </View>
                <TouchableOpacity 
                    style={[styles.button, { backgroundColor: '#001C3D0D' }]}
                    onPress={() => navigation.navigate('ChangePassword')}>
                    <Text
                        style={[styles.buttonText, { color: '#2688EB' }]}>
                        Изменить пароль
                    </Text>
                </TouchableOpacity>
                <View style={{paddingTop: 20}}>
                    <TouchableOpacity 
                        style={[styles.button, { backgroundColor: '#001C3D0D' }]}
                        onPress={logout}>
                        <Text
                            style={[styles.buttonText, { color: '#EB2626' }]}>
                            Выйти из аккаунта
                        </Text>
                    </TouchableOpacity>
                </View>
                {
                    !disable ?
                    <>
                        <View style={{paddingTop: 20}}>
                            <TouchableOpacity 
                                style={styles.button}
                                onPress={saveChanges}>
                                <Text
                                    style={styles.buttonText}>
                                    Сохранить изменения
                                </Text>
                            </TouchableOpacity>
                        </View>
                    </>
                    :
                    <>
                    </>
                }
            </View>
        </ScrollView>
    );
}