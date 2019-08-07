import { UserDetails } from '../user/user-details';
export class Post {
  Id: number;
  PostBody: string;
  PostTitle: string;
  PostPicture: string;
  UserInfoId: number;
  UserInfo: UserDetails;
  DateCreate: Date;
  LastEdit: Date;
  Comments: Comment[];
  Hashtags: string; 
  Src:string;   // путь к картирке поста
  IsBlocked: Boolean
}
