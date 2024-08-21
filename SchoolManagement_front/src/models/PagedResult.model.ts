import { Observable } from "rxjs";

export interface PagedResult<T> {
    Items: Observable<Array<T>>;
    totalCount: Observable<number>;
}