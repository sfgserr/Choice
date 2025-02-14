import {readFile} from '@dr.pogodin/react-native-fs';
import {Buffer} from 'buffer';
import {Alert} from 'react-native';
import {MinioBlob} from '../../components/ImageBox.tsx';
import {FilePathUtils} from '../../utils/FilePathUtils.ts';

export type ValidationResult = {
  object: MinioBlob | null
}

export class FileValidationService {
  public async getContentAndValidate(sourceFile: string): Promise<ValidationResult> {
    if (sourceFile == '') {
      return {object: null};
    }

    const contentType = sourceFile.split('.').pop();

    if (contentType === undefined || contentType != 'png' && contentType != 'jpg') {
      Alert.alert('Ошибка', 'поддерживаемые файлы PNG и JPG');
      return {object: null};
    }

    const fileContent = await readFile(sourceFile, 'base64');

    if (fileContent.length > 5e6) {
      Alert.alert('Ошибка', 'Макс. размер файла 5 МБ');
      return {object: null};
    }

    const objectName = FilePathUtils.getFileName(sourceFile);

    if (objectName == undefined) {
      Alert.alert('Ошибка', 'Неверный URI');
      return {object: null};
    }

    return {
      object: new MinioBlob(Buffer.from(fileContent, 'base64'), objectName, sourceFile, contentType),
    };
  }
}
