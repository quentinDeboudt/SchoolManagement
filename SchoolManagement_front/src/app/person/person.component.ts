import { Component, inject, OnInit } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { GenericTableComponent } from '../generic/generic-table/generic-table.component';
import { Person } from '../../models/person.model';
import { PersonService } from '../../service/person.service';
import { Observable } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { GenericHeaderComponent } from "../generic/generic-header/generic-header.component";
import { GenericModalComponent } from '../generic/generic-modal/generic-modal.component';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-person',
  standalone: true,
  imports: [
    MatGridListModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    GenericTableComponent,
    GenericHeaderComponent
],
  templateUrl: './person.component.html',
  styleUrl: './person.component.scss'
})
export class PersonComponent implements OnInit {
  public persons$!: Observable<Person[]>;
  public totalItems$!: Observable<number>;
  
  public pageEvent = new PageEvent();
  readonly dialog = inject(MatDialog);

  public headerData = "Nom, Prénom...";
  public icon = "person_add";
  public fieldsModal = [
    { label: 'Prénom', formControlName: 'firstName', type: 'text' },
    { label: 'Nom', formControlName: 'lastName', type: 'text' },
  ]

  
  constructor(private personService: PersonService) {
    this.pageEvent.pageIndex = 0;
    this.pageEvent.pageSize = 5;
  }

  public ngOnInit(): void {
    this.getPersons(this.pageEvent);
    this.totalItems$ = this.personService.count();
  }

  public openDialog(person?: Person): any {
    const dialogRef = this.dialog.open(GenericModalComponent, {
      data: {
        entityName: 'Person',
        fields: this.fieldsModal,
        value: person
      }
    });

    return dialogRef.afterClosed();
  }

  public getPersons(pageEvent: PageEvent): void {
    this.persons$ = this.personService.getPersons(pageEvent.pageIndex, pageEvent.pageSize);
  }

  public addPerson(): void {

    this.openDialog().subscribe((person: Person) => {
      if (person) {
        this.personService.createPerson(person).subscribe({
          error: (e) => console.error(e),
          complete: () => this.getPersons(this.pageEvent)
        });
      }
    });
  }

  public deletePerson(person: Person) {
   this.personService.deletePerson(person);
  }

  public editPerson(person: Person) {
    console.log("after-update: ", person)

    this.openDialog(person).subscribe((person: Person) => {
      if (person) {
        console.log("update: ", person)
        this.personService.editPerson(person).subscribe({
          error: (e) => console.error(e),
          complete: () => this.getPersons(this.pageEvent)
        });
      }
    });
  }

  
}
