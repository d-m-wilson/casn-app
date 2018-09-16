import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleRideComponent } from './schedule-ride.component';

describe('ScheduleRideComponent', () => {
  let component: ScheduleRideComponent;
  let fixture: ComponentFixture<ScheduleRideComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ScheduleRideComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScheduleRideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
