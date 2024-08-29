import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'extractName',
  standalone: true
})
export class ExtractNamePipe implements PipeTransform {

  transform(value: any): any {
    // Vérifier si la valeur est un objet et non null
    if (value && typeof value === 'object') {
      // Si c'est un tableau d'objets, transformer chaque objet
      if (Array.isArray(value)) {
        return value.map(item => this.extractName(item)).join(', ');
      }
      // Sinon, transformer l'objet
      return this.extractName(value);
    }

    // Si la valeur est déjà une chaîne de caractères ou autre type primitif
    return value;
  }

  private extractName(obj: any): string {
    // Vérifier si l'objet a une propriété "name" et la retourner
    if ('name' in obj) {
      return obj.name;
    }
    // Si l'objet n'a pas de propriété "name", retourner "[Object]" ou autre indication
    return '[Object]';
  }

}