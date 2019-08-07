import { Component, OnInit } from '@angular/core';
import { PostService } from '../../../shared/post.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { CommentService } from '../../../shared/comment.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-post',
  templateUrl: './user-post.component.html',
  styleUrls: ['./user-post.component.css']
})
export class UserPostComponent implements OnInit {

  private postOnEdit:boolean=false;
  private commentText: string = '';

  constructor(private postService: PostService, private router: Router,
     private commentService: CommentService,private toastr: ToastrService) {}

  

  ngOnInit() {
  }


  OnPostEdit(form:NgForm) {
    this.postService.EditPost(this.postService.post.Id, form).subscribe((data: any) => {
        this.postOnEdit = false;
        this.toastr.success('Post edited');
        },
        Error => {console.log(Error);
          this.toastr.error('HTTP status code', Error.status);
        });
  }


  OnDeletePost() {
    this.postService.DeletePost(this.postService.post.Id).subscribe((data: any) => {
      this.toastr.success('Post deleted');
        this.router.navigate(['/profile']);
      },
      Error => {console.log(Error);
        this.toastr.error('HTTP status code', Error.status);
      });
  }

  OnAddComment(comment:string) {
    this.commentService.AddComment(this.postService.post.Id, comment).subscribe((data: any) => {
      this.toastr.success('Comment Added');
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
