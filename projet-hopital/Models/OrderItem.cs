using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projet_hopital.Models;

public class OrderItem
{
    public int Id { get; set; }
    
    public int OrderId { get; set; }
    public virtual Order? Order { get; set; }
    
    public BasketItemType ItemType { get; set; }
    
    public int? ServiceId { get; set; }
    public virtual MedicalService? Service { get; set; }
    
    public int? DoctorId { get; set; }
    public virtual Doctor? Doctor { get; set; }
    
    [Required]
    [StringLength(200)]
    public string ItemName { get; set; } = string.Empty;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }
    
    public int Quantity { get; set; } = 1;
    
    public DateTime? AppointmentDate { get; set; }
    public TimeSpan? AppointmentTime { get; set; }
    
    [StringLength(500)]
    public string? Notes { get; set; }
    
    [NotMapped]
    public decimal SubTotal => UnitPrice * Quantity;
}

