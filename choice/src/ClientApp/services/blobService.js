import RNFS from 'react-native-fs';
import {toByteArray } from 'react-native-quick-base64';
import env from '../env';

const uploadImage = async (filePath) => {
    if (filePath == '') {
        return '';
    }

    const directories = filePath.split('/')

    const data = await RNFS.readFile(filePath, 'base64');
    const fileName = directories[directories.length-1].split('.')[0];
    const buffer = toByteArray(data);

    return await fetch(`${env.api_url}/api/objects/${fileName}`, {
        method: 'POST',
        body: buffer,
        headers: {
            'Content-Type':'application/octet-stream'
        }
    })
    .then(async response => { 
        console.log(response.status);
        if (response.status == 200) {
            return fileName; 
        }
        else {
            return '';
        }
    });
}

export default {
    uploadImage
}