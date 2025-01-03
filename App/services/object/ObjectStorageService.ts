import AWS from 'aws-sdk';
import {
  readFile
} from '@dr.pogodin/react-native-fs';
import {Buffer} from 'buffer';

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
    const bucketName = "app-files";

    try {
      const fileContent = await readFile(sourceFile, 'base64');
      const buffer = Buffer.from(fileContent, 'base64');

      if (fileContent.length > 5e6)
        return;

      await this.minioClient.upload({
        Bucket: bucketName,
        Body: buffer,
        Key: objectName,
        ContentType: 'image/png'
      }).promise();
    }
    catch (error) {
      console.log(error);
    }
  }
}
