import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { LessonService } from '../../service/lesson.service';
import { Lesson } from '../../models/lesson.model';
import { MatInputModule } from '@angular/material/input';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { GenericTableComponent } from '../generic/generic-table/generic-table.component';

@Component({
  selector: 'app-lesson',
  standalone: true,
  imports: [
    MatGridListModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    GenericTableComponent
  ],
  templateUrl: './lesson.component.html',
  styleUrl: './lesson.component.scss'
})
export class LessonComponent {
  
  public lesson$!: Observable<Lesson[]>;

  constructor(private lessonService: LessonService) {
  }

  public ngOnInit(): void {
    this.getLesson();
  }

  public getLesson(): void {
    this.lesson$ = this.lessonService.getLesson();
  }
}
