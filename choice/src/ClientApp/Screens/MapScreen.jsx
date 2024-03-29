import React from 'react';
import {
    View
} from 'react-native';
import MapView from 'react-native-maps';

export default function MapScreen({ navigation, route }) {
    const { category } = route.params;

    return (
        <View style={{flex: 1, backgroundColor: 'white'}}>
            <MapView>

            </MapView>
        </View>
    );
}