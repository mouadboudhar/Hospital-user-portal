using projet_hopital.Models;

namespace projet_hopital.ViewModels;

public class HomeViewModel
{
    public List<Department> FeaturedDepartments { get; set; } = new();
    public List<Doctor> FeaturedDoctors { get; set; } = new();
    public List<MedicalService> PopularServices { get; set; } = new();
}

public class DoctorListViewModel
{
    public List<Doctor> Doctors { get; set; } = new();
    public List<Department> Departments { get; set; } = new();
    public int? SelectedDepartmentId { get; set; }
    public string? SearchQuery { get; set; }
}

public class DoctorDetailViewModel
{
    public Doctor Doctor { get; set; } = null!;
    public List<MedicalService> AvailableServices { get; set; } = new();
    public List<TimeSlot> AvailableTimeSlots { get; set; } = new();
    public DateTime SelectedDate { get; set; } = DateTime.Today.AddDays(1);
}

public class TimeSlot
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAvailable { get; set; }
    public string DisplayTime => $"{StartTime:hh\\:mm} - {EndTime:hh\\:mm}";
}

public class ServiceListViewModel
{
    public List<MedicalService> Services { get; set; } = new();
    public List<Department> Departments { get; set; } = new();
    public int? SelectedDepartmentId { get; set; }
    public string? SearchQuery { get; set; }
}

public class BookAppointmentViewModel
{
    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
    public int? ServiceId { get; set; }
    public MedicalService? Service { get; set; }
    public DateTime AppointmentDate { get; set; }
    public TimeSpan SelectedTime { get; set; }
    public string? Notes { get; set; }
    public List<TimeSlot> AvailableTimeSlots { get; set; } = new();
}

