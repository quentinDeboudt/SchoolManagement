import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Lesson } from '../models/lesson.model';
import { Subject } from '../models/subject.model';
import { PagedResult } from '../models/PagedResult.model';
import { IEntityService } from '../interface-service/IEntity.service';

@Injectable({
  providedIn: 'root'
})
export class SubjectService implements IEntityService<Subject>{
  private apiUrl = 'http://localhost:5034/api/subject';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  public count(){
    return this.http.get<number>(this.apiUrl+`/count`);
  }

  public getAllEntities(): Observable<Subject[]>{
    return this.http.get<Subject[]>(this.apiUrl);
  }

  public getEntities(pageNumber: number, pageSize: number): Observable<Subject[]>{
    return this.http.get<Subject[]>(`${this.apiUrl}/pagination?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  public createEntity(subject: Subject): Observable<Subject> {
    return this.http.post<Subject>(this.apiUrl, subject);
  }

  public deleteEntity(subject: Subject): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete/${subject.id}`);
  }

  public editEntity(subject: Subject): Observable<Subject> {
    return this.http.put<Subject>(this.apiUrl, subject);
  }

  public searchEntities(searchTerm: string, pageIndex: number, pageSize: number): Observable<PagedResult<Subject>> {
    return this.http.get<PagedResult<Subject>>(`${this.apiUrl}/search?term=${searchTerm}&pageIndex=${pageIndex}&pageSize=${pageSize}`);
  }
}
