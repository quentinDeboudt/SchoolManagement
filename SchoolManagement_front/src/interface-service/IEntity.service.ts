import { InjectionToken } from '@angular/core';
import { Observable } from 'rxjs';

export interface IEntityService<T> {
  getEntities(pageIndex: number, pageSize: number): Observable<T[]>;
  count(): Observable<number>;
  createEntity(entity: T): Observable<T>;
  editEntity(entity: T): Observable<T>;
  deleteEntity(entity: T): Observable<void>;
  searchEntities(textInput: string, pageIndex: number, pageSize: number): Observable<{ items: T[], totalCount: number }>;
}

// Create a injection token:
export const ENTITY_SERVICE_TOKEN = new InjectionToken<IEntityService<any>>('EntityService');
