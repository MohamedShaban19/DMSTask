import { TestBed } from '@angular/core/testing';

import { SecretaryAppointmentService } from './secretary-appointment.service';

describe('SecretaryAppointmentService', () => {
  let service: SecretaryAppointmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SecretaryAppointmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
