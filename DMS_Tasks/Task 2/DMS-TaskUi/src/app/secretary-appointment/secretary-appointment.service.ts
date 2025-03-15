import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SecretaryAppointmentService {
  private apiUrl = 'https://localhost:44373/api';

  constructor(private http: HttpClient) {}

  getDoctors(clinicId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/doctors/GetDoctors/${clinicId}`);
  }
  getClincs(): Observable<any> {
    return this.http.get(`${this.apiUrl}/Clincs/GetClincs`);
  }

  getAvailableSlots(docId: number, date: string): Observable<string[]> {
    const dateObj = new Date(date);
    dateObj.setMinutes(dateObj.getMinutes() - dateObj.getTimezoneOffset());
    const formattedDate = dateObj.toISOString().split('T')[0];
    return this.http.get<string[]>(
      `${this.apiUrl}/appointments/available-slots/${docId}/${formattedDate}`
    );
  }

  bookAppointment(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/appointments/BookAppointment`, data);
  }

  getAppointments(): Observable<any> {
    return this.http.get(`${this.apiUrl}/appointments/LoadAppointments`);
  }
  deleteAppointment(appointmentId: number): Observable<any> {
    return this.http.post(
      `${this.apiUrl}/appointments/DeleteAppointment/${appointmentId}`,
      {}
    );
  }
}
