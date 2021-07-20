import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'ridecta'
})
export class RideCTAPipe implements PipeTransform {
  transform(value): string {
    const numValue = parseInt(value);
    switch(numValue) {
      case 0: return "Apply to Drive";
      case 1: return "Pending";
      case 2: return "Approved";
      case 3: return "Canceled";
      case 4: return "Rideshare";
      default: return "See Details";
    }
  }
}
