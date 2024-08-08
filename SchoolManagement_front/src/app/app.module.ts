import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PersonComponent } from './person/person.component';
import { routes } from './app.routes';

@NgModule({
  declarations: [ ],
  imports: [
    BrowserModule,
    MatButtonModule,
    AppComponent,
    DashboardComponent,
    PersonComponent, 
    RouterModule.forRoot(routes)
  ],
  bootstrap: []
})
export class AppModule { }
