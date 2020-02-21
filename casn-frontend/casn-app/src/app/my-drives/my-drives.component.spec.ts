import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyDrivesComponent } from './my-drives.component';

describe('MyDrivesComponent', () => {
  let component: MyDrivesComponent;
  let fixture: ComponentFixture<MyDrivesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyDrivesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyDrivesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
