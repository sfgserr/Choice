import * as KeyChain from 'react-native-keychain';
import env from '../env';

const send = async (review) => {
    const token = await KeyChain.getGenericPassword();
    
    return await fetch(`${env.api_url}/api/Review/Send`, {
        method: 'POST',
        body: JSON.stringify(review),
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token.password}`
        }
    })
    .then(async response => console.log(response.status))
    .catch(err => {
        console.log(err);
    });
}

export default {
    send
}