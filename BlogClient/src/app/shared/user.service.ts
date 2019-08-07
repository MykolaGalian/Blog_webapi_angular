import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient,HttpHeaders } from "@angular/common/http";
import { UserDetails } from '../model/user/user-details';
import {UserEdit} from '../model/user/user-edit';
import { ToastrService } from 'ngx-toastr';
import {ChangePass} from '../model/user/change-pass';



@Injectable({
  providedIn: 'root'
})
export class UserService {  

  constructor(private fb:FormBuilder,private http: HttpClient, private toastr: ToastrService) {     
  }
 

  editProfile: UserEdit =null ;
  editPassword: ChangePass = null;

  userDetails : UserDetails =null;
  usersDetails : UserDetails[] =null;

  readonly BaseURI = 'https://localhost:44302';  
  imageUrl: any = "/assets/img/unknown-user.png";
  readonly rootUrl = 'https://localhost:44302/api/';

  formModel = this.fb.group({
    Login: ['',Validators.required],      
    Email: ['',Validators.email],
    Name: [''],
    LastName: [''],
    Address: [''],
    Passwords: this.fb.group({
      Password: ['',[Validators.required,Validators.minLength(6)]],
      ConfirmPassword: ['',Validators.required]
    }, { validator: this.comparePasswords })
  });

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');
    
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password').value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);
    }
  }

  register() {
    var body = {
      Login: this.formModel.value.Login,
      Email: this.formModel.value.Email,
      Name: this.formModel.value.Name,
      LastName: this.formModel.value.LastName,
      Address: this.formModel.value.Address,
      Password: this.formModel.value.Passwords.Password,
      ConfirmPassword: this.formModel.value.Passwords.ConfirmPassword
    };
   localStorage.removeItem('access_token');  
    return this.http.post(this.rootUrl + 'accounts/Register', body);
  }

  login(formData) {  

    var body = "username=" + formData.Login + "&password=" + formData.Password + "&grant_type=password";
    localStorage.removeItem('access_token');
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/x-www-urlencoded', 'No-Auth': 'True'});
    return this.http.post(this.BaseURI + '/token', body, { headers: reqHeader });    
  }
 

  getUserProfile() {      
    var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});
    return this.http.get(this.rootUrl + 'users/profile', {headers: tokenHeader}).subscribe(
      (res:any) => {
        this.userDetails = res;
        if (this.userDetails.UserAvatar !== null) {   
          this.imageUrl = this.rootUrl+'users/image/get?imageName=' + this.userDetails.UserAvatar + '&userLogin=' + this.userDetails.Login;
        }
          if (this.userDetails.Posts != null)
            for (let i of this.userDetails.Posts) {
              i.Src = this.rootUrl+'post/image/get?imageName=' + i.PostPicture +'&userLogin=' + this.userDetails.Login;
            }
       },
      err => {
        console.log(err);
      },
    );
  }

  getAllUserProfiles() {      
    var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});
    return this.http.get(this.rootUrl + 'users/allprofiles', {headers: tokenHeader}).toPromise()
    .then(res => this.usersDetails = res as UserDetails[]);
  }

  GetUserByLogin(login: string) {
    
     var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});
      return this.http.get(this.rootUrl + 'users/' + login, { headers: tokenHeader });   
    
  }
    
  UpdateAvatar(fileToUpload: File) {
    const endpoint = this.rootUrl + 'users/avatar/set';
    const formData: FormData = new FormData();
    formData.append('Image', fileToUpload, fileToUpload.name);

    var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});
    return this.http.post(endpoint, formData, { headers: tokenHeader });
  }


  EditUserProfile()  {
  var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});
  return this.http.put(this.rootUrl + 'users/profile/edit', this.editProfile, { headers: tokenHeader });
  }

ChangePassword() {
  
   var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});
  return this.http.put(this.rootUrl + 'users/profile/password', this.editPassword, { headers: tokenHeader });
}

DeleteAccount() {
  var tokenHeader = new HttpHeaders({'Authorization': 'Bearer  ' + localStorage.getItem('access_token')});
  return this.http.delete(this.rootUrl + 'users/profile/edit', { headers: tokenHeader });
}

}
