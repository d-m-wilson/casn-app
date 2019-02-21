import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CallersComponent } from './callers.component';

describe('CallersComponent', () => {
  let component: CallersComponent;
  let fixture: ComponentFixture<CallersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CallersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CallersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
