import { Routes } from '@angular/router';
import { SecretaryAppointmentComponent } from './secretary-appointment/secretary-appointment.component';

export const routes: Routes = [
  { path: 'secretary-appointment', component: SecretaryAppointmentComponent },
  { path: '', redirectTo: 'secretary-appointment', pathMatch: 'full' },
  { path: '**', redirectTo: 'secretary-appointment' }
];
