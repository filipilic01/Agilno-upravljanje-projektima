import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BacklogItem } from 'app/models/backog-item';
import { ProductBacklog, ProductBacklogCreation } from 'app/models/product-backlog';
import { SprintBacklog, SprintBacklogCreation } from 'app/models/sprint-backlog';
import { map } from 'jquery';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BacklogService {

  baseUrl = "http://localhost:5255/backlogs"
  /*
  baseUrl_item = "http://localhost:5255/backlogs/backlogItem"
  baseUrl_task = "http://localhost:5255/backlogs/tasks"
  baseUrl_func = "http://localhost:5255/backlogs/functionalities"
  baseUrl_story = "http://localhost:5255/backlogs/userStories"
  baseUrl_epic = "http://localhost:5255/backlogs/epics"
  baseUrl_sprint = "http://localhost:5255/backlogs/sprintBacklogs"
  baseUrl_product = "http://localhost:5255/backlogs/productBacklogs"
  baseUrl_product_proj = "http://localhost:5255/backlogs/product/projekat"
  baseUrl_sprint_proj = "http://localhost:5255/backlogs/sprint/projekat"*/

  constructor(private http: HttpClient) { }

  public getBacklogs() : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}`, {headers})
  }

  public getBacklogItemsForBacklogId(id): Observable<BacklogItem[]>{
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get<BacklogItem[]>(`${this.baseUrl}/backlogItem/${id}`, {headers})
  }

  public getEpicsForBacklogId(id): Observable<any>{
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}/epics/${id}`, {headers})
  }

  public getUserStoriesForBacklogId(id): Observable<any>{
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}/userStories/${id}`, {headers})
  }

  public getFunctionalitiesForBacklogId(id): Observable<any>{
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}/functionalities/${id}`, {headers})
  }

  public getTasksForBacklogId(id): Observable<any>{
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}/tasks/${id}`, {headers})
  }

  public getSprintBacklog(id): Observable<any>{
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}/sprintBacklogs/${id}`, {headers})
  }

  public getProductBacklog(id): Observable<any>{
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}/productBacklogs/${id}`, {headers})
  }

  public getProductBacklogByProjekatId(id): Observable<any>{
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}/product/projekat/${id}`, {headers})
  }

  public getSprintBacklogByProjekatId(id): Observable<any>{
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}/sprint/projekat/${id}`, {headers})
  }


  public postSprintBacklog(backlog: SprintBacklogCreation) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.baseUrl}/sprintBacklogs`,backlog, {headers})
  }

  public postProductBacklog(backlog: ProductBacklogCreation) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.baseUrl}/productBacklogs`,backlog, {headers})
  }

  public updateProductBacklog(backlog: ProductBacklog) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.put(`${this.baseUrl}/productBacklogs`,backlog, {headers})
  }

  public updateSprintBacklog(backlog: SprintBacklog) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.put(`${this.baseUrl}/sprintBacklogs`,backlog, {headers})
  }
}
