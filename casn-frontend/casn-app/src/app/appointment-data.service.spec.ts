import { TestBed } from '@angular/core/testing';

import { AppointmentDataService } from './appointment-data.service';

describe('AppointmentDataService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AppointmentDataService = TestBed.get(AppointmentDataService);
    expect(service).toBeTruthy();
  });
});
