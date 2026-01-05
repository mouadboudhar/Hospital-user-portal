using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_hopital.Data;
using projet_hopital.ViewModels;

namespace projet_hopital.Controllers;

public class ServicesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ServicesController> _logger;

    public ServicesController(ApplicationDbContext context, ILogger<ServicesController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> Index(int? departmentId, string? search)
    {
        var query = _context.MedicalServices
            .Include(s => s.Department)
            .Where(s => s.IsActive);

        if (departmentId.HasValue)
        {
            query = query.Where(s => s.DepartmentId == departmentId.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(s => 
                s.Name.ToLower().Contains(search) || 
                (s.Description != null && s.Description.ToLower().Contains(search)));
        }

        var model = new ServiceListViewModel
        {
            Services = await query.OrderBy(s => s.Name).ToListAsync(),
            Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync(),
            SelectedDepartmentId = departmentId,
            SearchQuery = search
        };

        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var service = await _context.MedicalServices
            .Include(s => s.Department)
            .Include(s => s.DoctorServices)
                .ThenInclude(ds => ds.Doctor)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (service == null)
        {
            return NotFound();
        }

        return View(service);
    }

    public async Task<IActionResult> ByDepartment(int id)
    {
        var department = await _context.Departments
            .Include(d => d.Services.Where(s => s.IsActive))
            .FirstOrDefaultAsync(d => d.Id == id);

        if (department == null)
        {
            return NotFound();
        }

        return View(department);
    }
}

