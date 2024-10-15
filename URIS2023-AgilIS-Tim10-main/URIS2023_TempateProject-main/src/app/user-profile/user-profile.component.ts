import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserDto } from 'app/models/user';
import { UserServiceService } from 'app/services/user-service.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  users : UserDto[]=[]
  constructor(private userService: UserServiceService, private snackBar:MatSnackBar) { }

  ngOnInit() {
    this.userService.getUsers().subscribe(res => {
      if(res){
        this.users=res;
      }
      else{
        this.snackBar.open(`You don't have permission to this section!`, 'OK', {
          duration: 2500
        });
      }
    })
  }

}
