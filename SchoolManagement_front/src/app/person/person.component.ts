import { Component, inject, OnInit } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { GenericTableComponent } from '../generic/generic-table/generic-table.component';
import { Person } from '../../models/person.model';
import { PersonService } from '../../service/person.service';
import { Observable } from 'rxjs';
import { GenericModalComponent } from '../generic/generic-modal/generic-modal.component';
import { MatDialog } from '@angular/material/dialog';

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
export class PersonComponent implements OnInit {  
  public persons$!: Observable<Person[]>;
  readonly dialog = inject(MatDialog);

  constructor(private personService: PersonService) {
  }

  public ngOnInit(): void {
    this.getPersons();
  }

  public getPersons(): void {
    this.persons$ = this.personService.getPersons();
  }

  addPerson(): void {
    const dialogRef = this.dialog.open(GenericModalComponent, {
      data: {
        entityName: 'Person',
        fields: [
          { label: 'First Name', formControlName: 'firstName', type: 'text' },
          { label: 'Last Name', formControlName: 'lastName', type: 'text' }
        ]
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log("Result!!!!!!!!!!")
        // this.personService.createPerson(result).subscribe(() => {
        //   this.getPersons(); // Rafraîchir la liste des personnes après l'ajout
        // });
      }
    });
  }
}
