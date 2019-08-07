import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Editpost } from '../model/post/editpost';

import { NgForm } from '@angular/forms';
import { Post } from '../model/post/post';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  
  private postOnEdit:boolean=false;

  tegslist: string[] =null;
  tempPostId: number = 0;
  postsByTeg:  Post[] = null;
  moderPosts:  Post[] = null;
  posts: Post[] = null;
  post: Post =null;
  readonly rootUrl = 'https://localhost:44302/api/post';

  constructor(private http: HttpClient, private router: Router) { }

  GetPosts() {
    // if (localStorage.getItem("Login") == null) {
       var reqHeader = new HttpHeaders({ 'No-Auth': 'True' });
       
       return this.http.get(this.rootUrl + '/getallposts', {headers:reqHeader}).
 
       subscribe((res: any) => {
           this.posts = res as Post[]; 
           
 
           for (let i = 0; i < this.posts.length; i++) {
           this.posts[i].Src = this.rootUrl +'/image/get?imageName=' + this.posts[i].PostPicture +'&userLogin=' + this.posts[i].UserInfo.Login;
          }
         })
       }

       GetPostsById(PostId: number) {
        var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});           
           return this.http.get(this.rootUrl + '/' +PostId, {headers:tokenHeader}).
     
           subscribe((res: any) => {
               this.post = res as Post;  
               this.post.Src = this.rootUrl +'/image/get?imageName=' + this.post.PostPicture +'&userLogin=' + this.post.UserInfo.Login;
              
             })
           }

  GetAllPosts(){    
    var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});       
       return this.http.get(this.rootUrl + '/getallpostsModer', {headers:tokenHeader}).
 
       subscribe((res: any) => {
           this.moderPosts = res as Post[]; 
           
 
           for (let i = 0; i < this.moderPosts.length; i++) {
           this.moderPosts[i].Src = this.rootUrl +'/image/get?imageName=' + this.moderPosts[i].PostPicture +'&userLogin=' + this.moderPosts[i].UserInfo.Login;
          }
       })
  }
   
   AddPost(data: any, fileToUpload: File)  {  
      const formData: FormData = new FormData();
      formData.append('PostPicture', fileToUpload, fileToUpload.name);     
      formData.append("Post", btoa(data.value.body));   //шифруем HTML разметку перед отправкой utf-8
      formData.append("Title", data.value.title);
      formData.append("Hashtags", data.value.hashtags);  
    
    var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});
    return this.http.post(this.rootUrl+'/add', formData, {headers: tokenHeader});
  }
  
  EditPost(postId: number, form: NgForm) {
    const body: Editpost = {
      PostBody: form.value.PostBody, //PostBody - Body
      PostTitle: form.value.PostTitle,
      Hashtags: form.value.Hashtags     
    }
    var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});
    return this.http.put(this.rootUrl + '/'+postId , body, {headers: tokenHeader});
  }


  DeletePost(Id: number) {
    var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});
    return this.http.delete(this.rootUrl + '/' + Id, {headers: tokenHeader} );
  }  

  GetTegs(){
    var reqHeader = new HttpHeaders({ 'No-Auth': 'True' });       
       return this.http.get(this.rootUrl + '/getalltegs', {headers:reqHeader}).
 
       subscribe((res: any) => {
           this.tegslist = res as string[]; 
  });  
 }
  SearchByTeg(teg: string){
    const formData: FormData = new FormData();
    formData.append("Teg", teg); 
    var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});

    return this.http.post(this.rootUrl + '/getbyteg',formData, {headers: tokenHeader} ).
            subscribe((res: any) => {
              this.postsByTeg = res as Post[]; 
              for (let i = 0; i < this.postsByTeg.length; i++) {
                this.postsByTeg[i].Src = this.rootUrl +'/image/get?imageName=' + this.postsByTeg[i].PostPicture +'&userLogin=' + this.postsByTeg[i].UserInfo.Login;
              }     
              this.router.navigateByUrl('/post-byteg'); 
        });  
  }

 
 
}


