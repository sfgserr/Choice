import * as KeyChain from 'react-native-keychain';
import env from '../env';

const getMessages = async (receiverId) => {
    const token = await KeyChain.getGenericPassword();

    return await fetch(`${env.api_url}/api/Message/GetMessages?receiverId=${receiverId}`, {
        method: 'GET',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async res => {
        let json = await res.json();

        return json;
    });
}

const getChat = async (userId) => {
    const token = await KeyChain.getGenericPassword();

    return await fetch(`${env.api_url}/api/Message/GetChat?userId=${userId}`, {
        method: 'GET',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async res => {
        let json = await res.json();

        return json;
    });
}

const getChats = async () => {
    const token = await KeyChain.getGenericPassword();
    
    return await fetch(`${env.api_url}/api/Message/GetChats`, {
        method: 'GET',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async res => {
        let json = await res.json();
        return json;
    });
}

const sendMessage = async (text, receiverId) => {
    const token = await KeyChain.getGenericPassword();
    
    return await fetch(`${env.api_url}/api/Message/Send?receiverId=${receiverId}&text=${text}`, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async res => {
        let json = await res.json();

        return json;
    });
}

const read = async (id) => {
    const token = await KeyChain.getGenericPassword();
    
    return await fetch(`${env.api_url}/api/Message/Read?id=${id}`, {
        method: 'PUT',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    });
}

export default {
    getMessages,
    getChats,
    getChat,
    read,
    sendMessage
}