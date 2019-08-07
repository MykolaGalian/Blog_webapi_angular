import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'


@Injectable({
  providedIn: 'root'
})
export class CommentService {

  readonly rootUrl = 'https://localhost:44302/api/comments/';

  constructor(private http: HttpClient) { }

  AddComment(postId:number, comment:string) {
     
    var Header = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});  
    const formData: FormData = new FormData();
    formData.append("Coment", comment); 
    return this.http.post(this.rootUrl  + postId, formData,{ headers: Header } ); 
  }

  RemoveComment( comentId: number) {
    var Header = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});
    return this.http.delete(this.rootUrl + comentId,{ headers: Header } );   

  }
}
