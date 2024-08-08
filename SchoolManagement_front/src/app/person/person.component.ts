import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { GenericTableComponent } from '../generic/generic-table/generic-table.component';
import { Person } from '../../models/person.model';
import { PersonService } from '../../service/person.service';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-person',
  standalone: true,
  imports: [
    MatGridListModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    GenericTableComponent,
    AsyncPipe
  ],
  templateUrl: './person.component.html',
  styleUrl: './person.component.scss'
})
export class PersonComponent implements OnInit {

  public persons$!: Observable<Person[]>;

  constructor(private personService: PersonService) {
  }

  public ngOnInit(): void {
    this.getPersons();
  }

  public getPersons(): void {
    this.persons$ = this.personService.getPersons();
  }
}
