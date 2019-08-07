import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../shared/admin.service';
import { UserService } from '../../../shared/user.service';


@Component({
  selector: 'app-admin-commands',
  templateUrl: './admin-commands.component.html',
  styleUrls: ['./admin-commands.component.css']
})
export class AdminCommandsComponent implements OnInit {

  constructor(private adminService: AdminService, private service: UserService) { }

  ngOnInit() {
  }

}
