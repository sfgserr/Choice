import React from 'react';
import {
    View,
    Text,
    FlatList
} from 'react-native';
import RNFS from 'react-native-fs';
import categoryService from '../services/categoryService.js';
import blobService from '../services/blobService.js';

export default function CategoryScreen({ navigation, route }) {
    const { categories } = route.params;

    return (
        <View>
            <FlatList data={categories}/>
        </View>
    );
}