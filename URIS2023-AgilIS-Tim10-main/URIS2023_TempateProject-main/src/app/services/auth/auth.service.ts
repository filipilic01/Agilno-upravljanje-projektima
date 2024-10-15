import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthCreds } from 'app/models/auth-creds';
import { JwtToken } from 'app/models/jwt-token';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, catchError, map } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
    
    url1 = "http://localhost:5255/auth";
    url2 = "http://localhost:5255/user/currentUser";
    private currentUserSource = new BehaviorSubject<JwtToken | null>(null);
    currentUser$ = this.currentUserSource.asObservable();
    constructor(private http: HttpClient, private router: Router){
      const storedToken = localStorage.getItem('token');
      if (storedToken) {
        
        const jwt: JwtToken = {
          token: storedToken,
          expiresOn: localStorage.getItem('expires') || '',
          username: localStorage.getItem('username') || '',
          role: localStorage.getItem('role') || '',
          userId: localStorage.getItem('id') || ''
        };
        this.currentUserSource.next(jwt);
      }
    }

    

    loadCurrentUser(token: string){
      let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);

      return this.http.get<JwtToken>(`${this.url2}`, {headers}).pipe(
        map(jwt => {
          console.log(jwt);
          localStorage.setItem('token', jwt.token);
          localStorage.setItem('expires', jwt.expiresOn);
          localStorage.setItem('username', jwt.username);
          localStorage.setItem('role', jwt.role);
          localStorage.setItem('id', jwt.userId);
          this.currentUserSource.next(jwt);
          return jwt;
        })
      )
    }

    login(authCreds: AuthCreds): Observable<any> {
        return this.http.post<JwtToken>(this.url1, authCreds).pipe(
          map(jwt => {
            localStorage.setItem('token', jwt.token);
            localStorage.setItem('expires', jwt.expiresOn);
            localStorage.setItem('username', jwt.username);
            localStorage.setItem('role', jwt.role);
            localStorage.setItem('id', jwt.userId);
            this.currentUserSource.next(jwt);
            return jwt;
          }),
          catchError(error => {
            console.error('Error during login', error);
            throw error;
          })
        );
          
        
    }

    logout(){
      localStorage.removeItem('token');
      localStorage.removeItem('expires');
      localStorage.removeItem('role');
      localStorage.removeItem('username');
      localStorage.removeItem('id');
      this.currentUserSource.next(null);
      this.router.navigateByUrl('/home');
    }
}