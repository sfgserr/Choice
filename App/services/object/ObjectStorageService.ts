import AWS from 'aws-sdk';
import {
  readFile
} from '@dr.pogodin/react-native-fs';
import {Buffer} from 'buffer';
import {Alert} from "react-native";

export class ObjectStorageService {
  private readonly minioClient: AWS.S3;

  constructor(endPoint: string, accessKey: string, secretKey: string) {
    AWS.config.update({logger: console});
    this.minioClient = new AWS.S3({
      credentials: {
        accessKeyId: accessKey,
        secretAccessKey: secretKey
      },
      endpoint: endPoint,
      s3ForcePathStyle: true,
      sslEnabled: false
    });
  }

  async upload(sourceFile: string, objectName: string) {
    const bucketName = 'app-files';

    try {
      const fileContent = await readFile(sourceFile, 'base64');
      const buffer = Buffer.from(fileContent, 'base64');

      const fileType = sourceFile.split('.').pop();

      if (fileType === undefined || fileType != 'png' && fileType != 'jpg') {
        Alert.alert('Ошибка', 'поддерживаемые файлы PNG и JPG');
        return;
      }

      if (fileContent.length > 5e6) {
        Alert.alert('Ошибка', 'Макс. размер файла 5 МБ');
        return;
      }

      await this.minioClient.upload({
        Bucket: bucketName,
        Body: buffer,
        Key: objectName,
        ContentType: `image/${fileType}`,
      }).promise();
    }
    catch (error) {
      Alert.alert('Ошибка', 'ошибка загрузки файла', [{text: 'ok'}]);
    }
  }
}
