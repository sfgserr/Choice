import * as KeyChain from 'react-native-keychain';

const get = async () => {
    const token = await KeyChain.getGenericPassword();
    
    return await fetch('http://192.168.0.100/api/Company/Get', {
        method: 'GET',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async response => await response.json());
}

const fillCompanyData = async (data) => {
    const token = await KeyChain.getGenericPassword();
    
    return await fetch('http://192.168.0.100/api/Company/FillCompanyData', {
        method: 'PUT',
        body: JSON.stringify(data),
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(response => response.status);
}

export default {
    get,
    fillCompanyData
}