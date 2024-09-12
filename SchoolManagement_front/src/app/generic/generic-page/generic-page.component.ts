import { Component, inject, OnInit, OnDestroy, Input } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { GenericTableComponent } from '../generic-table/generic-table.component';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { GenericModalComponent } from '../generic-modal/generic-modal.component';
import { PageEvent } from '@angular/material/paginator';
import { takeUntil } from 'rxjs/operators';
import { IEntityService } from '../../../interface-service/IEntity.service';
import { GenericHeaderComponent } from "../generic-header/generic-header.component";

@Component({
  selector: 'app-generic-page',
  standalone: true,
  imports: [
    MatGridListModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    GenericTableComponent,
    GenericHeaderComponent
],
  templateUrl: './generic-page.component.html',
  styleUrl: './generic-page.component.scss'
})
export class GenericPageComponent<T> implements OnInit, OnDestroy {
  public entities$ = new BehaviorSubject<T[]>([]);
  public totalItems$ = new BehaviorSubject<number>(0);
  public isSearching = false;
  public textInput = '';
  public pageEvent = new PageEvent();
  private readonly destroy$ = new Subject<void>();
  readonly dialog = inject(MatDialog);
  public pageCurrently = new PageEvent();


  @Input() data$!: Observable<T[]>;
  @Input() icon!: string;
  @Input() headerData!: string;
  @Input() columns!: string[];
  @Input() fieldsModal!: { label: string; formControlName: string; type: string; }[];
  @Input() entityService!: IEntityService<T>;
  @Input() otherEntity!: any[];
  @Input() entityName!: string;

  constructor( ) {
    this.pageEvent.pageIndex = 0;
    this.pageEvent.pageSize = 5;
  }

  public ngOnInit(): void {
    this.getEntities(this.pageEvent);
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public pageChange(event: PageEvent): void {
    this.isSearching ? this.onSearch(this.textInput, event) : this.getEntities(event);
  }

  public getEntities(pageEvent?: PageEvent): void {
    if(pageEvent){
      this.pageCurrently = pageEvent
    }

    console.log("pageCurrently = ",this.pageCurrently);
    
    this.entityService.getEntities(this.pageCurrently.pageIndex, this.pageCurrently.pageSize)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: entities => this.entities$.next(entities),
        error: err => console.error('Failed to fetch entities', err)
      });

    this.entityService.count()
      .pipe(takeUntil(this.destroy$))
      .subscribe(count => this.totalItems$.next(count));
  }

  public addEntity(): void {
    this.openDialog().afterClosed().pipe(takeUntil(this.destroy$)).subscribe(entity => {
      if (entity) {
        this.entityService.createEntity(entity)
          .pipe(takeUntil(this.destroy$))
          .subscribe(() => this.getEntities(this.pageEvent));
      }
    });
  }

  public deleteEntity(entity: T): void {
    this.entityService.deleteEntity(entity)
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => this.getEntities(this.pageEvent));
  }

  public editEntity(entity: T): void {
    this.openDialog(entity).afterClosed().pipe(takeUntil(this.destroy$)).subscribe(updatedEntity => {
      if (updatedEntity) {
        const newEntity = { ...entity, ...updatedEntity };
        this.entityService.editEntity(newEntity)
          .pipe(takeUntil(this.destroy$))
          .subscribe(() => this.getEntities());
      }
    });
  }

  public onSearch(textInput?: string, pageEvent?: PageEvent): void {
    if (!textInput) {
      this.isSearching = false;
      this.getEntities(this.pageEvent);
      return;
    }

    this.isSearching = true;
    this.textInput = textInput;

    const pageIndex = pageEvent ? pageEvent.pageIndex : this.pageEvent.pageIndex;
    const pageSize = pageEvent ? pageEvent.pageSize : this.pageEvent.pageSize;

    this.entityService.searchEntities(this.textInput, pageIndex, pageSize)
      .pipe(takeUntil(this.destroy$))
      .subscribe(response => {
        this.entities$.next(response.items);
        this.totalItems$.next(response.totalCount);
      });
  }

  private openDialog(entity?: T) {
    return this.dialog.open(GenericModalComponent<T>, {
      data: {
        entityName: this.entityName,
        fields: this.fieldsModal,
        value: entity,
        otherEntity: this.otherEntity
      }
    });
  }
}
