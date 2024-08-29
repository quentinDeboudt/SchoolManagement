import {
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogTitle,
} from '@angular/material/dialog';
import { Component, Inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { JsonPipe, NgFor, NgIf } from '@angular/common';
import { MatInput } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectChange, MatSelectModule } from '@angular/material/select';
import {MatChipsModule} from '@angular/material/chips';
import { MatIcon } from '@angular/material/icon';
import { CdkListbox, CdkOption } from '@angular/cdk/listbox';

interface DialogData {
  entityName: string;
  fields: Field[];
  value: any;
  otherEntity: any[];
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
    NgFor,
    NgIf,
    MatSelectModule,
    MatChipsModule,
    MatIcon,
    CdkListbox, CdkOption
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
   * @returns void
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
   * @returns void
   */
  public onCancel(): void {
    this.dialogRef.close();
  }

  /**
   * Submits the form data.
   * @returns void
   */
  public onSubmit(): void {
    if (this.entityForm.valid) {
        // Combine the existing data with the updated values from the form
        const updatedPerson = { ...this.data.value, ...this.entityForm.value };
        this.dialogRef.close(updatedPerson);
    }
  }

  /**
   * Compare value to checked.
   * @param option 
   * @param selected 
   * @returns boolean
   */
  public isChecked(option: any, selected: any): boolean {
    return option && selected ? option.id === selected.id : option === selected;
  }
}
