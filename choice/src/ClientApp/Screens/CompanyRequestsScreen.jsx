import React from "react";
import {
    View,
    Text,
    FlatList,
    RefreshControl
} from 'react-native';
import clientService from "../services/clientService";
import userStore from "../services/userStore";
import CompanyRequestCard from "../Components/CompanyRequestCard";
import categoryStore from "../services/categoryStore";
import { useIsFocused } from '@react-navigation/native';

const CompanyRequestsScreen = ({navigation}) => {
    const [requests, setRequests] = React.useState([]);
    const [refreshing, setRefreshing] = React.useState(false);

    let user = userStore.get();

    const isFocused = useIsFocused();

    const onRefresh = React.useCallback(async () => {
        setRefreshing(true);

        let fetchedRequests = await clientService.getOrderRequest(user.categoriesId);
        await categoryStore.retrieveData();

        setRequests(fetchedRequests);

        setRefreshing(false);
    }, []);

    React.useEffect(() => {
        isFocused && onRefresh();
    }, [isFocused]);

    return (
        <View
            style={{
                flex: 1,
                backgroundColor: 'white'
            }}>
            <Text
                style={{
                    color: 'black',
                    fontSize: 21,
                    fontWeight: '600',
                    paddingTop: 20,
                    alignSelf: 'center'
                }}>
                Заказы    
            </Text>
            <FlatList
                data={requests}
                style={{
                    paddingTop: 20
                }}
                refreshControl={
                    <RefreshControl refreshing={refreshing} onRefresh={onRefresh}/>
                }
                renderItem={({item}) => {
                    return (
                        <View
                            style={{paddingHorizontal: 10, paddingBottom: 10}}>
                            <CompanyRequestCard 
                                orderRequest={item}
                                navigation={navigation}
                                button={true}/>
                        </View>
                    )
                }}/>    
        </View>
    );
}

export default CompanyRequestsScreen;