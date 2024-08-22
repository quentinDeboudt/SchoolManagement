import { Component, inject} from '@angular/core';
import { GenericPageComponent } from '../generic/generic-page/generic-page.component';
import { PersonService } from '../../service/person.service';

@Component({
  selector: 'app-person',
  standalone: true,
  imports: [
    GenericPageComponent
  ],
  templateUrl: './person.component.html',
  styleUrl: './person.component.scss'
})
export class PersonComponent {
  public readonly headerData = "Nom, Prénom...";
  public readonly icon = "person_add";
  public readonly fieldsModal = [
    { label: 'Prénom', formControlName: 'firstName', type: 'text' },
    { label: 'Nom', formControlName: 'lastName', type: 'text' },
  ];

  public columns = ['firstName', 'lastName'];
  public personService = inject(PersonService);
}
