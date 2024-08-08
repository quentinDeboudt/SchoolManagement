import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PersonComponent } from './person/person.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RoleComponent } from './role/role.component';
import { GroupComponent } from './group/group.component';
import { ClassroomComponent } from './classroom/classroom.component';


export const routes: Routes = [
    { path: '', component: DashboardComponent },
    { path: 'dashboard', component: DashboardComponent },
    { path: 'person', component: PersonComponent },
    { path: 'role', component: RoleComponent },
    { path: 'group', component: GroupComponent },
    { path: 'classroom', component: ClassroomComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }