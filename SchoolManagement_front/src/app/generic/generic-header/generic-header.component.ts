import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { GenericModalComponent } from '../generic-modal/generic-modal.component';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-generic-header',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatLabel,
    MatButtonModule,
    MatInputModule,
    MatIconModule
  ],
  templateUrl: './generic-header.component.html',
  styleUrl: './generic-header.component.scss'
})
export class GenericHeaderComponent {
  readonly dialog = inject(MatDialog);
  @Input() headerData!: string;
  @Input() icon!: string;
  @Output() openedModal = new EventEmitter();

  addEntity() {
    this.openedModal.emit();
  }
}
