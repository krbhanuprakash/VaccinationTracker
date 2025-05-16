using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VaccinationTracker.Data;
using VaccinationTracker.Models;
using Microsoft.AspNetCore.Identity;

namespace VaccinationTracker.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public NotificationsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Notifications
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            // Fix for CS0019: Convert the `user.Id` (string) to a `Guid` before comparison.
            var notifications = await _context.Notifications
                .Where(n => isAdmin || n.UserId == Guid.Parse(user.Id))
                .ToListAsync();

            foreach (var notification in notifications)
            {
                var userEntity = await _context.Users.FindAsync(notification.UserId.ToString());
                notification.User = userEntity?.UserName;
            }

            return View(notifications);
        }

        // GET: Notifications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = await _context.Notifications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notification == null)
            {
                return NotFound();
            }

            return View(notification);
        }

        // GET: Notifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Message,SentAt")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                notification.Id = Guid.NewGuid();
                _context.Add(notification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notification);
        }

        // GET: Notifications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }
            return View(notification);
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,Message,SentAt")] Notification notification)
        {
            if (id != notification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificationExists(notification.Id))
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
            return View(notification);
        }

        // GET: Notifications/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = await _context.Notifications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notification == null)
            {
                return NotFound();
            }

            return View(notification);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificationExists(Guid id)
        {
            return _context.Notifications.Any(e => e.Id == id);
        }
    }
}
