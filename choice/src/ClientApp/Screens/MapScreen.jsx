import React from 'react';
import {
    View,
    StyleSheet,
    Text,
    Dimensions,
    TouchableOpacity
} from 'react-native';
import MapView from 'react-native-maps';
import { Icon } from 'react-native-elements';

export default function MapScreen({ navigation, route }) {
    const { category } = route.params;
    const { width, height } = Dimensions.get('screen');

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
            <View style={{position: 'absolute', justifyContent: 'center', backgroundColor: 'white', width, height: height/12, paddingHorizontal: 10}}>
                <View style={{flex: 1, flexDirection: 'row', justifyContent: 'space-between'}}>
                    <TouchableOpacity style={{alignSelf: 'center'}}>
                        <Icon name='chevron-left'
                              type='material'
                              color={'#2688EB'}
                              size={40}/>
                    </TouchableOpacity>
                    <Text style={{alignSelf: 'center', color: 'black', fontWeight: '600', fontSize: 21}}>{category.title}</Text>
                    <Text></Text>
                </View>
            </View>
        </View>
    );
}

const styles = StyleSheet.create({
    map: {
      ...StyleSheet.absoluteFillObject
    },
});