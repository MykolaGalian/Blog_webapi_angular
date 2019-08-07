import { Component, OnInit } from '@angular/core';
import {NgForm} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  formModel = {
    Login: '',
    Password: ''};

    

  constructor(private service: UserService, private toastr: ToastrService, private router: Router) { }

  ngOnInit() {
    if (localStorage.getItem('access_token') != null)
   
    this.router.navigate(['/profile']);
  }

  onSubmit(form: NgForm) {

    
    this.service.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem('access_token', res.access_token);//запись токена в localStorage браузера
        localStorage.setItem('Login', form.value.Login);    
        console.log(form.value.Login);   
        this.router.navigateByUrl('/profile'); //переход на страницу профиля   this.router.navigateByUrl('/profile');   
             
      },  
        err => {
            console.log(err);              
            this.toastr.error('HTTP status code', err.status);
      }
      
    );
  }

}
