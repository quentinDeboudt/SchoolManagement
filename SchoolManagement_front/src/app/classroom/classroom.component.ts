import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Classroom } from '../../models/classroom.model';
import { ClassroomService } from '../../service/classroom.service';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { GenericTableComponent } from '../generic/generic-table/generic-table.component';

@Component({
  selector: 'app-classroom',
  standalone: true,
  imports: [
    MatGridListModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    GenericTableComponent
  ],
  templateUrl: './classroom.component.html',
  styleUrl: './classroom.component.scss'
})
export class ClassroomComponent {
  public classroom$!: Observable<Classroom[]>;

  constructor(private classroomService: ClassroomService) {
  }

  public ngOnInit(): void {
    this.getClassroom();
  }

  public getClassroom(): void {
    this.classroom$ = this.classroomService.getClassroom();
  }
}
