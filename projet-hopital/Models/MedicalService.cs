using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projet_hopital.Models;

public class MedicalService
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    public int DurationMinutes { get; set; } = 30;
    
    [StringLength(255)]
    public string? ImageUrl { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public int DepartmentId { get; set; }
    public virtual Department? Department { get; set; }
    
    public virtual ICollection<DoctorService> DoctorServices { get; set; } = new List<DoctorService>();
    public virtual ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

