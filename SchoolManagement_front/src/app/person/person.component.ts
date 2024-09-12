import { Component, inject, OnInit} from '@angular/core';
import { GenericPageComponent } from '../generic/generic-page/generic-page.component';
import { PersonService } from '../../service/person.service';
import { RoleService } from '../../service/role.service';
import { Role } from '../../models/role.model';

@Component({
  selector: 'app-person',
  standalone: true,
  imports: [
    GenericPageComponent
  ],
  templateUrl: './person.component.html',
  styleUrl: './person.component.scss'
})
export class PersonComponent implements OnInit{
  public personService = inject(PersonService);
  public roles!: Role[];

  public readonly headerData = "Nom, Prénom...";
  public readonly entityName = "personne";
  public readonly icon = "person_add";
  public readonly columns = ['firstName', 'lastName', 'roles'];
  public readonly fieldsModal = [
    { label: 'Prénom', formControlName: 'firstName', type: 'text' },
    { label: 'Nom', formControlName: 'lastName', type: 'text' },
    { label: 'Role', formControlName: 'roles', type: 'entity' }
  ];

  constructor(private roleService: RoleService) { }

  public ngOnInit(): void {
    this.getRoles();
  }

  /**
   * get all roles.
   */
  public getRoles(): void {
    this.roleService.getAllEntities().subscribe(
      (data: Role[]) => {
       
        const simplifiedRoles = data.map(role => ({
          id: role.id,
          name: role.name
        }))
        this.roles = simplifiedRoles
      },
    )
  }
}
