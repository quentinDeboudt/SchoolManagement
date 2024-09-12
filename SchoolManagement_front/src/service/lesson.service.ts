import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Lesson } from '../models/lesson.model';
import { PagedResult } from '../models/PagedResult.model';
import { IEntityService } from '../interface-service/IEntity.service';

@Injectable({
  providedIn: 'root'
})
export class LessonService implements IEntityService<Lesson>{
  private apiUrl = 'http://localhost:5034/api/lesson';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  public count(){
    return this.http.get<number>(this.apiUrl+`/count`);
  }

  public getEntities(pageNumber: number, pageSize: number): Observable<Lesson[]>{
    return this.http.get<Lesson[]>(`${this.apiUrl}/pagination?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  public createEntity(lesson: Lesson): Observable<Lesson> {
    return this.http.post<Lesson>(this.apiUrl, lesson);
  }

  public deleteEntity(lesson: Lesson): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete/${lesson.id}`);
  }

  public editEntity(lesson: Lesson): Observable<Lesson> {
    return this.http.put<Lesson>(this.apiUrl, lesson);
  }

  public searchEntities(searchTerm: string, pageIndex: number, pageSize: number): Observable<PagedResult<Lesson>> {
    return this.http.get<PagedResult<Lesson>>(`${this.apiUrl}/search?term=${searchTerm}&pageIndex=${pageIndex}&pageSize=${pageSize}`);
  }

}
