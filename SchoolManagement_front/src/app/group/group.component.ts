import { Group } from '../../models/group.model';
import { Observable } from 'rxjs';
import { GroupService } from '../../service/group.service';
import { Component, inject, OnInit } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { GenericTableComponent } from '../generic/generic-table/generic-table.component';
import { GenericHeaderComponent } from "../generic/generic-header/generic-header.component";
import { MatDialog } from '@angular/material/dialog';
import { GenericModalComponent } from '../generic/generic-modal/generic-modal.component';

@Component({
  selector: 'app-group',
  standalone: true,
  imports: [
    MatGridListModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    GenericTableComponent,
    GenericHeaderComponent
],
  templateUrl: './group.component.html',
  styleUrl: './group.component.scss'
})
export class GroupComponent implements OnInit{
  public group$!: Observable<Group[]>;
  readonly dialog = inject(MatDialog);
  public headerData = "Nom du Groupe";
  public icon = "group_add";

  constructor(private groupService: GroupService) {
  }

  public ngOnInit(): void {
    this.getGroup();
  }

  public getGroup(): void {
    this.group$ = this.groupService.getGroup();
  }

  public addEntity(): void {
    const dialogRef = this.dialog.open(GenericModalComponent, {
      data: {
        entityName: 'Group',
        fields: [
          { label: 'Nom du groupe', formControlName: 'name', type: 'text' },
          { label: 'Numero de la class', formControlName: 'classroomId', type: 'text' },
        ]
      }
    });

    dialogRef.afterClosed().subscribe(group => {
      if (group) {
        this.groupService.createGroup(group).subscribe(
          ()=>{
            this.getGroup();
          }
        )
      }
    });
  }

}
