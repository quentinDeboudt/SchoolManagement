import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Lesson } from '../models/lesson.model';

@Injectable({
  providedIn: 'root'
})
export class LessonService {
  private apiUrl = 'http://localhost:5034/api/lesson';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  public getLesson(): Observable<Lesson[]> {
    return this.http.get<Lesson[]>(this.apiUrl);
  }

  createLesson(lesson: Lesson) {
    return this.http.post<Lesson[]>(this.apiUrl, lesson);
  }

}
