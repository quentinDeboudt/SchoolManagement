import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'objectToText',
  standalone: true,
})
export class ObjectToTextPipe implements PipeTransform {
  transform(value: any, ...args: any[]): string {
    if (Array.isArray(value)) {
      return value.map(item => item.name || item).join(', ');
    }
    return typeof value === 'object' ? JSON.stringify(value) : value;
  }
}
