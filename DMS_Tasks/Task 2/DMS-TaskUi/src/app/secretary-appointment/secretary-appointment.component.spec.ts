import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SecretaryAppointmentComponent } from './secretary-appointment.component';

describe('SecretaryAppointmentComponent', () => {
  let component: SecretaryAppointmentComponent;
  let fixture: ComponentFixture<SecretaryAppointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SecretaryAppointmentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SecretaryAppointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
