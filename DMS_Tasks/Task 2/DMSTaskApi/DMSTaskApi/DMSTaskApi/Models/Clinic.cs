using System.ComponentModel.DataAnnotations;

namespace DMSTaskApi.Models
{
    public class Clinic
    {
        [Key]
        public int ClinicId { get; set; }
        public string ClinicName { get; set; }
        public int Is_Stopped { get; set; }
    }
}
