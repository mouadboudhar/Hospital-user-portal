using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_hopital.Data;
using projet_hopital.Models;
using projet_hopital.ViewModels;

namespace projet_hopital.Controllers;

[Authorize]
public class BasketController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<BasketController> _logger;

    public BasketController(
        ApplicationDbContext context, 
        UserManager<ApplicationUser> userManager,
        ILogger<BasketController> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        var basket = await GetOrCreateBasket(userId!);

        var model = new BasketViewModel
        {
            Basket = basket
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddService(int serviceId, int? doctorId, DateTime? preferredDate, string? preferredTime, string? notes)
    {
        var userId = _userManager.GetUserId(User);
        var basket = await GetOrCreateBasket(userId!);

        var service = await _context.MedicalServices.FindAsync(serviceId);
        if (service == null)
        {
            TempData["Error"] = "Service not found.";
            return RedirectToAction("Index", "Services");
        }

        TimeSpan? time = null;
        if (!string.IsNullOrEmpty(preferredTime) && TimeSpan.TryParse(preferredTime, out var parsedTime))
        {
            time = parsedTime;
        }

        var basketItem = new BasketItem
        {
            BasketId = basket.Id,
            ItemType = BasketItemType.Service,
            ServiceId = serviceId,
            DoctorId = doctorId,
            Price = service.Price,
            PreferredDate = preferredDate,
            PreferredTime = time,
            Notes = notes,
            Quantity = 1
        };

        _context.BasketItems.Add(basketItem);
        basket.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        TempData["Success"] = $"{service.Name} added to your basket.";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAppointment(int doctorId, DateTime appointmentDate, string appointmentTime, int? serviceId, string? notes)
    {
        var userId = _userManager.GetUserId(User);
        var basket = await GetOrCreateBasket(userId!);

        var doctor = await _context.Doctors.FindAsync(doctorId);
        if (doctor == null)
        {
            TempData["Error"] = "Doctor not found.";
            return RedirectToAction("Index", "Doctors");
        }

        if (!TimeSpan.TryParse(appointmentTime, out var time))
        {
            TempData["Error"] = "Invalid time selected.";
            return RedirectToAction("Details", "Doctors", new { id = doctorId });
        }

        var price = doctor.ConsultationFee;
        
        // Add service price if selected
        if (serviceId.HasValue)
        {
            var service = await _context.MedicalServices.FindAsync(serviceId.Value);
            if (service != null)
            {
                price += service.Price;
            }
        }

        var basketItem = new BasketItem
        {
            BasketId = basket.Id,
            ItemType = BasketItemType.Appointment,
            DoctorId = doctorId,
            ServiceId = serviceId,
            Price = price,
            PreferredDate = appointmentDate,
            PreferredTime = time,
            Notes = notes,
            Quantity = 1
        };

        _context.BasketItems.Add(basketItem);
        basket.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        TempData["Success"] = $"Appointment with {doctor.FullName} added to your basket.";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveItem(int itemId)
    {
        var userId = _userManager.GetUserId(User);
        var basket = await GetOrCreateBasket(userId!);

        var item = await _context.BasketItems
            .FirstOrDefaultAsync(i => i.Id == itemId && i.BasketId == basket.Id);

        if (item != null)
        {
            _context.BasketItems.Remove(item);
            basket.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Item removed from basket.";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Clear()
    {
        var userId = _userManager.GetUserId(User);
        var basket = await GetOrCreateBasket(userId!);

        _context.BasketItems.RemoveRange(basket.Items);
        basket.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        TempData["Success"] = "Basket cleared.";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> GetBasketCount()
    {
        if (!User.Identity?.IsAuthenticated ?? true)
        {
            return Json(new { count = 0 });
        }

        var userId = _userManager.GetUserId(User);
        var basket = await _context.Baskets
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.UserId == userId);

        return Json(new { count = basket?.TotalItems ?? 0 });
    }

    private async Task<Basket> GetOrCreateBasket(string userId)
    {
        var basket = await _context.Baskets
            .Include(b => b.Items)
                .ThenInclude(i => i.Service)
            .Include(b => b.Items)
                .ThenInclude(i => i.Doctor)
            .FirstOrDefaultAsync(b => b.UserId == userId);

        if (basket == null)
        {
            basket = new Basket
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Baskets.Add(basket);
            await _context.SaveChangesAsync();
        }

        return basket;
    }
}

