import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'withSign'})
export class WithSignPipe implements PipeTransform {
  transform(value: number): string {
    return value > 0 ? `+${value}` : `${value}`;
  }
}

