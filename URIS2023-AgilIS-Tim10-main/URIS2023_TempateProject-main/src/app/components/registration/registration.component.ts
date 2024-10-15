import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { User } from 'app/models/user';
import { UserServiceService } from 'app/services/user-service.service';


@Component({
  selector: 'registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  user: User = new User();
  users: User [];
  Role: string;

  constructor(private userService: UserServiceService, private snackBar: MatSnackBar, private route: Router) {}

  ngOnInit(): void {

  }
  
  registerUser() {
    if (!this.user.name || !this.user.surname || !this.user.email || !this.user.username || !this.user.password || !this.Role) {
      this.snackBar.open('Please fill the all fields!', 'OK', {
        duration: 2500
      })
    }
    else{
      if(this.Role == 'Admin'){
        this.user.role = 0;
      }
      else if(this.Role == 'Scrum Master'){
        this.user.role = 1;
      }
      else if(this.Role == 'Product Owner'){
        this.user.role = 2;
      }
      else if(this.Role == 'Stakeholder'){
        this.user.role = 4;
      }
      else if(this.Role == 'Developer'){
        this.user.role = 3;
      }
  
      this.userService.createUser(this.user).subscribe(
        (response) => {
          console.log('Successfully registered user:', response);
          this.user = new User();
          this.route.navigateByUrl('/login');
        },
        (error) => {
          console.error('Error during user registration', error);
          console.log('Odgovor sa servera:', error.error);
        }
      );
    }
    
  }
  
  

}
