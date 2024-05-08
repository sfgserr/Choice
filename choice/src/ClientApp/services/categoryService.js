import * as KeyChain from 'react-native-keychain';

const getCategories = async () => {
    const token = await KeyChain.getGenericPassword();
    
    return await fetch('http://192.168.0.100/api/Category/Get', {
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
            iconUri: json[i].iconUri,
            title: json[i].title,
            id: json[i].id
        }));
    })
    .catch(error => {
        console.log(error);
    });
}

export default {
    getCategories
}