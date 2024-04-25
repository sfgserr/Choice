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
    if (filePath == '')
        return;

    const directories = filePath.split('/');
    const fileName = directories[directories.length-1].split('.')[0];

    const bs64 = await RNFS.readFile(filePath, 'base64');
    const byteArray = toByteArray(bs64);

    return await fetch(`http://192.168.0.106/api/objects/${fileName}`, {
        method: 'POST',
        body: byteArray,
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/octet-stream',
        }
    })
    .then(async response => console.log(response.status));
}

export default {
    getImage,
    uploadImage
}