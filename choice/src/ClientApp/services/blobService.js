import RNFS from 'react-native-fs';

const getImage = async (fileName) => {
    const ifExists = RNFS.exists(fileName);

    if (!ifExists) {
        const response = RNFS.downloadFile({
            fromUrl: `http://10.0.2.2/api/objects/${fileName}`,
            toFile: `${RNFS.DocumentDirectoryPath}/auto.png`
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