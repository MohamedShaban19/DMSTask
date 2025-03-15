using System.Linq;
using DMSTaskApi.Data;
using DMSTaskApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMSTaskApi.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly DataContext _context;

        public AppointmentsController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("available-slots/{DocId}/{date}")]
        public async Task<ActionResult<IEnumerable<string>>> GetAvailableSlots(int DocId, string date)
        {
        
            if (!DateTime.TryParse(date, out DateTime appointmentDate))
            {
                return BadRequest("Invalid date format. Use yyyy-MM-dd.");
            }
            var doctor = await _context.Doctors
                .Where(d => d.DocId == DocId && d.Is_Stopped == 0)
                .Select(d => new { d.StartTime, d.EndTime, d.Day_Of_Week, d.Visit_Duration })
                .FirstOrDefaultAsync();

            if (doctor == null)
            {
                return NotFound("Doctor not found or unavailable.");
            }
            if ((int)appointmentDate.DayOfWeek == doctor.Day_Of_Week)
            {
                return BadRequest("Doctor is on the weekend.");
            }

            var bookedSlots = await _context.Appointments
                .Where(a => a.DocId == DocId && a.AppointmentDate.Date == appointmentDate.Date && a.Status == 0)
                .Select(a => a.AppointmentTime) 
                .ToListAsync();

             List<TimeSpan> allSlots = new();
            TimeSpan slotTime = doctor.StartTime;

            while (slotTime < doctor.EndTime)
            {
                allSlots.Add(slotTime);
                slotTime = slotTime.Add(TimeSpan.FromMinutes(doctor.Visit_Duration));
            }
            var availableSlots = allSlots
                .Select(slot => appointmentDate.Date.Add(slot).ToString("hh:mm tt"))
                .Where(slot => !bookedSlots.Contains(slot))
                .ToList();

            return Ok(availableSlots);
        }
        [HttpPost("BookAppointment")]
        public async Task<ActionResult<Appointment>> BookAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(BookAppointment), new { id = appointment.AppointmentId }, appointment);
        }

        [HttpGet("LoadAppointments")]
        public async Task<ActionResult<IEnumerable<object>>> GetAppointments()
        {
            var appointments = await _context.Appointments
                .Where(a => a.Status == 0) 
                .Join(
                    _context.Doctors,
                    a => a.DocId,
                    d => d.DocId,
                    (a, d) => new { a, d }
                )
                .Join(
                    _context.Clinics, 
                    ad => ad.d.ClinicId,
                    c => c.ClinicId,
                    (ad, c) => new
                    {
                        ad.a.AppointmentId,
                        ad.a.PatientName,
                        AppointmentDate = ad.a.AppointmentDate.ToString("yyyy-MM-dd"),
                        ad.a.AppointmentTime,
                        ad.a.Status,
                        DoctorName = ad.d.DocName,
                        ClinicName = c.ClinicName
                    }
                )
                .ToListAsync();

            return Ok(appointments);
        }
        [HttpPost("DeleteAppointment/{appointmentId}")]
        public async Task<IActionResult> UpdateAppointmentStatus(int appointmentId)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

            if (appointment == null)
            {
                return NotFound("Appointment not found.");
            }

            appointment.Status = 1;
            await _context.SaveChangesAsync();

            return Ok("Appointment status updated successfully.");
        }
    }
}
