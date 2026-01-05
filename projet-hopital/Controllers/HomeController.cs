using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_hopital.Data;
using projet_hopital.Models;
using projet_hopital.ViewModels;

namespace projet_hopital.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel
        {
            FeaturedDepartments = await _context.Departments.Take(6).ToListAsync(),
            FeaturedDoctors = await _context.Doctors
                .Include(d => d.Department)
                .Where(d => d.IsAvailable)
                .Take(4)
                .ToListAsync(),
            PopularServices = await _context.MedicalServices
                .Include(s => s.Department)
                .Where(s => s.IsActive)
                .Take(6)
                .ToListAsync()
        };
        
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}