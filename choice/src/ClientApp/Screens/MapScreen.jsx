import React from 'react';
import {
    View,
    StyleSheet,
    Text,
    Dimensions,
    TouchableOpacity,
    DeviceEventEmitter
} from 'react-native';
import MapView from 'react-native-maps';
import { Icon } from 'react-native-elements';
import styles from '../Styles.jsx';
import geoService from '../services/geoService.js';
import CustomMarker from '../Components/CustomMarker.jsx';
import * as RNFS from 'react-native-fs';
import userStore from '../services/userStore.js';
import dateHelper from '../helpers/dateHelper.js';
import OrderRequestCard from '../Components/OrderRequestCard.jsx';

export default function MapScreen({ navigation, route }) {
    const modalRef = React.useRef(null);

    const [category, setCategory] = React.useState({
        id: route.params.category.id,
        title: route.params.category.title
    });

    const [orderRequest, setOrderRequest] = React.useState({
        id: 0,
        status: 0,
        description: '',
        categoryId: 0,
        searchRadius: 0,
        toKnowPrice: false,
        toKnowDeadline: false,
        toKnowEnrollmentDate: false,
        creationDate: '',
        photoUris: []
    });

    const { width, height } = Dimensions.get('screen');

    const [coords, setCoords] = React.useState([]);
    const user = userStore.get();

    const setParams = (params) => {
        setCategory(params.selectedCategory);
        setOrderRequest(params.createdOrderRequest);
    }

    DeviceEventEmitter.addListener('orderRequestCreated', (params) => setParams(params));

    React.useEffect(() => {
        async function getCoords() {
            let coords = await geoService.getCoords();

            setCoords(coords);
        }
        getCoords();
    });

    const goBack = () => {
        navigation.goBack();
    }

    return (
        <View style={{flex: 1, backgroundColor: 'white'}}>
            <MapView 
                camera={{
                    center: {
                        latitude: coords[0] == null ? 20 : coords[0],
                        longitude: coords[1] == null ? 20 : coords[1],
                    },
                    pitch: 1,
                    heading: 1,
                    zoom: 16
                }}
                provider='google'
                scrollEnabled={false}
                zoomEnabled={false}
                rotateEnabled={false}
                style={mapStyles.map}>
                <CustomMarker imageUri={`file://${RNFS.DocumentDirectoryPath}/${user.iconUri}.png`}
                              coordinate={{
                                latitude: coords[0] == null ? 20 : coords[0],
                                longitude: coords[1] == null ? 20 : coords[1]
                              }}/>
            </MapView>
            <View style={{position: 'absolute', justifyContent: 'center', backgroundColor: 'white', width, height: height/12, paddingHorizontal: 10}}>
                <View style={{flex: 1, flexDirection: 'row', justifyContent: 'space-between'}}>
                    <TouchableOpacity 
                        style={{alignSelf: 'center'}}
                        onPress={goBack}>
                        <Icon name='chevron-left'
                              type='material'
                              color={'#2688EB'}
                              size={40}/>
                    </TouchableOpacity>
                    <Text style={{alignSelf: 'center', color: 'black', fontWeight: '600', fontSize: 21}}>{category.title}</Text>
                    <Text></Text>
                </View>
            </View>
            {
                orderRequest.id == 0 ?
                <>
                    <View style={{position: 'absolute', justifyContent: 'center', backgroundColor: 'white', width, height: height/10, bottom: 0, paddingHorizontal: 20}}>
                        <TouchableOpacity style={[styles.button, {bottom: 10}]}
                                          onPress={() => navigation.navigate("OrderRequestCreation", { category })}>
                            <Text style={styles.buttonText}>Создать заказ</Text>
                        </TouchableOpacity>
                    </View>
                </>
                :
                <>
                    <View
                        style={{
                            position: 'absolute',
                            bottom: 10,
                            width: '100%'
                        }}>
                        
                        <OrderRequestCard 
                            request={orderRequest} 
                            requestCategory={category} 
                            navigation={navigation}/>
                    </View>
                </>
            }
        </View>
    );
}

const mapStyles = StyleSheet.create({
    map: {
      ...StyleSheet.absoluteFillObject
    },
});