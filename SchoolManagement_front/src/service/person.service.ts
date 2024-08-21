import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Person } from '../models/person.model';
import { PagedResult } from '../models/PagedResult.model';

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  private apiUrl = 'http://localhost:5034/api/Person';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  public count(){
    return this.http.get<number>(this.apiUrl+`/count`);
  }

  public getPersons(pageNumber: number, pageSize: number): Observable<Person[]>{
    return this.http.get<Person[]>(`${this.apiUrl}/pagination?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  public createPerson(person: Person) {
    return this.http.post<Person[]>(this.apiUrl, person);
  }

  public deletePerson(personId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete/${personId}`);
  }

  public editPerson(person: Person) {
    return this.http.put<Person[]>(this.apiUrl, person);
  }

  public searchPersons(searchTerm: string, pageIndex: number, pageSize: number): Observable<PagedResult<Person>> {
    return this.http.get<PagedResult<Person>>(`${this.apiUrl}/search?term=${searchTerm}&pageIndex=${pageIndex}&pageSize=${pageSize}`);
  }

}
