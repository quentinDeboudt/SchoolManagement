import { Component, Input, OnInit, ViewChild} from '@angular/core';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { NgForOf } from '@angular/common';

@Component({
  selector: 'app-generic-table',
  standalone: true,
  imports: [
    MatTableModule,
    MatSortModule,
    MatPaginator,
    NgForOf
  ],
  templateUrl: './generic-table.component.html',
  styleUrl: './generic-table.component.scss'
})
export class GenericTableComponent<T> implements OnInit {
  @Input() columns: string[] = [];
  @Input() data: T[] = [];

  displayedColumns: string[] = [];
  dataSource = new MatTableDataSource<T>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngOnInit(): void {
    this.displayedColumns = this.columns;
    this.dataSource.data = this.data;
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
}
