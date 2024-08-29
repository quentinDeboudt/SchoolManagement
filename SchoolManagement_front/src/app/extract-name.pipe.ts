import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'extractName',
  standalone: true
})
export class ExtractNamePipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    return null;
  }

}
