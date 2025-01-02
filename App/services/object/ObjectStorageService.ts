import AWS from 'aws-sdk';
import RNFS from '@dr.pogodin/react-native-fs';

export class ObjectStorageService {
  private readonly minioClient: AWS.S3;

  constructor(endPoint: string, accessKey: string, secretKey: string) {
    this.minioClient = new AWS.S3({
      credentials: {
        accessKeyId: accessKey,
        secretAccessKey: secretKey
      },
      endpoint: endPoint,
      s3ForcePathStyle: true
    });
  }

  async upload(sourceFile: string) {
    const bucketName = "user-files";

    const objectName = sourceFile.split('\\').pop().split('/').pop();
    const fileContent = await RNFS.readFile(sourceFile);

    console.log(objectName);
    /*if (objectName != undefined) {
      this.minioClient.upload({
        Bucket: bucketName,
        Body: fileContent,
        Key: objectName
      });
    }*/
  }
}
