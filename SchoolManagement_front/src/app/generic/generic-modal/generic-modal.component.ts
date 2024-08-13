import {
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogTitle,
} from '@angular/material/dialog';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { NgFor } from '@angular/common';
import { MatInput } from '@angular/material/input';

interface DialogData {
  entityName: string; // Nom de l'entité (ex: Person, Classroom, Group)
  fields: Array<{ label: string, formControlName: string, type: string }>; // Configuration des champs du formulaire
}

@Component({
  selector: 'app-generic-modal',
  standalone: true,
  imports: [
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
    MatFormField,
    MatLabel,
    MatInput,
    ReactiveFormsModule,
    NgFor
  ],
  templateUrl: './generic-modal.component.html',
  styleUrl: './generic-modal.component.scss'
})
export class GenericModalComponent implements OnInit {
  entityForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<GenericModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData
  ) {
    this.entityForm = this.fb.group({});
  }

  ngOnInit(): void {
    this.data.fields.forEach(field => {
      this.entityForm.addControl(field.formControlName, this.fb.control('', Validators.required));
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    if (this.entityForm.valid) {
      this.dialogRef.close(this.entityForm.value);
    }
  }
}

