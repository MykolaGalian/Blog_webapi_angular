import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders} from '@angular/common/http'
import { ToastrService } from 'ngx-toastr';
import { PostService } from '../shared/post.service';

@Injectable({
  providedIn: 'root'
})
export class ModeratorService {

  readonly rootUrl = 'https://localhost:44302/api/moder';

  constructor(private http: HttpClient,private toastr: ToastrService, private postService: PostService,) { }

 
  BlockPost(PostId: string) {
    console.log(PostId + " :postdId");

    var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});
    return this.http.get(this.rootUrl + "/block/post/" + PostId, {headers: tokenHeader}).subscribe
    (
      (res:any) => {
        this.toastr.success('Post Blocked');
        this.postService.GetAllPosts();
        console.log("updated");
       },
      err => {
        console.log(err);
        this.toastr.error('HTTP status code', err.status);
      },
    );

  }

  UnblockPost(PostId: string) {

    console.log(PostId + " :postdId");
    var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});
    return this.http.get(this.rootUrl + "/unblock/post/" + PostId, {headers: tokenHeader}).subscribe
    (
      (res:any) => {
        this.toastr.success('Post Unblocked');
        this.postService.GetAllPosts();
        console.log("updated");
       },
      err => {
        console.log(err);
        this.toastr.error('HTTP status code', err.status);
      },
    );



  }
}
