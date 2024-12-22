import {UserType} from '../enums/ModelEnums.ts';

export class Token {
  id: string;
  userType: UserType;

  constructor(id: string, userType: UserType) {
    this.id = id;
    this.userType = userType;
  }
}
