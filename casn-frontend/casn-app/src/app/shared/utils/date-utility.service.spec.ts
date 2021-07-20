import { TestBed } from '@angular/core/testing';

import { DateUtilityService } from './date-utility.service';

describe('DateUtilityService', () => {
  let service: DateUtilityService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DateUtilityService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
