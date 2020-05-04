import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { DatePipe } from '@angular/common';

export interface DateConfig {
  startDate: string;
  startDateLong: Date;
  endDate: string;
  endDateLong: Date;
  activeDate: string;
  datesToDisplay: any[]; // All dates from startDate to endDate
}

@Injectable({
  providedIn: 'root'
})
export class DateUtilityService {
  private dateConfigSource = new BehaviorSubject(null);
  public readonly dateConfig = this.dateConfigSource.asObservable();
  private initialized: boolean = false;

  constructor( private datePipe: DatePipe ) {}

  setDateRange(date?: Date) {
    if(this.initialized && !date) return;
    this.initialized = true;
    const currentDate = date || new Date();
    const config: any = {};
    config.startDateLong = this.addDays(currentDate, -currentDate.getDay());
    config.startDate = this.datePipe.transform(config.startDateLong, 'yyyy-MM-dd');
    config.endDateLong = this.addDays(config.startDate, 7);
    config.endDate = this.datePipe.transform(config.endDateLong, 'yyyy-MM-dd');
    config.datesToDisplay = this.getDatesForDateRange(config.startDateLong);
    config.activeDate = null;

    this.updateDateConfig(config)
  }

  updateDateConfig(config: DateConfig) {
    console.log("Shared date service was updated:", config);
    this.dateConfigSource.next(config)
  }

  addDays(date, days) {
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
  }

  getDatesForDateRange(startDate: Date) {
    let datesToDisplay = [];
    let currentDate = new Date(startDate);
    for(let i = 0; i < 7; i++) {
      datesToDisplay.push(this.datePipe.transform(currentDate, 'yyyy-MM-dd'));
      currentDate.setDate(currentDate.getDate() + 1);
    }
    return datesToDisplay;
  }
}
