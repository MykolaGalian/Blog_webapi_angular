import { UserService } from '../../shared/user.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(public service: UserService, public toastr: ToastrService) { }  


  ngOnInit() {
    this.service.formModel.reset();
    
  }

  onSubmit() {
    this.service.register().subscribe(
      (res: any) => {
        if (res !== null) {
          this.service.formModel.reset();
          console.log(res); 
          this.toastr.success('New user created! Please Log In');
        }         
      },
      err => {
        console.log(err);              
        this.toastr.error('HTTP status code', err.status);
      }
    );
  }
}
