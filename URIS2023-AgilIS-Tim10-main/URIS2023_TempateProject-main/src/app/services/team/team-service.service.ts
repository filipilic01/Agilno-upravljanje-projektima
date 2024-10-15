import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Team, TeamMember } from 'app/models/team';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TeamServiceService {

  private base_url = "http://localhost:5255/Tim"

  constructor(private http: HttpClient) { }

  
  public postTeam(team:Team) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(team);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.base_url}`, team, {headers})
  }

  public updateTeam(id:string, team:Team) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(team);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.put(`${this.base_url}/${id}`, team, {headers})
  }

  public deleteTeam(id:string) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(id);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.base_url}/${id}`, {headers})
  }


  public postTeamMember(member:TeamMember) : Observable<any> {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    console.log(member);
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.base_url}/ClanTima`, member, {headers})
  }

  public getTeamsForUsername(username): Observable<any>{
    console.log(username);
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.base_url}/username/${username}`, {headers})
  }

  public getTeamMembersForTeam(id): Observable<any>{
    console.log(id);
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.base_url}/clan/${id}`, {headers})
  }

  public getProjectsForTeam(id): Observable<any>{
    console.log(id);
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.base_url}/projekat/${id}`, {headers})
  }




}
