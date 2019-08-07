import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../shared/user.service';
import { UserDetails } from 'src/app/model/user/user-details';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  constructor(private service: UserService) { }

  ngOnInit() {
    this.service.getAllUserProfiles();
 
  }
  populateForm(pd: UserDetails){
     this.service.userDetails = Object.assign({}, pd);
     
  }
}
