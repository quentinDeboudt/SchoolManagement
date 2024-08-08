import { Group } from '../../models/group.model';
import { Observable } from 'rxjs';
import { GroupService } from '../../service/group.service';
import { Component, OnInit } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { GenericTableComponent } from '../generic/generic-table/generic-table.component';

@Component({
  selector: 'app-group',
  standalone: true,
  imports: [
    MatGridListModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    GenericTableComponent
  ],
  templateUrl: './group.component.html',
  styleUrl: './group.component.scss'
})
export class GroupComponent implements OnInit{

  public group$!: Observable<Group[]>;

  constructor(private groupService: GroupService) {
  }

  public ngOnInit(): void {
    this.getGroup();
  }

  public getGroup(): void {
    this.group$ = this.groupService.getGroup();
  }
}
