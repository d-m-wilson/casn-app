import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RideDetailModalComponent } from './ride-detail-modal.component';

describe('RideDetailModalComponent', () => {
  let component: RideDetailModalComponent;
  let fixture: ComponentFixture<RideDetailModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RideDetailModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RideDetailModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
