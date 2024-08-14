import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, output, ViewChild} from '@angular/core';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatPaginatorModule } from '@angular/material/paginator';
import { NgForOf } from '@angular/common';
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
    MatButtonModule
  ],
  templateUrl: './generic-table.component.html',
  styleUrl: './generic-table.component.scss'
})
export class GenericTableComponent<T> implements OnInit, OnDestroy {
  @Input() columns: string[] = [];
  @Input() data$!: Observable<T[]>;
  private subscription!: Subscription;
  public displayedColumns: string[] = [];
  public dataSource = new MatTableDataSource<T>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  @Output('deleteEntity') deleteEntity = new EventEmitter<T>(); 


  public ngOnInit(): void {
    this.displayedColumns = this.columns;
  }

  public ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  public delete(entity: T) {
    this.deleteEntity.emit(entity);
  }

  public Edit(entity: T) {
    throw new Error('Method not implemented.');
  }
}
