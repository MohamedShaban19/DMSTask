using System.ComponentModel.DataAnnotations;

namespace DMSTaskApi.Models
{
    public class Doctor
    {
        [Key]
        public int DocId { get; set; }
        public int ClinicId { get; set; }
        public string DocName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Day_Of_Week { get; set; }
        public int Visit_Duration { get; set; }
        public int Is_Stopped { get; set; }
    }
}
