import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-rides-header',
  templateUrl: './rides-header.component.html',
  styleUrls: ['./rides-header.component.scss']
})
export class RidesHeaderComponent implements OnInit {
  @Input() title: string = 'CASN';

  @Input() showFilters: boolean = true;
  @Output() mapClick = new EventEmitter<void>();
  @Output() calendarClick = new EventEmitter<void>();
  @Output() filtersClick = new EventEmitter<void>();

  @Input() showDates: boolean = true;
  @Input() datesToDisplay: string[];
  @Input() activeDate: string;
  @Output() changeWeekClick = new EventEmitter<string>();
  @Output() dateClick = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void { }

  handleCalendarClick() {
    this.calendarClick.emit();
  }

  handleMapClick() {
    this.mapClick.emit();
  }

  handleFiltersClick() {
    this.filtersClick.emit();
  }

  handleDateClick(date: string) {
    this.dateClick.emit(date);
  }

  handleChangeWeekClick(changeType: string) {
    this.changeWeekClick.emit(changeType);
  }

}
