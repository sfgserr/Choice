import RNFS from 'react-native-fs';
import {toByteArray } from 'react-native-quick-base64';
import env from '../env';

const uploadImage = async (filePath) => {
    if (filePath == '') {
        return '';
    }

    const directories = filePath.split('/')

    const data = await RNFS.readFile(filePath, 'base64');
    const fileNameAndExtension = directories[directories.length-1].split('.');
    const buffer = toByteArray(data);

    if (buffer.length > 150000 || fileNameAndExtension[1] != 'png') {
        return 0;
    }

    return await fetch(`${env.api_url}/api/objects/${fileNameAndExtension[0]}`, {
        method: 'POST',
        body: buffer,
        headers: {
            'Content-Type':'application/octet-stream'
        }
    })
    .then(async response => { 
        console.log(response.status);
        if (response.status == 200) {
            return fileNameAndExtension[0]; 
        }
        else {
            return '';
        }
    });
}

export default {
    uploadImage
}