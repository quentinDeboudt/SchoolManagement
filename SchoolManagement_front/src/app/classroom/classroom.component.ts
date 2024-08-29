import { Component, inject } from '@angular/core';
import { ClassroomService } from '../../service/classroom.service';
import { GenericPageComponent } from '../generic/generic-page/generic-page.component';


@Component({
  selector: 'app-classroom',
  standalone: true,
  imports: [
    GenericPageComponent
],
  templateUrl: './classroom.component.html',
  styleUrl: './classroom.component.scss'
})
export class ClassroomComponent {

  public readonly headerData = "Nom de la Classe";
  public readonly icon = "location_city";
  public readonly fieldsModal = [
    { label: 'Nom de la Classe', formControlName: 'name', type: 'text' }
  ];

  public columns = ['firstName', 'lastName'];
  public classroomService = inject(ClassroomService);
}
