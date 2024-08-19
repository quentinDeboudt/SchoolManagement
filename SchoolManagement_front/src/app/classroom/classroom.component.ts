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
  public classroom$!: Observable<Classroom[]>;
  readonly dialog = inject(MatDialog);
  public headerData = "Nom de la Classe";
  public icon = "location_city";

  constructor(private classroomService: ClassroomService) {
  }

  public ngOnInit(): void {
    this.getClassroom();
  }

  public getClassroom(): void {
    this.classroom$ = this.classroomService.getClassroom();
  }
  
  public addEntity(): void {
    const dialogRef = this.dialog.open(GenericModalComponent, {
      data: {
        entityName: 'Classroom',
        fields: [
          { label: 'Nom de la Classe', formControlName: 'name', type: 'text' }
        ]
      }
    });

    dialogRef.afterClosed().subscribe(classroom => {
      if (classroom) {
        this.classroomService.createClassroom(classroom).subscribe(
          ()=>{
            this.getClassroom();
          }
        )
      }
    });
  }
}
