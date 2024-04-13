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

export default {
    get
}