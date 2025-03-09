import {Image, TouchableOpacity} from 'react-native';
import {
  View,
  Dimensions,
} from 'react-native';
import {ImageBoxProps} from '../types/ComponentTypes.ts';
import {useDependency} from "../services/Hooks.ts";
import {FileValidationService} from "../services/object/FileValidationService.ts";
import {launchImageLibrary} from "react-native-image-picker";
import React from "react";

export interface ImageBoxObject {
  getUri(): string;
  getObjectName(): string;

  isUpload: boolean;
}

export class MinioBlob implements ImageBoxObject {
  readonly buffer: Buffer | null = null;
  readonly objectName: string = '';
  readonly path: string = '';
  readonly contentType: string = '';
  readonly isUpload: boolean = false;

  constructor(buffer: Buffer | null, objectName: string, path: string, contentType: string) {
    this.buffer = buffer;
    this.objectName = objectName;
    this.path = path;
    this.contentType = contentType;
  }

  getUri(): string {
    return this.path;
  }

  getObjectName(): string {
    return this.objectName;
  }

  static createDefault() {
    return new MinioBlob(null, '', '', '');
  }
}

export class UploadedBlob implements ImageBoxObject {
  readonly objectName: string = '';
  readonly isUpload: boolean = true;

  constructor(objectName: string) {
    this.objectName = objectName;
  }

  getUri(): string {
    return `${process.env.MINIO_URL}/app-files/${this.objectName}`;
  }

  getObjectName(): string {
    return this.objectName;
  }
}

const d = Dimensions.get('screen');

export default function ImageBox({object, setPhoto, index, readonly = false}: ImageBoxProps) {
  const fileValidationService = useDependency<FileValidationService>('FileValidationService');

  const set = React.useCallback(async ()=> {
    let response = await launchImageLibrary({mediaType: 'photo'});

    if (response.assets != undefined) {
      const result = await fileValidationService.getContentAndValidate(response.assets[0].uri!);

      setPhoto(prev => {
        if (result.object != null) {
          prev[index] = result.object;
        }
        return [...prev];
      });
    }
  }, []);

  const remove = React.useCallback(() => {
    setPhoto(prev => {
      prev[index] = MinioBlob.createDefault();

      return [...prev];
    });
  }, []);

  return (
    <View
      style={{
        justifyContent: 'center',
        alignItems: 'center',
      }}>
      <View
        style={{
          width: d.width * 0.274,
          height: d.width * 0.274,
          borderRadius: 12,
          borderWidth: readonly ? 0 : 1,
          borderStyle: 'dashed',
          position: 'relative',
          backgroundColor: '#F9F9F9',
          borderColor: object.getObjectName() != '' ? '#4D4D4D' : '#C8C8C8',
          justifyContent: 'center',
        }}>
        {object.getObjectName() == '' ? (
          <>
            <TouchableOpacity
              onPress={set}>
              <Image
                source={require('../assets/images/imagebox.png')}
                style={{
                  alignSelf: 'center',
                  resizeMode: 'contain',
                  width: d.width * 0.091,
                  height: d.width * 0.091,
                }}/>
            </TouchableOpacity>
          </>
        ) : (
          <>
            <Image
              source={{uri: object.getUri()}}
              style={{
                width: '100%',
                height: '100%',
                borderRadius: 12,
              }}/>

            {!readonly && (
              <TouchableOpacity
                style={{
                  width: d.width * 0.06,
                  height: d.width * 0.06,
                  borderWidth: 1,
                  borderColor: '#E7E7E7',
                  backgroundColor: 'white',
                  position: 'absolute',
                  top: -6,
                  right: -6,
                  borderRadius: d.width * 0.03,
                  justifyContent: 'center',
                }}
                onPress={remove}>
                <Image
                  style={{
                    width: d.width * 0.03,
                    height: d.width * 0.03,
                    resizeMode: 'contain',
                    alignSelf: 'center',
                  }}
                  source={require('../assets/images/cross.png')}/>
              </TouchableOpacity>
            )}
          </>
        )}
      </View>
    </View>
  )
}
