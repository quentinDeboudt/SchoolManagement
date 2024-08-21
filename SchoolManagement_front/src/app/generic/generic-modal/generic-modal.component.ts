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
import { MatButtonModule } from '@angular/material/button';

interface DialogData {
  entityName: string;
  fields: Field[];
  value: any;
}

export interface Field {
  label: string;
  formControlName: string;
  type: string;
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
    MatButtonModule,
    ReactiveFormsModule,
    NgFor
  ],
  templateUrl: './generic-modal.component.html',
  styleUrl: './generic-modal.component.scss'
})
export class GenericModalComponent<T> implements OnInit {
  public entityForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<GenericModalComponent<T>>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData
  ) {
    this.entityForm = this.fb.group({});
  }

  /**
   * This method sets up the form group by iterating over the fields provided in the data.
   * If a value is provided for a field, it is set as the initial,
   * otherwise, the control is initialized with an empty string.
   */
  public ngOnInit(): void {
    this.data.fields.forEach(field => {
      const fieldValue = this.data.value ? this.data.value[field.formControlName] : '';
      this.entityForm.addControl(
        field.formControlName,
        this.fb.control(fieldValue, Validators.required)
      );
    });
  }

  /**
   * Closes the dialog without saving any changes.
   */
  public onCancel(): void {
    this.dialogRef.close();
  }

  /**
   * Submits the form data.
   */
  // public onSubmit(): void {
  //   if (this.entityForm.valid) {
  //     this.dialogRef.close(this.entityForm.value);
  //   }
  // }

  public onSubmit(): void {
    console.log('onSubmit called with form values:', this.entityForm.value);
    if (this.entityForm.valid) {
        // Combine the existing data with the updated values from the form
        const updatedPerson = { ...this.data.value, ...this.entityForm.value };
        this.dialogRef.close(updatedPerson);
    }
}
}
