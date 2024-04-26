import React from 'react';
import {
    View,
    Text,
    Image,
    TouchableOpacity,
    TextInput,
    ScrollView,
    Dimensions
} from 'react-native';
import * as RNFS from 'react-native-fs';
import userStore from '../services/userStore';
import styles from '../Styles';
import blobService from '../services/blobService';
import * as ImagePicker from 'react-native-image-picker';

export default function AccountScreen({ navigation }) {
    const user = userStore.get();

    const { width, height } =  Dimensions.get('screen');

    const [iconUri, setIconUri] = React.useState(`file://${RNFS.DocumentDirectoryPath}/${user.iconUri}.png`);
    const [email, setEmail] = React.useState(user.email);
    const [name, setName] = React.useState(user.name);
    const [surname, setSurname] = React.useState(user.surname);
    const [phone, setPhone] = React.useState(user.phoneNumber);
    const [address, setAddress] = React.useState(`${user.city},${user.street}`);

    const addImage = async () => {
        let response = await ImagePicker.launchImageLibrary();

        if (!response.didCancel) {
            let iconUri = await blobService.uploadImage(response.assets[0].uri);
            setIconUri(response.assets[0].uri);
        }
    }

    return (
        <ScrollView 
            style={{flex: 1, backgroundColor: 'white'}}
            showsVerticalScrollIndicator={false}>
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
                            onChangeText={(text) => setName(text)}/>
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
                            onChangeText={(text) => setSurname(text)}/>
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
                            onChangeText={(text) => setEmail(text)}/>
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
                            onChangeText={(text) => setPhone(text)}/>
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
                            onChangeText={(text) => setAddress(text)}/>
                    </View>
                </View>
                <TouchableOpacity style={[styles.button, { backgroundColor: '#001C3D0D' }]}>
                    <Text
                        style={[styles.buttonText, { color: '#2688EB' }]}>
                        Изменить пароль
                    </Text>
                </TouchableOpacity>
                <View style={{paddingTop: 20}}>
                    <TouchableOpacity style={[styles.button, { backgroundColor: '#001C3D0D' }]}>
                        <Text
                            style={[styles.buttonText, { color: '#EB2626' }]}>
                            Выйти из аккаунта
                        </Text>
                    </TouchableOpacity>
                </View>
                <View style={{paddingTop: 20}}>
                    <TouchableOpacity style={styles.button}>
                        <Text
                            style={styles.buttonText}>
                            Сохранить изменения
                        </Text>
                    </TouchableOpacity>
                </View>
            </View>
        </ScrollView>
    );
}