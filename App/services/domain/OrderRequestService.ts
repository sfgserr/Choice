import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {CompanyOrderRequest, OrderRequest, OrderRequestDetails, OrderRequestRadius} from '../../types/DomainTypes.ts';
import {ObjectStorageService} from '../object/ObjectStorageService.ts';
import {FilePathUtils} from '../../utils/FilePathUtils.ts';

export class OrderRequestService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;
  private readonly objectStorageService: ObjectStorageService;

  constructor(httpService: RefreshTokenHttpServiceDecorator, objectStorageService: ObjectStorageService) {
    this.httpService = httpService;
    this.objectStorageService = objectStorageService;
  }

  async create(
    categoryId: number,
    description: string,
    toKnowPrice: boolean,
    toKnowDeadline: boolean,
    toKnowEnrollmentDate: boolean,
    photoUris: string[],
    distance: number) {
    const sources = ['', '', ''];
    for (let i = 0; i < 3; i++) {
      sources[i] = photoUris[i];
      let path = FilePathUtils.getFileName(photoUris[i]);

      photoUris[i] = path == undefined ? '' : path;
    }

    const response = await this.httpService.requestWithContent<OrderRequest>(
      'orderRequests',
      'POST',
      JSON.stringify({
        categoryId,
        description,
        toKnowPrice,
        toKnowDeadline,
        toKnowEnrollmentDate,
        photoUris,
        distance
      }));

    if (response.result == 'successful') {
      for (let i = 0; i < 3; i++) {
        if (sources[i] != '')
          await this.objectStorageService.upload(sources[i], photoUris[i]);
      }
    }

    return response;
  }

  async edit(
    requestId: string,
    categoryId: number,
    description: string,
    toKnowPrice: boolean,
    toKnowDeadline: boolean,
    toKnowEnrollmentDate: boolean,
    photoUris: string[],
    distance: number) {
    const toUpload = ['', '', ''];

    for (let i = 0; i < 3; i++) {
      if (photoUris[i].includes('file:///')) {
        toUpload[i] = photoUris[i];
        let path = FilePathUtils.getFileName(photoUris[i]);

        photoUris[i] = path == undefined ? '' : path;
      }
    }

    const response = await this.httpService.request(
      'orderRequests',
      'PUT',
      JSON.stringify({
        requestId,
        categoryId,
        description,
        toKnowPrice,
        toKnowDeadline,
        toKnowEnrollmentDate,
        photoUris,
        distance
      }));

    if (response.result == 'successful') {
      for (let i = 0; i < 3; i++) {
        if (toUpload[i] != '')
          await this.objectStorageService.upload(toUpload[i], photoUris[i]);
      }
    }

    return response;
  }

  async getOrderRequests() {
    return await this.httpService.requestWithContent<OrderRequest[]>(
      'orderRequests',
      'GET',
      undefined
    );
  }

  async getOrderRequest(id: string) {
    return await this.httpService.requestWithContent<OrderRequestDetails>(
      `orderRequests/${id}`,
      'GET',
      undefined);
  }

  async getOrderRequestsRadius() {
    return await this.httpService.requestWithContent<OrderRequestRadius[]>(
      'orderRequests/radius',
      'GET',
      undefined);
  }

  async getOrderRequestAsCompany(id: string) {
    return await this.httpService.requestWithContent<CompanyOrderRequest>(
      `orderRequests/company/${id}`,
      'GET',
      undefined);
  }
}
