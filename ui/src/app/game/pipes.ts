import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'roundStatusClass'})
export class RoundStatusClassPipe implements PipeTransform {
  transform(value: number): string {
    switch (value) {
      case 1:
        return 'label-default';
      case 2:
        return 'label-success';
      case 3:
        return 'label-warning';
      case 4:
        return 'label-primary';
      case 5:
        return 'label-default';
      case 6:
        return 'label-danger';
      case 7:
        return 'label-default';
      case 8:
        return 'label-info';
    }
    return '';
  }
}
