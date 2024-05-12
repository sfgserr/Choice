import * as KeyChain from 'react-native-keychain';

const getMessages = async (receiverId) => {
    const token = await KeyChain.getGenericPassword();

    return await fetch(`http://192.168.0.100/api/Message/GetMessages?receiverId=${receiverId}`, {
        method: 'GET',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async res => {
        let json = await res.json();

        return Object.keys(json).map((i) => ({
            id: json[i].id,
            body: json[i].body,
            senderId: json[i].senderId,
            receiverId: json[i].receiverId,
            iconUri: json[i].iconUri,
            type: json[i].type,
            creationTime: json[i].creationTime            
        }));
    });
}

const getChats = async () => {
    const token = await KeyChain.getGenericPassword();

    return await fetch(`http://192.168.0.100/api/Message/GetChats`, {
        method: 'GET',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async res => await res.json());
}

export default {
    getMessages,
    getChats
}