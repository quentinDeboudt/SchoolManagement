import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Classroom } from '../models/classroom.model';

@Injectable({
  providedIn: 'root'
})
export class ClassroomService {
  private apiUrl = 'http://localhost:5034/api/Classroom';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  public getClassroom(): Observable<Classroom[]> {
    return this.http.get<Classroom[]>(this.apiUrl);
  }

  public createClassroom(classroom: any) {
    return this.http.post<Classroom[]>(this.apiUrl, classroom);
  }


}
