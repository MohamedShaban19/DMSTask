using DMSTaskApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DMSTaskApi.Data;
namespace DMSTaskApi.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly DataContext _context;

        public DoctorsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetDoctors/{ClincId}")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors(int ClincId)
        {
            return await _context.Doctors
                   .Where(d =>  d.ClinicId == ClincId &&  d.Is_Stopped == 0)
                   .ToListAsync();
        }
    }
}
