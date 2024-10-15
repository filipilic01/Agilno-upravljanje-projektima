import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AcceptanceCriteria, AcceptanceCriteriaCreation, AcceptanceCriteriaUpdate } from 'app/models/acceptance-criteria';
import { BacklogItem, BacklogItemDto } from 'app/models/backog-item';
import { DescriptionCreation, DescriptionUpdate } from 'app/models/description';
import { StoryPointCreation, StoryPointUpdate } from 'app/models/story-point';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BacklogItemService {

  private base_url ="http://localhost:5255/backlogItems"

  constructor(private http: HttpClient) { }

  //Backlog items
  public updateBacklogItem(item: BacklogItem): Observable<any>{
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
      return this.http.put(`${this.base_url}`, item , {headers})

  }

  public deleteBacklogItem(id:string) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(id);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.base_url}/${id}`, {headers})
  }
  //Acceptance criteria
  public getAcceptanceCriteria(id): Observable<any>{
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.base_url}/criteria/${id}`, {headers})
  }

  public postAcceptanceCriteria(criteria:AcceptanceCriteriaCreation) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(criteria);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.base_url}/acceptanceCriteria`, criteria, {headers})
  }

  public updateAcceptanceCriteria(criteria:AcceptanceCriteriaUpdate) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(criteria);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.put(`${this.base_url}/acceptanceCriteria`, criteria, {headers})
  }

  public deleteAcceptanceCriteria(id:string) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(id);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.base_url}/acceptanceCriteria/${id}`, {headers})
  }

  //Description
  public getDescription(id): Observable<any>{
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.base_url}/description/${id}`, {headers})
  }

  public postDescription(description:DescriptionCreation) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(description);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.base_url}/descriptions`, description, {headers})
  }

  public updateDescription(description:DescriptionUpdate) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(description);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.put(`${this.base_url}/descriptions`, description, {headers})
  }

  public deleteDescription(id:string) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(id);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.base_url}/descriptions/${id}`, {headers})
  }

  //StoryPoint
  public getStoryPoint(id): Observable<any>{
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.base_url}/storyPoint/${id}`, {headers})
  }

  public postStoryPoint(storyPoint:StoryPointCreation) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(storyPoint);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.base_url}/storyPoints`, storyPoint, {headers})
  }

  public updateStoryPoint(storyPoint:StoryPointUpdate) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(storyPoint);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.put(`${this.base_url}/storyPoints`, storyPoint, {headers})
  }

  public deleteStoryPoint(id:string) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(id);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.base_url}/storyPoints/${id}`, {headers})
  }

  //Items
  public postEpic(item:BacklogItemDto) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(item);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.base_url}/epics`, item, {headers})
  }

  public postStory(item: BacklogItemDto) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.base_url}/userStories`, item, {headers})
  }
  public postFunc(item: BacklogItemDto) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.base_url}/functionalities`, item , {headers})
  }
  public postTask(item:BacklogItemDto) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.base_url}/tasks`, item, {headers})
  }


  //RAG
  public getRag(id): Observable<any>{
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.base_url}/rag/${id}`, {headers})
  }

  //STATUS
  public getStatus(id): Observable<any>{
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.base_url}/status/${id}`, {headers})
  }
}
