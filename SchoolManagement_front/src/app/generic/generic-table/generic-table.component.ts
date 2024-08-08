import { Component, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatPaginatorModule } from '@angular/material/paginator';
import { NgForOf } from '@angular/common';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-generic-table',
  standalone: true,
  imports: [
    MatTableModule,
    MatSortModule,
    MatPaginator,
    MatPaginatorModule,
    NgForOf
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
    this.data$.subscribe(data => {
      this.dataSource.data = data || [];
    });
  }

  public ngAfterViewInit(): void {
    this.displayedColumns = this.columns;

    this.subscription = this.data$.subscribe(data => {
      this.dataSource.data = data || [];
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  public ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
