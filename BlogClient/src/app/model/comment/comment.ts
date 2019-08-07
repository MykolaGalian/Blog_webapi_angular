import { UserDetails } from '../user/user-details';
export class Comment {
    Id: number;
    CommentBody: string;
    PostId: number;
    DateCreation: Date;
    LastEdit: Date;
    UserInfoId: number;
    UserInfo: UserDetails;
    
}
