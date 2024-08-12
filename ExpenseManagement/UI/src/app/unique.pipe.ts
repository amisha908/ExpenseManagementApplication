import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'unique'
})
export class UniquePipe implements PipeTransform {
  transform(value: any[], key: string): any[] {
    if (!value || value.length === 0) {
      return [];
    }

    return value.filter((item, index, self) => 
      index === self.findIndex((t) => (
        t[key] === item[key]
      ))
    );
  }
}
