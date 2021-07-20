import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RideFormStatusComponent } from './ride-form-status.component';

describe('RideFormStatusComponent', () => {
  let component: RideFormStatusComponent;
  let fixture: ComponentFixture<RideFormStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RideFormStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RideFormStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
