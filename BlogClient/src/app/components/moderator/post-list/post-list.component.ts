import { Component, OnInit } from '@angular/core';
import { PostService } from '../../../shared/post.service';
import { CommentService } from '../../../shared/comment.service';
import { UserService } from '../../../shared/user.service';
import { ModeratorService } from '../../../shared/moderator.service';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.css']
})
export class PostListComponent implements OnInit { 

  constructor(private postService: PostService,private commentService: CommentService,
    private service: UserService, private moderService: ModeratorService) { }

  ngOnInit() {
    this.postService.GetAllPosts();
  }
  

}
