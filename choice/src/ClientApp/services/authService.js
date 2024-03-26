import * as KeyChain from 'react-native-keychain';

const loginByEmail = async (email, password) => {
    return await fetch(`http://10.0.2.2/api/Auth/Login?email=${email}&password=${password}`, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        }
    })
    .then(async response => {
        if (response.status == 200) {
            const json = await response.json();
            await KeyChain.setGenericPassword('api_key', json);
            return true;
        }
        
        return false;
    })
    .catch(error => {
        console.log(error);
    });
}

const loginByPhone = async (phone) => {
    return await fetch(`http://10.0.2.2/api/Auth/LoginByPhone?phone=${phone}`, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        }
    })
    .then(response => response.status == 200);
}

const verifyCode = async (phone, code) => {
    await fetch(`http://10.0.2.2/api/Auth/Verify?phone=${phone}&code=${code}`, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        }
    })
    .then(async response => {
        if (response.status == 200) {
            const json = await response.json();
            await KeyChain.setGenericPassword('api_key', json);
            return true;
        }
        
        return false;
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