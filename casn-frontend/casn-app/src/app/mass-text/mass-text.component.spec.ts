import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MassTextComponent } from './mass-text.component';

describe('MassTextComponent', () => {
  let component: MassTextComponent;
  let fixture: ComponentFixture<MassTextComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MassTextComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MassTextComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
