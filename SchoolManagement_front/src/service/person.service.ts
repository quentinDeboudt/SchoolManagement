import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Person } from '../models/person.model';

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  private apiUrl = 'http://localhost:5034/api/Person';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  public getPersons(): Observable<Person[]> {
    return this.http.get<Person[]>(this.apiUrl);
  }

  public createPerson(person: Person) {
    return this.http.post<Person[]>(this.apiUrl, person);
  }

  public deletePerson(person: Person) {
    return this.http.delete<Person[]>(this.apiUrl+`/${person}`);
  }

}
