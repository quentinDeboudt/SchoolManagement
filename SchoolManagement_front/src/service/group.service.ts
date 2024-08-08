import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Group } from '../models/group.model';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  private apiUrl = 'http://localhost:5034/api/Group';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  public getGroup(): Observable<Group[]> {
    return this.http.get<Group[]>(this.apiUrl);
  }

}
