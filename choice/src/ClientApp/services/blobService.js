import RNFS from 'react-native-fs';

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

export default {
    getImage
}