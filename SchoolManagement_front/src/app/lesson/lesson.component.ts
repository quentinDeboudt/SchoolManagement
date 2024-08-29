import { Component, inject } from '@angular/core';
import { LessonService } from '../../service/lesson.service';
import { GenericPageComponent } from '../generic/generic-page/generic-page.component';
import { IEntityService } from '../../interface-service/IEntity.service';
import { Lesson } from '../../models/lesson.model';

@Component({
  selector: 'app-lesson',
  standalone: true,
  imports: [
    GenericPageComponent
  ],
  templateUrl: './lesson.component.html',
  styleUrl: './lesson.component.scss'
})
export class LessonComponent {
  public readonly headerData = "Nom du Cours";
  public readonly entityName = "Cour";
  public readonly icon = "bag";
  public readonly fieldsModal = [
    { label: 'Numero de la Matière', formControlName: 'subjectId', type: 'text' }
  ];

  public columns = ['id', 'subject', 'teachers', 'groups'];
  public lessonService = inject(LessonService);

}
