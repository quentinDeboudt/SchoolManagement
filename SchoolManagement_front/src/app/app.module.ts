import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PersonComponent } from './person/person.component';
import { routes } from './app.routes';
import { GenericModalComponent } from './generic/generic-modal/generic-modal.component';

@NgModule({
  declarations: [ ],
  imports: [
    BrowserModule,
    MatButtonModule,
    AppComponent,
    DashboardComponent,
    PersonComponent,
    GenericModalComponent,
    RouterModule.forRoot(routes)
  ],
  bootstrap: []
})
export class AppModule { }
