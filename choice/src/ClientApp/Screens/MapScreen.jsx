import React from 'react';
import {
    View,
    StyleSheet
} from 'react-native';
import MapView from 'react-native-maps';

export default function MapScreen({ navigation, route }) {
    const { category } = route.params;

    return (
        <View style={{flex: 1, backgroundColor: 'white'}}>
            <MapView 
                region={{
                    latitude: 37.78825,
                    longitude: -122.4324,
                    latitudeDelta: 0.015,
                    longitudeDelta: 0.0121,
                }}
                provider='google'
                style={styles.map}>

            </MapView>
        </View>
    );
}

const styles = StyleSheet.create({
    map: {
      ...StyleSheet.absoluteFillObject,
    },
});