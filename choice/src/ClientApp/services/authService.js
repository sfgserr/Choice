import * as KeyChain from 'react-native-keychain';

const loginByEmail = async (email, password) => {
    await fetch(`http://172.21.112.1/api/Auth/Login?email=${email}&password=${password}`, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        }
    })
    .then(response => response.json())
    .then(async json => {
        await KeyChain.setGenericPassword('api_key', json);
    })
    .catch(error => {
        console.log(error);
    });
}

const loginByPhone = async (phone) => {
    return await fetch(`http://172.21.112.1/api/Auth/LoginByPhone?phone=${phone}`, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        }
    })
    .then(response => response.status == 200);
}

const verifyCode = async (phone, code) => {
    await fetch(`http://172.21.112.1/api/Auth/Verify?phone=${phone}&code=${code}`, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        }
    })
    .then(response => response.json())
    .then(async json => {
        await KeyChain.setGenericPassword('api_key', json);
    })
    .catch(error => {
        console.log(error);
    });
}

export default {
    loginByEmail,
    loginByPhone,
    verifyCode
}