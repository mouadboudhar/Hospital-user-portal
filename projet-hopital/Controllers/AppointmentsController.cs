using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_hopital.Data;
using projet_hopital.Models;

namespace projet_hopital.Controllers;

[Authorize]
public class AppointmentsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<AppointmentsController> _logger;

    public AppointmentsController(
        ApplicationDbContext context, 
        UserManager<ApplicationUser> userManager,
        ILogger<AppointmentsController> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        var appointments = await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Service)
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.AppointmentDate)
            .ThenBy(a => a.StartTime)
            .ToListAsync();

        return View(appointments);
    }

    public async Task<IActionResult> Details(int id)
    {
        var userId = _userManager.GetUserId(User);
        var appointment = await _context.Appointments
            .Include(a => a.Doctor)
                .ThenInclude(d => d!.Department)
            .Include(a => a.Service)
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        if (appointment == null)
        {
            return NotFound();
        }

        return View(appointment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        var userId = _userManager.GetUserId(User);
        var appointment = await _context.Appointments
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        if (appointment == null)
        {
            return NotFound();
        }

        if (appointment.Status == AppointmentStatus.Completed)
        {
            TempData["Error"] = "Cannot cancel a completed appointment.";
            return RedirectToAction("Details", new { id });
        }

        if (appointment.AppointmentDate <= DateTime.Today)
        {
            TempData["Error"] = "Cannot cancel an appointment on the same day.";
            return RedirectToAction("Details", new { id });
        }

        appointment.Status = AppointmentStatus.Cancelled;
        await _context.SaveChangesAsync();

        TempData["Success"] = "Appointment cancelled successfully.";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Upcoming()
    {
        var userId = _userManager.GetUserId(User);
        var appointments = await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Service)
            .Where(a => a.UserId == userId && 
                        a.AppointmentDate >= DateTime.Today &&
                        a.Status != AppointmentStatus.Cancelled)
            .OrderBy(a => a.AppointmentDate)
            .ThenBy(a => a.StartTime)
            .ToListAsync();

        return View(appointments);
    }
}

