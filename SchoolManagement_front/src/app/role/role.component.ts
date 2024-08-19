import { RoleService } from '../../service/role.service';
import { Role } from '../../models/role.model';
import { Component, inject, OnInit } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { GenericTableComponent } from '../generic/generic-table/generic-table.component';
import { Observable } from 'rxjs';
import { GenericHeaderComponent } from "../generic/generic-header/generic-header.component";
import { MatDialog } from '@angular/material/dialog';
import { GenericModalComponent } from '../generic/generic-modal/generic-modal.component';

@Component({
  selector: 'app-role',
  standalone: true,
  imports: [
    GenericTableComponent,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatGridListModule,
    GenericHeaderComponent
],
  templateUrl: './role.component.html',
  styleUrl: './role.component.scss'
})
export class RoleComponent implements OnInit {
  public roles$!: Observable<Role[]>;
  readonly dialog = inject(MatDialog);
  public headerData = "Nom du Rôle";
  public icon = "local_offer";

  constructor(private rolesService: RoleService) {
  }

  public ngOnInit(): void {
    this.getRole();
  }

  public getRole(): void {
    this.roles$ = this.rolesService.getRoles();
  }
  
  public addEntity(): void {
    const dialogRef = this.dialog.open(GenericModalComponent, {
      data: {
        entityName: 'Rôles',
        fields: [
          { label: 'Nom du Rôle', formControlName: 'name', type: 'text' }
        ]
      }
    });

    dialogRef.afterClosed().subscribe(lesson => {
      if (lesson) {
        this.rolesService.createRole(lesson).subscribe(
          ()=>{
            this.getRole();
          }
        )
      }
    });
  }
}
