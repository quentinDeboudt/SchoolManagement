import { Component, inject } from '@angular/core';
import { GenericPageComponent } from "../generic/generic-page/generic-page.component";
import { GroupService } from '../../service/group.service';
import { Classroom } from '../../models/classroom.model';
import { ClassroomService } from '../../service/classroom.service';

@Component({
  selector: 'app-group',
  standalone: true,
  imports: [
    GenericPageComponent
],
  templateUrl: './group.component.html',
  styleUrl: './group.component.scss'
})
export class GroupComponent{
  public groupService = inject(GroupService);
  public classroom!: Classroom[];

  public readonly headerData = "Nom du Groupe";
  public readonly icon = "group_add";
  public readonly columns = ['name','classroom'];
  public readonly fieldsModal = [
    { label: 'Nom du groupe', formControlName: 'name', type: 'text' },
    { label: 'Nom de la class', formControlName: 'classroom', type: 'entity' }
  ];

  constructor(private classroomService: ClassroomService) { }

  public ngOnInit(): void {
    this.getClassroom();
  }

  /**
   * get all Classroom.
   */
  public getClassroom(): void {
    this.classroomService.getAllEntities().subscribe(
      (data: Classroom[]) => {
       
        const simplifiedClassroom = data.map(classroom => ({
          id: classroom.id,
          name: classroom.name
        }))

        console.log(simplifiedClassroom)
        this.classroom = simplifiedClassroom
      },
    )
  }

}
