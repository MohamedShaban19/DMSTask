using DMSTaskApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DMSTaskApi.Data;
namespace DMSTaskApi.Controllers
{
    [Route("api/Clincs")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly DataContext _context;

        public ClinicsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetClincs")]
        public async Task<ActionResult<IEnumerable<Clinic>>> getClincs()
        {
            return await _context.Clinics
                   .Where(d => d.Is_Stopped == 0)
                   .ToListAsync();
        }
    }
}
