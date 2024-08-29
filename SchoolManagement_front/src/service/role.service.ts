import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Role } from '../models/role.model';
import { PagedResult } from '../models/PagedResult.model';
import { IEntityService } from '../interface-service/IEntity.service';

@Injectable({
  providedIn: 'root'
})
export class RoleService implements IEntityService<Role>{
  private apiUrl = 'http://localhost:5034/api/Role';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  public count(){
    return this.http.get<number>(this.apiUrl+`/count`);
  }
  public getAllEntities(): Observable<Role[]>{
    return this.http.get<Role[]>(`${this.apiUrl}`);
  }

  public getEntities(pageNumber: number, pageSize: number): Observable<Role[]>{
    return this.http.get<Role[]>(`${this.apiUrl}/pagination?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  public createEntity(role: Role): Observable<Role> {
    return this.http.post<Role>(this.apiUrl, role);
  }

  public deleteEntity(role: Role): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete/${role.id}`);
  }

  public editEntity(role: Role): Observable<Role> {
    return this.http.put<Role>(this.apiUrl, role);
  }

  public searchEntities(searchTerm: string, pageIndex: number, pageSize: number): Observable<PagedResult<Role>> {
    return this.http.get<PagedResult<Role>>(`${this.apiUrl}/search?term=${searchTerm}&pageIndex=${pageIndex}&pageSize=${pageSize}`);
  }

}
