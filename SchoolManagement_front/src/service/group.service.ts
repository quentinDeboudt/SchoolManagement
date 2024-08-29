import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Group } from '../models/group.model';
import { IEntityService } from '../interface-service/IEntity.service';
import { PagedResult } from '../models/PagedResult.model';

@Injectable({
  providedIn: 'root'
})
export class GroupService implements IEntityService<Group>{
  private apiUrl = 'http://localhost:5034/api/Group';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }
  
  public count(){
    return this.http.get<number>(this.apiUrl+`/count`);
  }

  public getEntities(pageNumber: number, pageSize: number): Observable<Group[]>{
    return this.http.get<Group[]>(`${this.apiUrl}/pagination?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  public createEntity(group: Group): Observable<Group> {
    return this.http.post<Group>(this.apiUrl, group);
  }

  public deleteEntity(group: Group): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete/${group.id}`);
  }

  public editEntity(group: Group): Observable<Group> {
    return this.http.put<Group>(this.apiUrl, group);
  }

  public searchEntities(searchTerm: string, pageIndex: number, pageSize: number): Observable<PagedResult<Group>> {
    return this.http.get<PagedResult<Group>>(`${this.apiUrl}/search?term=${searchTerm}&pageIndex=${pageIndex}&pageSize=${pageSize}`);
  }

}
