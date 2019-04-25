import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CancelDriveModalComponent } from './cancel-drive-modal.component';

describe('CancelDriveModalComponent', () => {
  let component: CancelDriveModalComponent;
  let fixture: ComponentFixture<CancelDriveModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CancelDriveModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CancelDriveModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
