import * as KeyChain from 'react-native-keychain';

const get = async () => {
    const token = await KeyChain.getGenericPassword();
    
    return await fetch('http://192.168.0.106/api/Client/GetClient', {
        method: 'GET',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async response => await response.json());
}

const sendOrderRequest = async (orderRequest) => {
    const token = await KeyChain.getGenericPassword();

    return await fetch('http://192.168.0.106/api/Client/SendOrderRequest', {
        method: 'POST',
        body: JSON.stringify(orderRequest),
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async response => {
        const json = await response.json();



        return {
            categoryId: json.categoryId,
            creationDate: json.creationDate.toString(),
            description: json.description,
            id: json.id,
            searchRadius: json.searchRadius,
            status: json.status,
            toKnowDeadLine: json.toKnowDeadLine,
            toKnowEnrollmentDate: json.toKnowEnrollmentDate,
            toKnowPrice: json.toKnowPrice 
        };
    });
}

export default {
    get,
    sendOrderRequest
}