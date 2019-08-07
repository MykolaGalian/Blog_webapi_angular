import { Component, OnInit } from '@angular/core';
import { PostService } from '../../../shared/post.service';
import { CommentService } from '../../../shared/comment.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-read-post',
  templateUrl: './read-post.component.html',
  styleUrls: ['./read-post.component.css']
})
export class ReadPostComponent implements OnInit {

  private userLogin: string = '';
  private commentText: string = '';

  constructor(private postService: PostService, private commentService: CommentService,private toastr: ToastrService) { }

  ngOnInit() {
   this.userLogin =this.postService.post.UserInfo.Login;
  
  }

  AutorizeCheck() {
    return (localStorage.getItem('access_token') === null)
  }

  OnAddComment(comment:string) {
    this.commentService.AddComment(this.postService.post.Id, comment).subscribe((data: any) => {
      this.toastr.success('Comment added');
      this.postService.GetPostsById(this.postService.tempPostId);
        this.commentText = '';
      },
      Error => {
        this.toastr.error('HTTP status code', Error.status);
      });
  }

  OnDeleteComment(commentId: number){

    this.commentService.RemoveComment(commentId).subscribe((data: any) => {
      this.toastr.success('Comment deleted');
      this.postService.GetPostsById(this.postService.tempPostId);
    },
    Error => {
      this.toastr.error('HTTP status code', Error.status);
    });


  }

}
