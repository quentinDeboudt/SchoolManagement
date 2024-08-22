import { GroupService } from '../../service/group.service';
import { Component, inject } from '@angular/core';
import { GenericHeaderComponent } from "../generic/generic-header/generic-header.component";

@Component({
  selector: 'app-group',
  standalone: true,
  imports: [
    GenericHeaderComponent
],
  templateUrl: './group.component.html',
  styleUrl: './group.component.scss'
})
export class GroupComponent{

  public readonly headerData = "Nom du Groupe";
  public readonly icon = "group_add";
  public readonly fieldsModal = [
    { label: 'Nom du groupe', formControlName: 'name', type: 'text' },
    { label: 'Numero de la class', formControlName: 'classroomId', type: 'text' }
  ];

  public columns = ['firstName', 'lastName'];
  public personService = inject(GroupService);

}
