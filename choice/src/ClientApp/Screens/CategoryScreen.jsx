import React from 'react';
import {
    View,
    Text,
    FlatList,
    Image,
    TouchableOpacity
} from 'react-native';
import { Icon } from 'react-native-elements';
import RNFS from 'react-native-fs';
import store from '../services/store.js';

export default function CategoryScreen({ navigation, route }) {
    const categories = store.getCategories();

    return (
        <View>
            <Text style={{color: 'black', alignSelf: 'center', fontSize: 21, fontWeight: '600', paddingTop: 30}}>Услуги</Text>

            <FlatList data={categories}
                      style={{paddingTop: 10}}
                      renderItem={({item}) => {
                        return (
                            <TouchableOpacity style={{flex:1, flexDirection: 'row', justifyContent: 'flex-start', paddingHorizontal: 20, paddingVertical: 15, borderColor: '#e9e9e9', borderWidth: 1}}>
                                <View style={{backgroundColor: '#47A4F9', borderRadius: 10, justifyContent: 'center', padding: 10}}>
                                    <Image style={{height:20, width:20}}
                                           source={{uri: `file://${RNFS.DocumentDirectoryPath}/${item.iconUri}.png`}}/>
                                </View>
                                <Text style={{alignSelf: 'center', paddingLeft: 10, color: '#181818', fontWeight: '400', fontSize: 18}}>{item.title}</Text>
                                <View style={{flex: 1, flexDirection: 'row', alignSelf: 'center', justifyContent: 'flex-end'}}>
                                    <Icon type='material'
                                          name='chevron-right'
                                          color={'#CDCECF'}/>
                                </View>
                            </TouchableOpacity>
                        );
                      }}/>
        </View>
    );
}