import { Component, inject, OnInit } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { GenericTableComponent } from '../generic/generic-table/generic-table.component';
import { Person } from '../../models/person.model';
import { PersonService } from '../../service/person.service';
import { Observable } from 'rxjs';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { GenericHeaderComponent } from "../generic/generic-header/generic-header.component";
import { GenericModalComponent } from '../generic/generic-modal/generic-modal.component';

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
  readonly dialog = inject(MatDialog);
  public headerData = "Nom, Prénom...";
  public icon = "person_add";
  
  constructor(private personService: PersonService) { }

  public ngOnInit(): void {
    this.getPersons();
  }

  public getPersons(): void {
    this.persons$ = this.personService.getPersons();
  }

  public addEntity(): void {
    const dialogRef = this.dialog.open(GenericModalComponent, {
      data: {
        entityName: 'Person',
        fields: [
          { label: 'Prénom', formControlName: 'firstName', type: 'text' },
          { label: 'Nom', formControlName: 'lastName', type: 'text' },
        ]
      }
    });

    dialogRef.afterClosed().subscribe(perosn => {
      if (perosn) {
        console.log("Result: ", perosn)
        this.personService.createPerson(perosn).subscribe(
          ()=>{
            this.getPersons();
          }
        )
        
      }
    });
  }

  public deletePerson(person: Person) {
   this.personService.deletePerson(person);
  }

  public editPerson(person: Person) {
    this.personService.editPerson(person);
  }

  
}
