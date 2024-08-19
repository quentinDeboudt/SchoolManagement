import { Component, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { LessonService } from '../../service/lesson.service';
import { Lesson } from '../../models/lesson.model';
import { MatInputModule } from '@angular/material/input';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { GenericTableComponent } from '../generic/generic-table/generic-table.component';
import { GenericHeaderComponent } from "../generic/generic-header/generic-header.component";
import { MatDialog } from '@angular/material/dialog';
import { GenericModalComponent } from '../generic/generic-modal/generic-modal.component';

@Component({
  selector: 'app-lesson',
  standalone: true,
  imports: [
    MatGridListModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    GenericTableComponent,
    GenericHeaderComponent
],
  templateUrl: './lesson.component.html',
  styleUrl: './lesson.component.scss'
})
export class LessonComponent {
  public lesson$!: Observable<Lesson[]>;
  readonly dialog = inject(MatDialog);
  public headerData = "Nom du Cours";
  public icon = "bag";


  constructor(private lessonService: LessonService) {
  }

  public ngOnInit(): void {
    this.getLesson();
  }

  public getLesson(): void {
    this.lesson$ = this.lessonService.getLesson();
  }
  
  public addEntity(): void {
    const dialogRef = this.dialog.open(GenericModalComponent, {
      data: {
        entityName: 'Lesson',
        fields: [
          { label: 'Numero de la Matière', formControlName: 'subjectId', type: 'text' }
        ]
      }
    });

    dialogRef.afterClosed().subscribe(lesson => {
      if (lesson) {
        this.lessonService.createLesson(lesson).subscribe(
          ()=>{
            this.getLesson();
          }
        )
      }
    });
  }
}
