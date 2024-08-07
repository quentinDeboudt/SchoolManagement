import { Routes } from '@angular/router';
import { NavigationComponent } from './navigation/navigation.component';
import { PersonComponent } from './person/person.component';
import { DashboardComponent } from './dashboard/dashboard.component';

export const routes: Routes = [
    { path: 'dashboard', component: DashboardComponent },
    { path: 'person', component: PersonComponent },
];