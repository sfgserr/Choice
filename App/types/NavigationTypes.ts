import {UserService} from '../services/UserService.ts';
import {CategoriesService} from '../services/CategoriesService.ts';

export type StackProps = {
  Login: {userService: UserService};
  Loading: undefined;
};

export type ClientTabProps = {
  Categories: {categoriesService: CategoriesService};
  OrderRequests: undefined;
  Chat: undefined;
  Account: undefined;
};
