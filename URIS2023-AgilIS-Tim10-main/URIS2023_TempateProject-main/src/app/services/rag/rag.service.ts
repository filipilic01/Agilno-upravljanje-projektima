import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Observable} from 'rxjs';
import { RagCreation, RagUpdate } from 'app/models/rag';

@Injectable({
  providedIn: 'root'
})
export class RagService {

  constructor(private http: HttpClient) { }

  private base_url = "http://localhost:5255/rag"


  public postRag(rag:RagCreation) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(rag);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.base_url}`, rag, {headers})
  }

  public updateRag(rag:RagUpdate) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(rag);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.put(`${this.base_url}`, rag, {headers})
  }

  public deleteRag(id:string) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(id);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.base_url}/${id}`, {headers})
  }
}
