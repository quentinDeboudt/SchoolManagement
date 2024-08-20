import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild} from '@angular/core';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatPaginatorModule } from '@angular/material/paginator';
import { AsyncPipe, NgForOf } from '@angular/common';
import { Observable, Subscription } from 'rxjs';
import { ObjectToTextPipe } from '../../pipe/object-to-text-pipe.pipe';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-generic-table',
  standalone: true,
  imports: [
    MatTableModule,
    MatSortModule,
    MatPaginator,
    MatPaginatorModule,
    NgForOf,
    ObjectToTextPipe,
    MatIconModule,
    MatButtonModule,
    AsyncPipe,
    MatSortModule
  ],
  templateUrl: './generic-table.component.html',
  styleUrl: './generic-table.component.scss'
})
export class GenericTableComponent<T> implements OnInit, OnDestroy {
  @Input() columns: string[] = [];
  @Input() data$!: Observable<T[]>;
  @Input() totalItems$!: Observable<number>;
  @Output('deleteEntity') deleteEntity = new EventEmitter<T>();
  @Output('editEntity') editEntity = new EventEmitter<T>();
  @Output('pageChange') pageChange = new EventEmitter<PageEvent>();

  private subscription!: Subscription;
  public displayedColumns: string[] = [];
  public allColums: string[] = [];
  public dataSource = new MatTableDataSource<T>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  /**
   * Angular lifecycle hook that is called after the component's view has been initialized.
   * Initializes the displayed columns and adds an "action" column to the list of all columns.
   */
  public ngOnInit(): void {
    this.displayedColumns = this.columns;

    this.displayedColumns.forEach(element => {
      this.allColums.push(element)
    });
    this.allColums.push("action");
  }

  /**
   * Angular lifecycle hook that is called just before the component is destroyed.
   * Unsubscribes from the subscription to prevent memory leaks.
   */
  public ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  /**
   * Event handler that is triggered when the pagination page is changed.
   * Emits the new page event to notify parent components or services.
   *
   * @param {PageEvent} $event - The pagination event containing the new page index and page size.
   */
  public onPageChange($event: PageEvent): void {
    this.pageChange.emit($event);
  }

  /**
   * Triggers the deletion of a specified entity.
   * Emits the entity to be deleted so that parent components or services can handle the deletion process.
   *
   * @param {T} element - The entity to be deleted.
   */
  public delete(element: T): void {
    this.deleteEntity.emit(element);
  }

  /**
   * Triggers the edit action for a specified entity.
   * Emits the entity to be edited so that parent components or services can handle the editing process.
   *
   * @param {T} element - The entity to be edited.
   */
  public edit(element: T): void {
    this.editEntity.emit(element);
  }
  
}
