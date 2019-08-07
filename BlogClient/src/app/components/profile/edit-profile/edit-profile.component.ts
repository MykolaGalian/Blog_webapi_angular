import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from '../../../shared/user.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

 
  constructor(private router: Router,private service: UserService, private toastr: ToastrService ) { }

  ngOnInit() {
    console.log(this.service.userDetails.Id);
    console.log(this.service.userDetails.Login);

   this.initEditProfile();

  }
 
  initEditProfile(){
  this.service.editProfile =
  { Id: this.service.userDetails.Id,
    Login: this.service.userDetails.Login,
    Name: this.service.userDetails.Name,
    LastName: this.service.userDetails.LastName,
    Email: this.service.userDetails.Email,
    Address: this.service.userDetails.Address,
    UserAvatar: this.service.userDetails.UserAvatar,
    DateRegistration: this.service.userDetails.DateRegistration,
    Posts: this.service.userDetails.Posts,
    IsAdmin: this.service.userDetails.IsAdmin,
    IsModerator: this.service.userDetails.IsModerator,
    IsBlocked: this.service.userDetails.IsBlocked
    };

    this.service.editPassword ={
      oldPassword:"",
      newPassword:"",
      confirmPassword:""
    };

  }

  OnSubmit() {
    this.service.EditUserProfile().
      subscribe(
        (res:any) => {
          this.service.getUserProfile();  
          this.toastr.success('User profile updated');
          console.log("updated");
            
         },
        err => {
          console.log(err);
          this.toastr.error('HTTP status code', err.status);
        }
    
      );       
      
  }

  OnSubmitPass() {
    this.service.ChangePassword().
    subscribe(
      (res:any) => {
        this.toastr.success('User password updated');
        console.log("updated");
        this.ngOnInit();
       },
      err => {
        console.log(err);
        this.toastr.error('HTTP status code', err.status);
      }
  
    );     


  }
  OnDeleteAcc() {
    this.service.DeleteAccount().
    subscribe(
      (res:any) => {
        this.toastr.success('User profile deleted');
        console.log("deleted");
        localStorage.removeItem('access_token');
        localStorage.removeItem('Login');
        this.router.navigate(['/login']);

       },
      err => {
        console.log(err);
        this.toastr.error('HTTP status code', err.status);
      }
  
    );     
     
  }

}
