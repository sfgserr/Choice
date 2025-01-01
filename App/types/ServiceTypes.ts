export type UserClaims = {
  sub: string;
  type: string;
};

export type TokenResponse = {
  access_token: string;
  refresh_token: string;
};

export type HttpResponseWithContent<T> = {
  content: T | null,
  result: 'successful' | 'unauthorized' | 'bad_request'
};

export type HttpResponse = {
  result: 'successful' | 'unauthorized' | 'bad_request'
};

