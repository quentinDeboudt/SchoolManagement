import { Component, inject } from '@angular/core';
import { LessonService } from '../../service/lesson.service';
import { GenericPageComponent } from '../generic/generic-page/generic-page.component';
import { IEntityService } from '../../interface-service/IEntity.service';
import { Lesson } from '../../models/lesson.model';
import { PersonService } from '../../service/person.service';
import { GroupService } from '../../service/group.service';
import { Person } from '../../models/person.model';
import { Group } from '../../models/group.model';
import { SubjectService } from '../../service/subject.service';
import { Subject } from '../../models/subject.model';

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
  public lessonService = inject(LessonService);
  public Teachers!: Person[];
  public Groups!: Group[];
  public Subject!: Subject[];


  public readonly headerData = "Nom du Cours";
  public readonly entityName = "Cour";

  public readonly icon = "bag";
  public columns = ['id', 'subject', 'teachers', 'groups'];
  public readonly fieldsModal = [
    { label: 'subject', formControlName: 'matière', type: 'text' },
    { label: 'Teacher', formControlName: 'Nom du prof', type: 'entity' },
    { label: 'Groups', formControlName: 'Nom Group', type: 'entity' },
  ];

  constructor(
    private personService: PersonService,
    private groupService: GroupService,
    private subjectService: SubjectService
  ){ }

  public ngOnInit(): void {
    this.getTeachers();
    this.getGroups();
  }

  /**
   * get all teachers.
   */
  public getTeachers(): void {
    this.personService.getAllEntities().subscribe(
      (data: Person[]) => {
       
        const simplified = data.map(teacher => ({
          id: teacher.id,
          firstName: teacher.firstName,
          lastName: teacher.lastName,
          roles: teacher.roles,
        }))
        this.Teachers = simplified
      },
    )
  }

   /**
   * get all groups.
   */
   public getGroups(): void {
    this.groupService.getAllEntities().subscribe(
      (data: Group[]) => {
       
        const simplified = data.map(group => ({
          id: group.id,
          name: group.name,
          classroomId: group.classroomId,
          classroom: group.classroom,
          students: group.students,
          lessons: group.lessons,
        }))
        this.Groups = simplified
      },
    )
  }

   /**
   * get all subject.
   */
   public getSubject(): void {
    this.subjectService.getAllEntities().subscribe(
      (data: Subject[]) => {
       
        const simplified = data.map(Subject => ({
          id: Subject.id,
          name: Subject.name
        }))
        this.Subject = simplified
      },
    )
  }
}
