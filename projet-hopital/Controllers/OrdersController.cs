using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_hopital.Data;
using projet_hopital.Models;
using projet_hopital.ViewModels;

namespace projet_hopital.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(
        ApplicationDbContext context, 
        UserManager<ApplicationUser> userManager,
        ILogger<OrdersController> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        var orders = await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        var model = new OrderHistoryViewModel
        {
            Orders = orders
        };

        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var userId = _userManager.GetUserId(User);
        var order = await _context.Orders
            .Include(o => o.Items)
                .ThenInclude(i => i.Service)
            .Include(o => o.Items)
                .ThenInclude(i => i.Doctor)
            .Include(o => o.Appointments)
                .ThenInclude(a => a.Doctor)
            .Include(o => o.Appointments)
                .ThenInclude(a => a.Service)
            .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);

        if (order == null)
        {
            return NotFound();
        }

        var model = new OrderViewModel
        {
            Order = order
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        var userId = _userManager.GetUserId(User);
        var basket = await _context.Baskets
            .Include(b => b.Items)
                .ThenInclude(i => i.Service)
            .Include(b => b.Items)
                .ThenInclude(i => i.Doctor)
            .FirstOrDefaultAsync(b => b.UserId == userId);

        if (basket == null || !basket.Items.Any())
        {
            TempData["Error"] = "Your basket is empty.";
            return RedirectToAction("Index", "Basket");
        }

        var model = new CheckoutViewModel
        {
            Basket = basket
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProcessCheckout(CheckoutViewModel model)
    {
        var userId = _userManager.GetUserId(User);
        var basket = await _context.Baskets
            .Include(b => b.Items)
                .ThenInclude(i => i.Service)
            .Include(b => b.Items)
                .ThenInclude(i => i.Doctor)
            .FirstOrDefaultAsync(b => b.UserId == userId);

        if (basket == null || !basket.Items.Any())
        {
            TempData["Error"] = "Your basket is empty.";
            return RedirectToAction("Index", "Basket");
        }

        // Create order
        var order = new Order
        {
            OrderNumber = GenerateOrderNumber(),
            UserId = userId!,
            TotalAmount = basket.TotalPrice,
            Status = OrderStatus.Confirmed,
            PaymentStatus = PaymentStatus.Paid, // Simulated payment
            Notes = model.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var createdAppointments = new List<Appointment>();

        // Create order items and appointments
        foreach (var basketItem in basket.Items)
        {
            var orderItem = new OrderItem
            {
                OrderId = order.Id,
                ItemType = basketItem.ItemType,
                ServiceId = basketItem.ServiceId,
                DoctorId = basketItem.DoctorId,
                ItemName = basketItem.ItemDescription,
                UnitPrice = basketItem.Price,
                Quantity = basketItem.Quantity,
                AppointmentDate = basketItem.PreferredDate,
                AppointmentTime = basketItem.PreferredTime,
                Notes = basketItem.Notes
            };
            _context.OrderItems.Add(orderItem);

            // Create appointment for appointment-type items
            if (basketItem.ItemType == BasketItemType.Appointment && 
                basketItem.DoctorId.HasValue && 
                basketItem.PreferredDate.HasValue && 
                basketItem.PreferredTime.HasValue)
            {
                var appointment = new Appointment
                {
                    UserId = userId!,
                    DoctorId = basketItem.DoctorId.Value,
                    ServiceId = basketItem.ServiceId,
                    AppointmentDate = basketItem.PreferredDate.Value,
                    StartTime = basketItem.PreferredTime.Value,
                    EndTime = basketItem.PreferredTime.Value.Add(TimeSpan.FromMinutes(30)),
                    TotalCost = basketItem.Price,
                    Status = AppointmentStatus.Confirmed,
                    Notes = basketItem.Notes,
                    OrderId = order.Id,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Appointments.Add(appointment);
                createdAppointments.Add(appointment);
            }
        }

        // Clear basket
        _context.BasketItems.RemoveRange(basket.Items);
        
        await _context.SaveChangesAsync();

        // Load navigation properties for confirmation view
        foreach (var appointment in createdAppointments)
        {
            await _context.Entry(appointment).Reference(a => a.Doctor).LoadAsync();
            if (appointment.ServiceId.HasValue)
            {
                await _context.Entry(appointment).Reference(a => a.Service).LoadAsync();
            }
        }

        return RedirectToAction("Confirmation", new { id = order.Id });
    }

    public async Task<IActionResult> Confirmation(int id)
    {
        var userId = _userManager.GetUserId(User);
        var order = await _context.Orders
            .Include(o => o.Items)
            .Include(o => o.Appointments)
                .ThenInclude(a => a.Doctor)
            .Include(o => o.Appointments)
                .ThenInclude(a => a.Service)
            .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);

        if (order == null)
        {
            return NotFound();
        }

        var model = new OrderConfirmationViewModel
        {
            Order = order,
            CreatedAppointments = order.Appointments.ToList()
        };

        return View(model);
    }

    private string GenerateOrderNumber()
    {
        return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
    }
}

