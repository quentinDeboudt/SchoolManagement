import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Classroom } from '../models/classroom.model';
import { PagedResult } from '../models/PagedResult.model';
import { IEntityService } from '../interface-service/IEntity.service';

@Injectable({
  providedIn: 'root'
})
export class ClassroomService implements IEntityService<Classroom>{
  private apiUrl = 'http://localhost:5034/api/Classroom';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  public count(){
    return this.http.get<number>(this.apiUrl+`/count`);
  }

  public getAllEntities(): Observable<Classroom[]>{
    return this.http.get<Classroom[]>(this.apiUrl);
  }

  public getEntities(pageNumber: number, pageSize: number): Observable<Classroom[]>{
    return this.http.get<Classroom[]>(`${this.apiUrl}/pagination?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  public createEntity(classroom: Classroom): Observable<Classroom> {
    return this.http.post<Classroom>(this.apiUrl, classroom);
  }

  public deleteEntity(classroom: Classroom): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete/${classroom.id}`);
  }

  public editEntity(classroom: Classroom): Observable<Classroom> {
    return this.http.put<Classroom>(this.apiUrl, classroom);
  }

  public searchEntities(searchTerm: string, pageIndex: number, pageSize: number): Observable<PagedResult<Classroom>> {
    return this.http.get<PagedResult<Classroom>>(`${this.apiUrl}/search?term=${searchTerm}&pageIndex=${pageIndex}&pageSize=${pageSize}`);
  }


}
