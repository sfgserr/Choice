export type UserClaims = {
  sub: string;
  type: string;
};

export type TokenResponse = {
  access_token: string;
  refresh_token: string;
};

