import {Status} from '../enums/AccountManagerEnums.ts';

export type FetchAccountResult = {
  status: Status;
  tokens: string[];
};
