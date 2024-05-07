import React from "react";
import {
    View,
    Text,
    FlatList
} from 'react-native';
import clientService from "../services/clientService";
import userStore from "../services/userStore";
import CompanyRequestCard from "../Components/CompanyRequestCard";
import categoryStore from "../services/categoryStore";

const CompanyRequestsScreen = ({navigation}) => {
    const [requests, setRequests] = React.useState([]);

    React.useEffect(() => {
        async function getRequestsAndCategories() {
            let user = userStore.get();

            let fetchedRequests = await clientService.getOrderRequest(user.categoriesId);
            await categoryStore.retrieveData();

            setRequests(fetchedRequests);
        }
        
        getRequestsAndCategories();
    }, []);

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
                renderItem={({item}) => {
                    return (
                        <View
                            style={{paddingHorizontal: 10}}>
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