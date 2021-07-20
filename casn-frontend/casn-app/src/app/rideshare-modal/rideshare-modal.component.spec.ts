import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RideshareModalComponent } from './rideshare-modal.component';

describe('RideshareModalComponent', () => {
  let component: RideshareModalComponent;
  let fixture: ComponentFixture<RideshareModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RideshareModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RideshareModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
