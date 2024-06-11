import React from 'react';
import {
    View,
    StyleSheet,
    Text,
    Dimensions,
    TouchableOpacity,
    DeviceEventEmitter,
    ScrollView,
    RefreshControl
} from 'react-native';
import MapView from 'react-native-maps';
import { Icon } from 'react-native-elements';
import styles from '../Styles.jsx';
import CustomMarker from '../Components/CustomMarker.jsx';
import userStore from '../services/userStore.js';
import OrderRequestCard from '../Components/OrderRequestCard.jsx';
import { useIsFocused } from '@react-navigation/native';
import env from '../env.js';
import companyService from '../services/companyService.js';
import MapOrderCard from '../Components/MapOrderCard.jsx';
import { Modalize } from 'react-native-modalize';

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
    const [companies, setCompanies] = React.useState([]);
    const [order, setOrder] = React.useState('');
    const [company, setCompany] = React.useState(company);
    const { width, height } = Dimensions.get('screen');
    const map = React.createRef();
    const [refreshing, setRefreshing] = React.useState(false);

    const isFocused = useIsFocused();

    const setParams = (params) => {
        setCategory(params.selectedCategory);
        setOrderRequest(params.createdOrderRequest);
    }

    const retrieveData = React.useCallback(async () => {
        setRefreshing(true);

        let companies = await companyService.getAll();
        setCompanies(companies);

        let currentUserType = userStore.getUserType();
        await userStore.retrieveData(currentUserType);

        setRefreshing(false);
    }, []);

    const handleMessageReceived = (message) => {
        console.log('in');
        if (message.type == 3 && JSON.parse(message.body).OrderRequestId == orderRequest.id && JSON.parse(message.body).PastEnrollmentTime == null) {
            let id = userStore.get().guid != message.receiverId ? message.receiverId : message.senderId;
            let index = companies.findIndex(c => c.guid == id);
            let coords = companies[index].coords.split(',');

            const region = {
                latitude: Number(coords[0]),
                longitude: Number(coords[1]),
                latitudeDelta: 0.1,
                longitudeDelta: 0.1
            }
            map.current.animateToRegion(region, 500);

            setCompany(companies[index]);
            setOrder(message);
        }
    }

    React.useEffect(() => {
        isFocused && retrieveData();
    }, [isFocused]);

    React.useEffect(() => {
        DeviceEventEmitter.addListener('orderRequestCreated', (params) => setParams(params));
        DeviceEventEmitter.addListener('messageReceived', handleMessageReceived);

        return () => {
            DeviceEventEmitter.removeAllListeners('orerRequestCreated');
            DeviceEventEmitter.removeAllListeners('messageReceived');
        };
    }, [setParams, handleMessageReceived]);

    const goBack = () => {
        navigation.goBack();
    }

    return (
        <View 
            style={{flex: 1, backgroundColor: 'white'}}>
            <MapView 
                camera={{
                    center: {
                        latitude: userStore.get() == '' ? 20 : Number(userStore.get().coords.split(',')[0]),
                        longitude: userStore.get() == '' ? 20 : Number(userStore.get().coords.split(',')[1]),
                    },
                    pitch: 1,
                    heading: 1,
                    zoom: 16
                }}
                ref={map}
                provider='google'
                scrollEnabled
                zoomEnabled
                onPress={(lat) => setOrder()}
                rotateEnabled={false}
                style={mapStyles.map}>
                <CustomMarker imageUri={`${env.api_url}/api/objects/${userStore.get().iconUri}`}
                              coordinate={{
                                latitude: userStore.get() == '' ? 20 : Number(userStore.get().coords.split(',')[0]),
                                longitude: userStore.get() == '' ? 20 : Number(userStore.get().coords.split(',')[1]),
                              }}
                              onPress={(obj) => {}}/>
                {companies.length > 0 ? companies.map((company) => (
                    <CustomMarker
                        key={company.id} 
                        imageUri={`${env.api_url}/api/objects/${company.iconUri}`}
                        coordinate={{
                            latitude: Number(company.coords.split(',')[0]),
                            longitude: Number(company.coords.split(',')[1]),
                        }}
                        onPress={(obj) => {
                            modalRef.current?.open();
                        }}/>
                )) : <></>}
            </MapView>
            <Modalize 
                ref={modalRef}
                adjustToContentHeight={true}
                scrollViewProps={{nestedScrollEnabled: false, scrollEnabled: false}}
                childrenStyle={{height: '90%'}}>
                
            </Modalize>
            <View
                style={{
                    position: 'absolute', 
                    justifyContent: 'center',
                    width,
                    top: 0
                }}>
                <View style={{backgroundColor: 'white', height: height/12, paddingHorizontal: 10}}>
                    <View style={{flex: 1, flexDirection: 'row', justifyContent: 'space-between'}}>
                        <TouchableOpacity 
                            style={{alignSelf: 'center'}}
                            onPress={goBack}>
                            <Icon   
                                name='chevron-left'
                                type='material'
                                color={'#2688EB'}
                                size={40}/>
                        </TouchableOpacity>
                        <Text style={{alignSelf: 'center', color: 'black', fontWeight: '600', fontSize: 21}}>{category.title}</Text>
                        <Text></Text>
                    </View>
                </View>
                {
                    order != '' ?
                    <>
                        <View
                            style={{
                                paddingTop: 10,
                                paddingHorizontal: 15
                            }}>
                            <MapOrderCard
                                message={order}
                                company={company}/>
                        </View>
                    </>
                    :
                    <>
                    </>
                }
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