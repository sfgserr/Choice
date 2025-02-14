import AWS from 'aws-sdk';
import {Alert} from 'react-native';
import {MinioBlob} from '../../components/ImageBox.tsx';

export class ObjectStorageService {
  private readonly minioClient: AWS.S3;

  constructor(endPoint: string, accessKey: string, secretKey: string) {
    AWS.config.update({logger: console});
    this.minioClient = new AWS.S3({
      credentials: {
        accessKeyId: accessKey,
        secretAccessKey: secretKey,
      },
      endpoint: endPoint,
      s3ForcePathStyle: true,
      sslEnabled: false,
    });
  }

  public async upload(minioObject: MinioBlob) {
    if (minioObject.buffer == null) {
      return;
    }

    const bucketName = 'app-files';

    try {
      await this.minioClient.upload({
        Bucket: bucketName,
        Body: minioObject.buffer,
        Key: minioObject.objectName,
        ContentType: `image/${minioObject.contentType}`,
      }).promise();
    }
    catch (error) {
      Alert.alert('Ошибка', 'ошибка загрузки файла', [{text: 'ok'}]);
    }
  }
}
