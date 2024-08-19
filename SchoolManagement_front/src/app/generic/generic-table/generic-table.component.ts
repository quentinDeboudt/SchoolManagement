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
    AsyncPipe
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

  public ngOnInit(): void {
    this.displayedColumns = this.columns;

    this.displayedColumns.forEach(element => {
      this.allColums.push(element)
    });
    this.allColums.push("action");
  }

  public ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  public onPageChange($event: PageEvent) {
    this.pageChange.emit($event);
  }

  public delete(element: T) {
    this.deleteEntity.emit(element);
  }

  public edit(element: T) {
    this.editEntity.emit(element);
  }
}
