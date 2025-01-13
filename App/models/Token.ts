import {UserType} from '../enums/ModelEnums.ts';

export class Token {
  id: string;
  userType: UserType;
  subscribed: boolean | undefined

  constructor(id: string, userType: UserType, subscribed: boolean | undefined) {
    this.id = id;
    this.userType = userType;
    this.subscribed = subscribed;
  }
}
