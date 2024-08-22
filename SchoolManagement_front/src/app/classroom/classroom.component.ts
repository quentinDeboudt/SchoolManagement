import { Component, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Classroom } from '../../models/classroom.model';
import { ClassroomService } from '../../service/classroom.service';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { GenericTableComponent } from '../generic/generic-table/generic-table.component';
import { MatDialog } from '@angular/material/dialog';
import { GenericModalComponent } from '../generic/generic-modal/generic-modal.component';
import { GenericHeaderComponent } from "../generic/generic-header/generic-header.component";

@Component({
  selector: 'app-classroom',
  standalone: true,
  imports: [
    MatGridListModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    GenericTableComponent,
    GenericHeaderComponent
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
  public personService = inject(ClassroomService);

}
