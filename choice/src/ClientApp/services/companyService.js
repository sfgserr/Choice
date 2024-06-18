import * as KeyChain from 'react-native-keychain';
import env from '../env';

const get = async () => {
    const token = await KeyChain.getGenericPassword();
    
    return await fetch(`${env.api_url}/api/Company/Get`, {
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
    
    return await fetch(`${env.api_url}/api/Company/FillCompanyData`, {
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

const getCompany = async (guid) => {
    const token = await KeyChain.getGenericPassword();
    
    return await fetch(`${env.api_url}/api/Company/GetCompany?guid=${guid}`, {
        method: 'GET',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async response => await response.json());
}

const getAll = async () => {
    const token = await KeyChain.getGenericPassword();
    
    return await fetch(`${env.api_url}/api/Company/GetAll`, {
        method: 'GET',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async response => await response.json());
}

const changeData = async (data) => {
    const token = await KeyChain.getGenericPassword();
    
    return await fetch(`${env.api_url}/api/Company/ChangeData`, {
        method: 'PUT',
        body: JSON.stringify(data),
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async response => await response.json());
}

const changeIconUri = async (iconUri) => {
    const token = await KeyChain.getGenericPassword();
    
    return await fetch(`${env.api_url}/api/Company/ChangeIconUri?uri=${iconUri}`, {
        method: 'PUT',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async response => await response.json());
}

export default {
    changeIconUri,
    changeData,
    get,
    fillCompanyData,
    getCompany,
    getAll
}