using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VaccinationTracker.Data;
using VaccinationTracker.Models;

namespace VaccinationTracker.Controllers
{
    public class VaccinationSchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public VaccinationSchedulesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: VaccinationSchedules
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            var schedules = await _context.VaccinationSchedules
                .Include(vs => vs.User)
                .Include(vs => vs.Vaccine)
                .ToListAsync();

            if (!isAdmin)
            {
                schedules = schedules.Where(vs => vs.UserId == user.Id).ToList();
            }

            return View(schedules);
        }

        // GET: VaccinationSchedules/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationSchedule = await _context.VaccinationSchedules
                .Include(vs => vs.User)
                .Include(vs => vs.Vaccine)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vaccinationSchedule == null)
            {
                return NotFound();
            }

            return View(vaccinationSchedule);
        }

        // GET: VaccinationSchedules/Create
        public IActionResult Create()
        {
            ViewBag.Users = _userManager.Users.Select(u => new { u.Id, u.Email }).ToList();
            ViewBag.Vaccines = _context.Vaccines.Select(v => new { v.Id, v.Name }).ToList();

            if (ViewBag.Users.Count == 0)
            {
                ModelState.AddModelError("UserId", "No users available. Please add users first.");
            }
            if (ViewBag.Vaccines.Count == 0)
            {
                ModelState.AddModelError("VaccineId", "No vaccines available. Please add vaccines first.");
            }

            return View();
        }

        // POST: VaccinationSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,VaccineId,ScheduledDate,Status,CertificateUrl")] VaccinationSchedule vaccinationSchedule)
        {
            if (ModelState.IsValid)
            {
                vaccinationSchedule.Id = Guid.NewGuid();
                vaccinationSchedule.Vaccine = await _context.Vaccines.FindAsync(vaccinationSchedule.VaccineId); // Ensure Vaccine is bound
                _context.Add(vaccinationSchedule);
                await _context.SaveChangesAsync();

                // Add notification for the assigned user
                var user = await _userManager.FindByIdAsync(vaccinationSchedule.UserId);
                if (user != null)
                {
                    var notification = new Notification
                    {
                        Id = Guid.NewGuid(),
                        UserId = Guid.Parse(user.Id),
                        Message = $"You have been assigned a new vaccination schedule for vaccine {vaccinationSchedule.Vaccine.Name}.",
                        SentAt = DateTime.UtcNow
                    };
                    _context.Notifications.Add(notification);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationSchedule);
        }

        // GET: VaccinationSchedules/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationSchedule = await _context.VaccinationSchedules.FindAsync(id);
            if (vaccinationSchedule == null)
            {
                return NotFound();
            }

            ViewBag.Users = new SelectList(_userManager.Users, "Id", "Email", vaccinationSchedule.UserId);
            ViewBag.Vaccines = new SelectList(_context.Vaccines, "Id", "Name", vaccinationSchedule.VaccineId);
            ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(ScheduleStatus)).Cast<ScheduleStatus>(), vaccinationSchedule.Status);

            return View(vaccinationSchedule);
        }

        // POST: VaccinationSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,VaccineId,ScheduledDate,Status,CertificateUrl")] VaccinationSchedule vaccinationSchedule)
        {
            if (id != vaccinationSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccinationSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccinationScheduleExists(vaccinationSchedule.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Users = new SelectList(_userManager.Users, "Id", "Email", vaccinationSchedule.UserId);
            ViewBag.Vaccines = new SelectList(_context.Vaccines, "Id", "Name", vaccinationSchedule.VaccineId);
            ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(ScheduleStatus)).Cast<ScheduleStatus>(), vaccinationSchedule.Status);

            return View(vaccinationSchedule);
        }

        // GET: VaccinationSchedules/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationSchedule = await _context.VaccinationSchedules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccinationSchedule == null)
            {
                return NotFound();
            }

            return View(vaccinationSchedule);
        }

        // POST: VaccinationSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var vaccinationSchedule = await _context.VaccinationSchedules.FindAsync(id);
            if (vaccinationSchedule != null)
            {
                _context.VaccinationSchedules.Remove(vaccinationSchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationScheduleExists(Guid id)
        {
            return _context.VaccinationSchedules.Any(e => e.Id == id);
        }
    }
}
