using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_hopital.Data;
using projet_hopital.ViewModels;

namespace projet_hopital.Controllers;

public class DoctorsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DoctorsController> _logger;

    public DoctorsController(ApplicationDbContext context, ILogger<DoctorsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> Index(int? departmentId, string? search)
    {
        var query = _context.Doctors
            .Include(d => d.Department)
            .Where(d => d.IsAvailable);

        if (departmentId.HasValue)
        {
            query = query.Where(d => d.DepartmentId == departmentId.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(d => 
                d.FirstName.ToLower().Contains(search) || 
                d.LastName.ToLower().Contains(search) ||
                d.Specialization.ToLower().Contains(search));
        }

        var model = new DoctorListViewModel
        {
            Doctors = await query.OrderBy(d => d.LastName).ToListAsync(),
            Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync(),
            SelectedDepartmentId = departmentId,
            SearchQuery = search
        };

        return View(model);
    }

    public async Task<IActionResult> Details(int id, DateTime? date)
    {
        var doctor = await _context.Doctors
            .Include(d => d.Department)
            .Include(d => d.DoctorServices)
                .ThenInclude(ds => ds.Service)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (doctor == null)
        {
            return NotFound();
        }

        var selectedDate = date ?? DateTime.Today.AddDays(1);
        if (selectedDate < DateTime.Today)
        {
            selectedDate = DateTime.Today.AddDays(1);
        }

        var model = new DoctorDetailViewModel
        {
            Doctor = doctor,
            AvailableServices = doctor.DoctorServices.Select(ds => ds.Service!).Where(s => s != null && s.IsActive).ToList(),
            SelectedDate = selectedDate,
            AvailableTimeSlots = await GetAvailableTimeSlots(doctor.Id, selectedDate)
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> GetTimeSlots(int doctorId, DateTime date)
    {
        var timeSlots = await GetAvailableTimeSlots(doctorId, date);
        return Json(timeSlots);
    }

    private async Task<List<TimeSlot>> GetAvailableTimeSlots(int doctorId, DateTime date)
    {
        // Get existing appointments for this doctor on this date
        var existingAppointments = await _context.Appointments
            .Where(a => a.DoctorId == doctorId && 
                        a.AppointmentDate.Date == date.Date &&
                        a.Status != Models.AppointmentStatus.Cancelled)
            .Select(a => a.StartTime)
            .ToListAsync();

        var timeSlots = new List<TimeSlot>();
        
        // Generate time slots from 9 AM to 5 PM
        for (int hour = 9; hour < 17; hour++)
        {
            var startTime = new TimeSpan(hour, 0, 0);
            var endTime = new TimeSpan(hour, 30, 0);
            
            timeSlots.Add(new TimeSlot
            {
                StartTime = startTime,
                EndTime = endTime,
                IsAvailable = !existingAppointments.Contains(startTime) && 
                             (date > DateTime.Today || (date == DateTime.Today && startTime > DateTime.Now.TimeOfDay))
            });

            startTime = new TimeSpan(hour, 30, 0);
            endTime = new TimeSpan(hour + 1, 0, 0);
            
            timeSlots.Add(new TimeSlot
            {
                StartTime = startTime,
                EndTime = endTime,
                IsAvailable = !existingAppointments.Contains(startTime) &&
                             (date > DateTime.Today || (date == DateTime.Today && startTime > DateTime.Now.TimeOfDay))
            });
        }

        return timeSlots;
    }
}

