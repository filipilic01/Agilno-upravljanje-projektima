import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Project } from 'app/models/project';
@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  private base_url ="http://localhost:5255/Projekat"

  constructor(private http: HttpClient) { }

  
  public postProject(project:Project) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(project);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.base_url}`, project, {headers})
  }

  public updateProject(id:string, project:Project) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(project);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.put(`${this.base_url}/${id}`, project, {headers})
  }

  public deleteProject(id:string) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(id);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.base_url}/${id}`, {headers})
  }
}
