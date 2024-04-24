import React from 'react';
import {
    View,
    Text,
    Image
} from 'react-native';
import * as RNFS from 'react-native-fs';
import userStore from '../services/userStore';

export default function AccountScreen({ navigation }) {
    const user = userStore.get();

    return (
        <View style={{flex: 1, backgroundColor: 'white'}}>
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
                    source={{uri: `file://${RNFS.DocumentDirectoryPath}/${user.iconUri}.png`}}
                    style={{
                        width: 70,
                        height: 70,
                        borderRadius: 360,
                        alignSelf: 'center',
                    }}/>
            </View>
        </View>
    );
}