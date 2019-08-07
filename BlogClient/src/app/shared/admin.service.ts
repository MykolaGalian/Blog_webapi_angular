import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http'
import { UserService } from '../shared/user.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  readonly rootUrl = 'https://localhost:44302/api/admins';

  constructor(private http: HttpClient, private service: UserService,private toastr: ToastrService) { }
 

  BlockAccount() {
    
    var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});
    return this.http.get(this.rootUrl + "/block/user/" + this.service.userDetails.Login, {headers: tokenHeader}).
    subscribe(
      (res:any) => {
        this.toastr.success('User Blocked');
        this.service.getAllUserProfiles();
        console.log("updated");
       },
      err => {
        console.log(err);
        this.toastr.error('HTTP status code', err.status);
      }
    );

  }

  UnblockAccount() {
    var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});
    return this.http.get(this.rootUrl + "/unblock/user/" + this.service.userDetails.Login,  {headers: tokenHeader}).
    subscribe(
      (res:any) => {
        this.toastr.success('User Unblocked');
        this.service.getAllUserProfiles();
        console.log("updated");
       },
      err => {
        console.log(err);
        this.toastr.error('HTTP status code', err.status);
      },
    );
  }

 }
