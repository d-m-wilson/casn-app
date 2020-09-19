import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RidesHeaderComponent } from './rides-header.component';

describe('RidesHeaderComponent', () => {
  let component: RidesHeaderComponent;
  let fixture: ComponentFixture<RidesHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RidesHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RidesHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
