import { RoleService } from '../../service/role.service';
import { Component, inject} from '@angular/core';
import { GenericPageComponent } from '../generic/generic-page/generic-page.component';
import { IEntityService } from '../../interface-service/IEntity.service';
import { Role } from '../../models/role.model';

@Component({
  selector: 'app-role',
  standalone: true,
  imports: [
    GenericPageComponent
  ],
  templateUrl: './role.component.html',
  styleUrl: './role.component.scss'
})
export class RoleComponent{
  public readonly headerData = "Nom du Rôle";
  public readonly icon = "local_offer";
  public readonly fieldsModal = [
    { label: 'Nom du Rôle', formControlName: 'name', type: 'text' }
  ];

  public columns = ['id', 'name'];
  public roleService = inject(RoleService);

}
