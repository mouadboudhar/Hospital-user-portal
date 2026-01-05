using System.ComponentModel.DataAnnotations;

namespace projet_hopital.Models;

public class Department
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [StringLength(255)]
    public string? ImageUrl { get; set; }
    
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    public virtual ICollection<MedicalService> Services { get; set; } = new List<MedicalService>();
}

