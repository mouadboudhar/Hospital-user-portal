using projet_hopital.Models;

namespace projet_hopital.ViewModels;

public class BasketViewModel
{
    public Basket? Basket { get; set; }
    public decimal TotalPrice => Basket?.TotalPrice ?? 0;
    public int TotalItems => Basket?.TotalItems ?? 0;
}

public class AddToBasketViewModel
{
    public BasketItemType ItemType { get; set; }
    public int? ServiceId { get; set; }
    public int? DoctorId { get; set; }
    public DateTime? PreferredDate { get; set; }
    public TimeSpan? PreferredTime { get; set; }
    public string? Notes { get; set; }
}

public class CheckoutViewModel
{
    public Basket? Basket { get; set; }
    public string? Notes { get; set; }
    public string PaymentMethod { get; set; } = "Card";
}

public class OrderViewModel
{
    public Order Order { get; set; } = null!;
}

public class OrderHistoryViewModel
{
    public List<Order> Orders { get; set; } = new();
}

public class OrderConfirmationViewModel
{
    public Order Order { get; set; } = null!;
    public List<Appointment> CreatedAppointments { get; set; } = new();
}

