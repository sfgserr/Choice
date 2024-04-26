import RNFS from 'react-native-fs';
import {toByteArray } from 'react-native-quick-base64';

const getImage = async (fileName) => {
    const filePath = `${RNFS.DocumentDirectoryPath}/${fileName}.png`
    const ifExists = await RNFS.exists(filePath);

    if (!ifExists) {
        const response = RNFS.downloadFile({
            fromUrl: `http://192.168.0.106/api/objects/${fileName}`,
            toFile: filePath
        });
        response.promise.then(res => {
            console.log(res.bytesWritten);
            console.log(res.statusCode);
        })
    }
}

const uploadImage = async (filePath) => {
    if (filePath == '') {
        return;
    }

    const data = await RNFS.readFile(filePath, 'base64');
    const buffer = toByteArray(data);

    return await fetch(`http://192.168.0.106/api/objects/damn`, {
        method: 'POST',
        body: buffer,
        headers: {
            'Content-Type':'octet-stream'
        }
    })
    .then(async response => console.log(response.status));
}

export default {
    getImage,
    uploadImage
}