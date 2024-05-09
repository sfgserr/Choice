import * as KeyChain from 'react-native-keychain';

const createOrder = async (order) => {
    const token = await KeyChain.getGenericPassword();

    return await fetch('http://192.168.0.100/api/Order/Create', {
        method: 'POST',
        body: JSON.stringify(order),
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(response => response.status)
    .catch(err => {
        console.log(err);
    });
}

export default {
    createOrder
}