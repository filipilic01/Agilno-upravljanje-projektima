import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TasksService {
  // Kopiran link sa MicroENV platforme
  baseUrl = "https://app.microenv.com/backend/key/4e4185ac56dffb723380ab/rest/api/tasks/"

  // HttpClient je klasa koja nam omogucava da saljemo zahteve iz Angular aplikacije
  constructor(private http: HttpClient) { }

  // Koristimo jednu jednostavnu metodu za izvrsavanje GET metode. Vracamo citav response iz koga dobijamo lako boddy u klasi table-list.component.ts 
  public getTasks() : Observable<any> {
    return this.http.get(this.getUrl())
      .pipe(map((response: Response) => response));
  }

  // Cesto cemo koristiti ovaj URL pa da ga sakrijemo, da mu se ne moze lako pristupiti od spolja.
  private getUrl() {
    return `${this.baseUrl}`;
  }
}
