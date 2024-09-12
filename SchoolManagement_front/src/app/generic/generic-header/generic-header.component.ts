import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-generic-header',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatLabel,
    MatButtonModule,
    MatInputModule,
    MatIconModule,
    FormsModule
  ],
  templateUrl: './generic-header.component.html',
  styleUrl: './generic-header.component.scss'
})
export class GenericHeaderComponent {
  readonly dialog = inject(MatDialog);
  @Input() headerData!: string;
  @Input() icon!: string;
  @Output() openedModal = new EventEmitter();
  @Output() search = new EventEmitter<string>();
  @Input() entityName!: string;

  public searchTerm: string = '';

  /**
   * Emits an event when the add button is clicked.
   */
  addEntity() {
    this.openedModal.emit();
  }

  /**
   * Emits the search term whenever the user types in the search input.
   */
  public onSearch(): void {
    if(this.searchTerm){
      this.search.emit(this.searchTerm);
    }else{
      this.search.emit();
    }
  }
}
