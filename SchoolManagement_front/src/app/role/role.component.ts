import { RoleService } from '../../service/role.service';
import { Role } from '../../models/role.model';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { GenericTableComponent } from '../generic/generic-table/generic-table.component';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-role',
  standalone: true,
  imports: [
    GenericTableComponent,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatGridListModule
  ],
  templateUrl: './role.component.html',
  styleUrl: './role.component.scss'
})
export class RoleComponent implements OnInit, OnDestroy {

  public roles$!: Observable<Role[]>;
  private subscription: Subscription = new Subscription();


  constructor(private roleService: RoleService) {
  }

  public ngOnInit(): void {
    this.getRole();
  }

  public ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  public getRole(): void {
    this.roles$ = this.roleService.getRoles();
  }
}
