import { Component, inject } from '@angular/core';
import { LessonService } from '../../service/lesson.service';
import { GenericPageComponent } from '../generic/generic-page/generic-page.component';

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
  public readonly icon = "bag";
  public readonly fieldsModal = [
    { label: 'Numero de la Matière', formControlName: 'subjectId', type: 'text' }
  ];

  public columns = ['firstName', 'lastName'];
  public personService = inject(LessonService);

}
