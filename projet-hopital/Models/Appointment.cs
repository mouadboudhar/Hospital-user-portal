using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projet_hopital.Models;

public enum AppointmentStatus
{
    Pending,
    Confirmed,
    Completed,
    Cancelled
}

public class Appointment
{
    public int Id { get; set; }
    
    [Required]
    public DateTime AppointmentDate { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
    
    [StringLength(500)]
    public string? Notes { get; set; }
    
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalCost { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser? User { get; set; }
    
    public int DoctorId { get; set; }
    public virtual Doctor? Doctor { get; set; }
    
    public int? ServiceId { get; set; }
    public virtual MedicalService? Service { get; set; }
    
    public int? OrderId { get; set; }
    public virtual Order? Order { get; set; }
}

