namespace projet_hopital.Models;

public class DoctorService
{
    public int DoctorId { get; set; }
    public virtual Doctor? Doctor { get; set; }
    
    public int ServiceId { get; set; }
    public virtual MedicalService? Service { get; set; }
}

