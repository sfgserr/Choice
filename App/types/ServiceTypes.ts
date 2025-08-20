import {UserType} from '../enums/ModelEnums.ts';

export type UserClaims = {
  sub: string
  type: string
  subscribed: boolean | undefined
  banned: boolean | undefined
};

export type TokenResponse = {
  access_token: string
  refresh_token: string
};

export type HttpResponseWithContent<T> = {
  content: T | null
  result: 'successful' | 'unauthorized' | 'bad_request'
  error: string
};

export type HttpResponse = {
  result: 'successful' | 'unauthorized' | 'bad_request',
  error: string
};

export type Token = {
  id: string
  userType: UserType
  subscribed: boolean | undefined
  banned: boolean | undefined
}

