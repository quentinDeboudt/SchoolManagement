import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatIcon } from '@angular/material/icon';

import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { Person } from '../../models/person.model';
import { PersonService } from '../../service/person.service';
import { map, Subject, takeUntil } from 'rxjs';
import { ClassroomService } from '../../service/classroom.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    MatGridListModule,
    MatCardModule,
    MatIcon,
    MatProgressSpinnerModule,
    MatProgressBarModule
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit, OnDestroy {

  private readonly destroy$ = new Subject<void>();
  public totalStudent!: number;
  public totalClassroom!: number;
  public totalTeachers!: number;


  /**
   *
   */
  constructor(
    private personService: PersonService,
    private classroomService: ClassroomService
  ) { }

  ngOnInit(): void {
    this.getTotalStudent();
    this.getTotalClassroom();
    this.getTotalTeachers();
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }


  public getTotalStudent(): void {    
    this.personService.getAllEntities()
      .pipe(takeUntil(this.destroy$),
      map(entities => entities.filter((entity: any) => {
        return entity.roles.some((role: any) => role.name === 'Student');
      })),
      map(students => students.length)
      )
      .subscribe({
        next: total => this.totalStudent = total,
      });
  }

  public getTotalClassroom(): void {    
    this.classroomService.getAllEntities()
      .pipe(takeUntil(this.destroy$),
      map(students => students.length)
      )
      .subscribe({
        next: total => this.totalClassroom = total,
        error: err => console.error('Failed to fetch entities', err)
      });
  }

  public getTotalTeachers(): void {    
    this.personService.getAllEntities()
      .pipe(takeUntil(this.destroy$),
      map(entities => entities.filter((entity: any) => {
        return entity.roles.some((role: any) => role.name === 'Teacher');
      })),
      map(students => students.length)
      )
      .subscribe({
        next: total => this.totalTeachers = total,
        error: err => console.error('Failed to fetch entities', err)
      });
  }

}
