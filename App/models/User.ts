export enum UserType {
  User,
  Client,
  Company,
  Admin
}

export class User {
  id: string;
  userType: UserType;

  constructor(id: string, userType: UserType) {
    this.id = id;
    this.userType = userType;
  }
}
