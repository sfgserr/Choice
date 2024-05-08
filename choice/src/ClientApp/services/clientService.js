import * as KeyChain from 'react-native-keychain';
import { advanceAnimationByFrame } from 'react-native-reanimated';
import arrayHelper from '../helpers/arrayHelper';

const get = async () => {
    const token = await KeyChain.getGenericPassword();

    return await fetch('http://192.168.0.100/api/Client/GetClient', {
        method: 'GET',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async response => await response.json())
    .catch(err => console.log(err));
}

const getOrderRequest = async (categoriesId) => {
    const token = await KeyChain.getGenericPassword();

    let index = 0;

    let queryArray = arrayHelper.project(categoriesId, (id) => {
        let string = `categoriesId[${index}]=${id}`
        index = index + 1;

        return string;
    });

    index = 0;

    return await fetch(`http://192.168.0.100/api/Client/GetOrderRequests?${queryArray.join('&')}`, {
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

    return await fetch('http://192.168.0.100/api/Client/SendOrderRequest', {
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
            toKnowDeadline: json.toKnowDeadline,
            toKnowEnrollmentDate: json.toKnowEnrollmentDate,
            toKnowPrice: json.toKnowPrice,
            photoUris: [json.photoUris[0], json.photoUris[1], json.photoUris[2]]
        };
    });
}

const changeOrderRequest = async (orderRequest) => {
    const token = await KeyChain.getGenericPassword();
    
    return await fetch('http://192.168.0.100/api/Client/ChangeOrderRequest', {
        method: 'PUT',
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
            toKnowDeadline: json.toKnowDeadline,
            toKnowEnrollmentDate: json.toKnowEnrollmentDate,
            toKnowPrice: json.toKnowPrice,
            photoUris: [json.photoUris[0], json.photoUris[1], json.photoUris[2]]
        };
    });
}

const getClientRequests = async () => {
    const token = await KeyChain.getGenericPassword();

    return await fetch('http://192.168.0.100/api/Client/GetClientRequests', {
        method: 'GET',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async response => {
        const json = await response.json();

        return Object.keys(json).map((i) => ({
            id: json[i].id,
            status: json[i].status,
            description: json[i].description,
            categoryId: json[i].categoryId,
            searchRadius: json[i].searchRadius,
            toKnowPrice: json[i].toKnowPrice,
            toKnowDeadline: json[i].toKnowDeadline,
            toKnowEnrollmentDate: json[i].toKnowEnrollmentDate,
            creationDate: json[i].creationDate,
            photoUris: [json[i].photoUris[0], json[i].photoUris[1], json[i].photoUris[2]]
        }));
    });
}

const changeIconUri = async (iconUri) => {
    const token = await KeyChain.getGenericPassword();

    return await fetch(`http://192.168.0.100/api/Client/ChangeIconUri?iconUri=${iconUri}`, {
        method: 'PUT',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async res => await res.json());
}

const changeUserData = async (state) => {
    const token = await KeyChain.getGenericPassword();

    return await fetch(`http://192.168.0.100/api/Client/ChangeUserData`, {
        method: 'PUT',
        body: JSON.stringify(state),
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async res => await res.json());
}

export default {
    get,
    sendOrderRequest,
    getClientRequests,
    changeOrderRequest,
    changeIconUri,
    changeUserData,
    getOrderRequest
}