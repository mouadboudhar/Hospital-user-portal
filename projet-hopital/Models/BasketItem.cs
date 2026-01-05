using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projet_hopital.Models;

public enum BasketItemType
{
    Service,
    Appointment
}

public class BasketItem
{
    public int Id { get; set; }
    
    public int BasketId { get; set; }
    public virtual Basket? Basket { get; set; }
    
    public BasketItemType ItemType { get; set; }
    
    public int? ServiceId { get; set; }
    public virtual MedicalService? Service { get; set; }
    
    public int? DoctorId { get; set; }
    public virtual Doctor? Doctor { get; set; }
    
    public DateTime? PreferredDate { get; set; }
    public TimeSpan? PreferredTime { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    public int Quantity { get; set; } = 1;
    
    [StringLength(500)]
    public string? Notes { get; set; }
    
    [NotMapped]
    public decimal SubTotal => Price * Quantity;
    
    [NotMapped]
    public string ItemDescription
    {
        get
        {
            if (ItemType == BasketItemType.Service && Service != null)
                return Service.Name;
            if (ItemType == BasketItemType.Appointment && Doctor != null)
                return $"Consultation with {Doctor.FullName}";
            return "Item";
        }
    }
}

