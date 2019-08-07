import { Post } from '../post/post';

export class UserDetails {    
    Id: number;
    Login: string;
    Name: string;
    LastName: string;
    Address: string;
    UserAvatar: string;
    DateRegistration: Date;  
    Email: string;  
    Posts:Post[];  
    IsAdmin: Boolean;
    IsModerator: Boolean;
    IsBlocked: Boolean;  
}