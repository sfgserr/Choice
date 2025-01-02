import * as Minio from 'minio';

export class ObjectStorageService {
  private readonly minioClient: Minio.Client;

  constructor(endPoint: string, accessKey: string, secretKey: string) {
    this.minioClient = new Minio.Client({
      endPoint,
      accessKey,
      secretKey,
      useSSL: false,
      port:8080
    });
  }

  async upload(sourceFile: string) {
    const bucketName = "user-files";

    const objectName = sourceFile.split('\\').pop().split('/').pop();

    if (objectName != undefined)
      await this.minioClient.fPutObject(bucketName, objectName, sourceFile);
  }
}
