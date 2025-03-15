namespace DMSTaskApi.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int DocId { get; set; }
        public string PatientName { get; set; }
        public string Phone { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string AppointmentTime { get; set; }
        public int Status { get; set; }
    }
}
