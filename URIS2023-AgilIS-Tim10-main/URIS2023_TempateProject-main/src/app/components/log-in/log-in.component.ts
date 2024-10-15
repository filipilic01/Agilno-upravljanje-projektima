import { Component, OnInit } from '@angular/core';
import { AuthCreds } from 'app/models/auth-creds';
import { JwtToken } from 'app/models/jwt-token';
import { User } from 'app/models/user';
import { AuthService } from 'app/services/auth/auth.service';
import { UserServiceService } from 'app/services/user-service.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.scss']
})
export class LogInComponent implements OnInit {

  users: User [];
  authCreds: AuthCreds = new AuthCreds();
  jwtToken: JwtToken;
  constructor(private userService: UserServiceService, private loginService: AuthService, private route: Router, private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.startSubscription();
  }
  startSubscription() {
   
  }

  onLogin(){
   if(!this.authCreds.Username || !this.authCreds.Password){
      this.snackBar.open('Please fill all fields!', 'OK', {
        duration: 2500
      });
    } else {
      this.loginService.login(this.authCreds).subscribe(
        () => {
          this.snackBar.open('Successfully logged in!', 'OK', {
            duration: 2500
          });
          this.route.navigateByUrl('/home');
        },
        (error) => {
          console.error('Login error:', error);
          let errorMessage = 'Error during login';

          if (error.status === 404 || error.status === 403) {
            errorMessage = 'Wrong username or password! Try again!';
          }

          this.snackBar.open(errorMessage, 'OK', {
            duration: 2500
          });
        }
      );
    }
  }
    
    
  

}
