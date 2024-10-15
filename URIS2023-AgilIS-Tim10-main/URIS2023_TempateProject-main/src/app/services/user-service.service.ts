import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from 'app/models/user';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {

  baseUrl = "http://localhost:5255/user"

  constructor(private http: HttpClient) { }

  public getUsers() : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}`, {headers});
  }

  
  public createUser(user: User): Observable<any> {
    return this.http.post(this.baseUrl, user);
  }

  private getUrl() {
    return `${this.baseUrl}`;
  }
}
