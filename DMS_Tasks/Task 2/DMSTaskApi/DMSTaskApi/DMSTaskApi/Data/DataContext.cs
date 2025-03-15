using Microsoft.EntityFrameworkCore;
using DMSTaskApi.Models;

namespace DMSTaskApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
