using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccinationTracker.Data;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationTracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.VaccinationSchedules
                .Include(vs => vs.User)
                .Include(vs => vs.Vaccine)
                .GroupBy(vs => new { vs.User.Email, vs.Status, vs.Vaccine.Name })
                .Select(g => new
                {
                    UserEmail = g.Key.Email,
                    Status = g.Key.Status.ToString(),
                    VaccineName = g.Key.Name,
                    Count = g.Count()
                })
                .ToListAsync();

            return View(data);
        }
    }
}
