import { Component, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatPaginatorModule } from '@angular/material/paginator';
import { NgForOf } from '@angular/common';
import { Observable, Subscription } from 'rxjs';
import { ObjectToTextPipe } from '../../pipe/object-to-text-pipe.pipe';

@Component({
  selector: 'app-generic-table',
  standalone: true,
  imports: [
    MatTableModule,
    MatSortModule,
    MatPaginator,
    MatPaginatorModule,
    NgForOf,
    ObjectToTextPipe
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


  public ngOnInit(): void {
    this.displayedColumns = this.columns;
  }

  public ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
