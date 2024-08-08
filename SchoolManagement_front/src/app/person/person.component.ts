import { Component, OnInit } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { GenericTableComponent } from '../generic/generic-table/generic-table.component';
import { Person } from '../../models/person.model';
import { PersonService } from '../../service/person.service';

@Component({
  selector: 'app-person',
  standalone: true,
  imports: [
    MatGridListModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    GenericTableComponent
  ],
  templateUrl: './person.component.html',
  styleUrl: './person.component.scss'
})
export class PersonComponent implements OnInit{

  persons: Person[] = [];

  constructor(private personService: PersonService) {
    console.log(this.persons);
  }

  ngOnInit(): void {
    this.getPersons();
  }

  getPersons(): void {
    this.personService.getPersons().subscribe(persons => this.persons = persons);
  }
}
