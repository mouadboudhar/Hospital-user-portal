using System.ComponentModel.DataAnnotations;

namespace projet_hopital.Models;

public class Doctor
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(200)]
    public string Specialization { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Bio { get; set; }
    
    [StringLength(255)]
    public string? ImageUrl { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
    [Phone]
    public string? Phone { get; set; }
    
    public decimal ConsultationFee { get; set; }
    
    public bool IsAvailable { get; set; } = true;
    
    public int YearsOfExperience { get; set; }
    
    public int DepartmentId { get; set; }
    public virtual Department? Department { get; set; }
    
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual ICollection<DoctorService> DoctorServices { get; set; } = new List<DoctorService>();
    
    public string FullName => $"Dr. {FirstName} {LastName}";
}

