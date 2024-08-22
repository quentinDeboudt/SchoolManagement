import { Component, inject, OnInit } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { GenericTableComponent } from '../generic/generic-table/generic-table.component';
import { Person } from '../../models/person.model';
import { PersonService } from '../../service/person.service';
import { BehaviorSubject } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { GenericHeaderComponent } from "../generic/generic-header/generic-header.component";
import { GenericModalComponent } from '../generic/generic-modal/generic-modal.component';
import { PageEvent } from '@angular/material/paginator';
import { AsyncPipe, NgFor, NgForOf } from '@angular/common';

@Component({
  selector: 'app-person',
  standalone: true,
  imports: [
    MatGridListModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    GenericTableComponent,
    GenericHeaderComponent,
    NgForOf, NgFor, AsyncPipe
],
  templateUrl: './person.component.html',
  styleUrl: './person.component.scss'
})
export class PersonComponent implements OnInit {
  public persons$ = new BehaviorSubject<Person[]>([]);
  public totalItems$ = new BehaviorSubject<number>(0);
  public isSearching = false;
  public textInput!: string;
  
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
  }

  public pageChange($event: PageEvent) {
    if(this.isSearching){
      this.onSearch('', $event)
    }else{
      this.getPersons($event)
    }
  }

  public getPersons(pageEvent: PageEvent): void {
    this.personService.getPersons(pageEvent.pageIndex, pageEvent.pageSize).subscribe(persons =>{
      this.persons$.next(persons);
      console.log('getPersons', persons); 

    });

    this.personService.count().subscribe(number =>{
      this.totalItems$.next(number);
    });;
  }

  public addPerson(): void {
    const dialogRef = this.dialog.open(GenericModalComponent<Person>, {
      data: {
        entityName: 'Person',
        fields: this.fieldsModal,
      }
    });
    dialogRef.afterClosed().subscribe((person: Person) => {
      if (person) {
        this.personService.createPerson(person).subscribe(response => {
          console.log(response);
          this.getPersons(this.pageEvent);
        });
      }
    });
  }

  public deletePerson(person: Person) {
    this.personService.deletePerson(person.id).subscribe(response => {
      console.log(response);
      this.getPersons(this.pageEvent)
    })
  }

  public editPerson(person: Person) {
    const dialogRef = this.dialog.open(GenericModalComponent<Person>, {
      data: {
        entityName: 'Person',
        fields: this.fieldsModal,
        value: person
      }
    });
    
    dialogRef.afterClosed().subscribe((newPerson: Person) => {
      if (newPerson) {
        const updatedPerson = { ...person, ...newPerson };
        
        this.personService.editPerson(updatedPerson).subscribe({
            error: (e) => console.error(e),
            complete: () => this.getPersons(this.pageEvent)
        });
      }  
    });
  }

  public onSearch(textInput?: string, pageEvent?: PageEvent): void {

    if(textInput == '' || !textInput){
      this.isSearching = false;
      this.textInput = '';
      this.getPersons(this.pageEvent);
    }

    if(textInput != '' && textInput){
      this.isSearching = true;
      this.textInput = textInput;
    }

    if(this.isSearching){
      const pageIndex = pageEvent ? pageEvent.pageIndex : this.pageEvent.pageIndex;
      const pageSize = pageEvent ? pageEvent.pageSize : this.pageEvent.pageSize;

      this.personService.searchPersons(this.textInput, pageIndex, pageSize).subscribe(response => {
        this.persons$.next(response.items);
        console.log('Is searching', response.items, response.totalCount); 
        this.totalItems$.next(response.totalCount);

      });
    }   
  }
}
