import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PersonComponent } from './person/person.component';
import { routes } from './app.routes';
import { GenericModalComponent } from './generic/generic-modal/generic-modal.component';
import { ENTITY_SERVICE_TOKEN } from '../interface-service/IEntity.service';
import { PersonService } from '../service/person.service';
import { ClassroomService } from '../service/classroom.service';
import { GroupService } from '../service/group.service';
import { LessonService } from '../service/lesson.service';
import { RoleService } from '../service/role.service';
import { SubjectService } from '../service/subject.service';

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
  bootstrap: [],
  providers: [
    { provide: ENTITY_SERVICE_TOKEN, useClass: PersonService },
    { provide: ENTITY_SERVICE_TOKEN, useClass: ClassroomService },
    { provide: ENTITY_SERVICE_TOKEN, useClass: SubjectService },
    { provide: ENTITY_SERVICE_TOKEN, useClass: GroupService },
    { provide: ENTITY_SERVICE_TOKEN, useClass: LessonService },
    { provide: ENTITY_SERVICE_TOKEN, useClass: RoleService },

  ]
})

export class AppModule { }
