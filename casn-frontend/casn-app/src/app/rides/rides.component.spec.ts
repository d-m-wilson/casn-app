import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RidesComponent } from './rides.component';

describe('RidesComponent', () => {
  let component: RidesComponent;
  let fixture: ComponentFixture<RidesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RidesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RidesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
