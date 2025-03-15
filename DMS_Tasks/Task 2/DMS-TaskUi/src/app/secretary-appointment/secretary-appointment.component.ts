import { Component, OnInit } from '@angular/core';
import { SecretaryAppointmentService } from './secretary-appointment.service';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatIconModule } from '@angular/material/icon';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatNativeDateModule } from '@angular/material/core';

@Component({
  selector: 'secretary-appointment',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatTableModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
  ],
  templateUrl: './secretary-appointment.component.html',
  styleUrl: './secretary-appointment.component.css',
})
export class SecretaryAppointmentComponent implements OnInit {
  appointmentForm: FormGroup;
  doctors: any[] = [];
  clinics: any[] = [];
  availableSlots: string[] = [];
  appointments: any[] = [];

  displayedColumns: string[] = [
    'patient',
    'clinic',
    'doctor',
    'date',
    'time',
    'actions',
  ];

  constructor(
    private fb: FormBuilder,
    private appointmentService: SecretaryAppointmentService
  ) {
    this.appointmentForm = this.fb.group({
      patientName: [''],
      birthDate: [''],
      phone: [''],
      docId: [''],
      clinicId: [''],
      appointmentDate: [''],
      appointmentTime: [''],
    });
  }

  ngOnInit(): void {
    this.loadClincs();
    this.loadAppointments();
  }

  loadClincs(): void {
    this.appointmentService.getClincs().subscribe(
      (clinics) => {
        this.clinics = clinics;
      },
      (error) => {
        console.error('Error fetching clinics', error);
      }
    );
  }
  loadDoctors(clinicId: number): void {
    this.appointmentService.getDoctors(clinicId).subscribe(
      (doctors) => {
        this.doctors = doctors;
      },
      (error) => {
        console.error('Error fetching doctors', error);
      }
    );
  }
  loadAppointments(): void {
    this.appointmentService.getAppointments().subscribe(
      (appointments) => {
        this.appointments = appointments;
      },
      (error) => {
        console.error('Error fetching doctors', error);
      }
    );
  }
  loadAvailableSlots() {
    const docId = this.appointmentForm.value.docId;
    const appointmentDate = this.appointmentForm.value.appointmentDate;

    if (docId && appointmentDate) {
      this.appointmentService
        .getAvailableSlots(docId, appointmentDate)
        .subscribe(
          (slots) => {
            this.availableSlots = slots;
          },
          (error) => {
            console.error('Error fetching available slots', error);
            this.availableSlots = [];
          }
        );
    }
  }

  saveAppointment() {
    const selectedDate = this.appointmentForm.value.appointmentDate;
    const formattedDate = `${selectedDate.getFullYear()}-${(selectedDate.getMonth() + 1).toString()
      .padStart(2, '0')}-${selectedDate.getDate().toString().padStart(2, '0')}`;
    const newAppointment = {
      patientName: this.appointmentForm.value.patientName,
      docId: this.appointmentForm.value.docId,
      birthDate: this.appointmentForm.value.birthDate,
      phone: this.appointmentForm.value.phone,
      appointmentDate: formattedDate,
      appointmentTime: this.appointmentForm.value.appointmentTime,
    };

    this.appointmentService.bookAppointment(newAppointment).subscribe(
      (res) => {
        this.appointmentForm.reset();
        this.loadAppointments();
      },
      (error) => {
        console.error('Error saving appointment:', error);
        this.availableSlots = [];
      }
    );
  }

  deleteAppointment(appointmentId: number) {
    if (confirm('Are you sure you want to delete this appointment?')) {
      this.appointmentService.deleteAppointment(appointmentId).subscribe(
        () => {
          this.loadAppointments();
        },
        (error) => {
          console.error('Error deleting appointment', error);
        }
      );
    }
  }
}
