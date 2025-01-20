import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {CompanyMapMarker} from '../../types/DomainTypes.ts';
import {HttpResponseWithContent} from '../../types/ServiceTypes.ts';
import {FilePathUtils} from '../../utils/FilePathUtils.ts';
import {ObjectStorageService} from '../object/ObjectStorageService.ts';

export class CompanyService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;
  private readonly objectStorageService: ObjectStorageService;

  constructor(httpService: RefreshTokenHttpServiceDecorator, objectStorageService: ObjectStorageService) {
    this.httpService = httpService;
    this.objectStorageService = objectStorageService;
  }

  async createCompany(
    name: string,
    password: string,
    email: string,
    phoneNumber: string,
    city: string,
    street: string) {
    return await this.httpService.request(
      'companies',
      'POST',
      JSON.stringify({
        name,
        password,
        email,
        phoneNumber,
        city,
        street
      }));
  }

  async fillData(
    description: string,
    categoryIds: number[],
    photoUris: string[],
    socialMediaUris: string[],
    isPrepaymentAvailable: boolean) {
    const sources = ['', '', '', '', '', ''];
    for (let i = 0; i < 6; i++) {
      sources[i] = photoUris[i];
      let path = FilePathUtils.getFileName(photoUris[i]);

      photoUris[i] = path == undefined ? '' : path;
    }

    const response = await this.httpService.request(
      'companies/fillData',
      'PUT',
      JSON.stringify({
        description,
        categoryIds,
        photoUris,
        socialMediaUris,
        isPrepaymentAvailable
      }));

    if (response.result == 'successful') {
      for (let i = 0; i < 6; i++) {
        if (sources[i] != '')
          await this.objectStorageService.upload(sources[i], photoUris[i]);
      }
    }

    return response;
  }

  async getCompanies(categoryId: number): Promise<HttpResponseWithContent<CompanyMapMarker[]>> {
    return await this.httpService.requestWithContent<CompanyMapMarker[]>(
      `companies/${categoryId}`,
      'GET',
      undefined);
  }
}
