import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Observable} from 'rxjs';
import { StatusCreation, StatusUpdate } from 'app/models/status';

@Injectable({
  providedIn: 'root'
})
export class StatusService {

  private base_url = "http://localhost:5255/status"

  constructor(private http: HttpClient) { }

  
  public postStatus(status:StatusCreation) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(status);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.base_url}`, status, {headers})
  }

  public updateStatus(status:StatusUpdate) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(status);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.put(`${this.base_url}`, status, {headers})
  }

  public deleteStatus(id:string) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(id);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.base_url}/${id}`, {headers})
  }
}
