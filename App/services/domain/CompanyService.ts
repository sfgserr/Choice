import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {CompanyInfo, CompanyMapMarker} from '../../types/DomainTypes.ts';
import {HttpResponseWithContent} from '../../types/ServiceTypes.ts';
import {FilePathUtils} from '../../utils/FilePathUtils.ts';
import {ObjectStorageService} from '../object/ObjectStorageService.ts';
import {MinioBlob} from "../../components/ImageBox.tsx";

export class CompanyService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(
    httpService: RefreshTokenHttpServiceDecorator
  ) {
    this.httpService = httpService;
  }

  async createCompany(
    name: string,
    password: string,
    email: string,
    phoneNumber: string,
    city: string,
    street: string,
  ) {
    return await this.httpService.request(
      'companies',
      'POST',
      JSON.stringify({
        name,
        password,
        email,
        phoneNumber,
        city,
        street,
      }),
    );
  }

  async fillData(
    description: string,
    categoryIds: number[],
    photoUris: string[],
    socialMediaUris: string[],
    isPrepaymentAvailable: boolean,
  ) {
    return await this.httpService.request(
      'companies/fillData',
      'PUT',
      JSON.stringify({
        description,
        categoryIds,
        photoUris,
        socialMediaUris,
        isPrepaymentAvailable,
      }),
    );
  }

  async getCompanies(
    categoryId: number,
  ): Promise<HttpResponseWithContent<CompanyMapMarker[]>> {
    return await this.httpService.requestWithContent<CompanyMapMarker[]>(
      `companies/${categoryId}`,
      'GET',
      undefined,
    );
  }

  async getCompanyOnMap(companyId: string) {
    return await this.httpService.requestWithContent<CompanyInfo>(
      `companies/map/${companyId}`,
      'GET',
      undefined,
    );
  }

  async changeData(
    name: string,
    phoneNumber: string,
    email: string,
    city: string,
    street: string,
    description: string,
    categories: number[],
    photoUris: string[],
    socialMediaUris: string[],
    isPrepaymentAvailable: boolean) {
    return await this.httpService.request(
      'companies',
      'PUT',
      JSON.stringify({
        name,
        phoneNumber,
        email,
        city,
        street,
        description,
        categories,
        photoUris,
        socialMediaUris,
        isPrepaymentAvailable,
      }));
  }
}
