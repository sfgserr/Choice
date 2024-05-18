import * as KeyChain from 'react-native-keychain';
import env from '../env';

const createOrder = async (order) => {
    const token = await KeyChain.getGenericPassword();

    return await fetch(`${env.api_url}/api/Order/Create`, {
        method: 'POST',
        body: JSON.stringify(order),
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async response => await response.json())
    .catch(err => {
        console.log(err);
    });
}

export default {
    createOrder
}