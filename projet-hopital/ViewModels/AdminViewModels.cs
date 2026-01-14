using System.ComponentModel.DataAnnotations;
using projet_hopital.Models;

namespace projet_hopital.ViewModels;

// Dashboard ViewModel
public class AdminDashboardViewModel
{
    public int TotalUsers { get; set; }
    public int TotalDoctors { get; set; }
    public int TotalServices { get; set; }
    public int TotalDepartments { get; set; }
    public int TotalAppointments { get; set; }
    public int PendingAppointments { get; set; }
    public int TodayAppointments { get; set; }
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public List<Appointment> RecentAppointments { get; set; } = new();
    public List<Order> RecentOrders { get; set; } = new();
}

// User Management ViewModels
public class UserListViewModel
{
    public List<UserViewModel> Users { get; set; } = new();
    public string? SearchQuery { get; set; }
    public string? RoleFilter { get; set; }
    public List<string> AvailableRoles { get; set; } = new();
}

public class UserViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<string> Roles { get; set; } = new();
    public int AppointmentsCount { get; set; }
    public int OrdersCount { get; set; }
    
    public string FullName => $"{FirstName} {LastName}";
}

public class EditUserViewModel
{
    public string Id { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "L'email est requis")]
    [EmailAddress(ErrorMessage = "Email invalide")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Le prénom est requis")]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Phone(ErrorMessage = "Numéro de téléphone invalide")]
    public string? PhoneNumber { get; set; }
    
    [StringLength(200)]
    public string? Address { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }
    
    public List<string> SelectedRoles { get; set; } = new();
    public List<string> AvailableRoles { get; set; } = new();
}

public class CreateUserViewModel
{
    [Required(ErrorMessage = "L'email est requis")]
    [EmailAddress(ErrorMessage = "Email invalide")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Le mot de passe est requis")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas")]
    public string ConfirmPassword { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Le prénom est requis")]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Phone(ErrorMessage = "Numéro de téléphone invalide")]
    public string? PhoneNumber { get; set; }
    
    [StringLength(200)]
    public string? Address { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }
    
    public List<string> SelectedRoles { get; set; } = new();
    public List<string> AvailableRoles { get; set; } = new();
}

// Doctor Management ViewModels
public class AdminDoctorListViewModel
{
    public List<Doctor> Doctors { get; set; } = new();
    public List<Department> Departments { get; set; } = new();
    public string? SearchQuery { get; set; }
    public int? DepartmentFilter { get; set; }
}

public class CreateDoctorViewModel
{
    [Required(ErrorMessage = "Le prénom est requis")]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La spécialisation est requise")]
    [StringLength(200)]
    public string Specialization { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Bio { get; set; }
    
    [StringLength(255)]
    [Url(ErrorMessage = "URL invalide")]
    public string? ImageUrl { get; set; }
    
    [EmailAddress(ErrorMessage = "Email invalide")]
    public string? Email { get; set; }
    
    [Phone(ErrorMessage = "Numéro de téléphone invalide")]
    public string? Phone { get; set; }
    
    [Required(ErrorMessage = "Les frais de consultation sont requis")]
    [Range(0, 100000, ErrorMessage = "Les frais doivent être entre 0 et 100000")]
    public decimal ConsultationFee { get; set; }
    
    [Range(0, 60, ErrorMessage = "L'expérience doit être entre 0 et 60 ans")]
    public int YearsOfExperience { get; set; }
    
    public bool IsAvailable { get; set; } = true;
    
    [Required(ErrorMessage = "Le département est requis")]
    public int DepartmentId { get; set; }
    
    public List<int> SelectedServiceIds { get; set; } = new();
    
    public List<Department> Departments { get; set; } = new();
    public List<MedicalService> AvailableServices { get; set; } = new();
}

public class EditDoctorViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Le prénom est requis")]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La spécialisation est requise")]
    [StringLength(200)]
    public string Specialization { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Bio { get; set; }
    
    [StringLength(255)]
    [Url(ErrorMessage = "URL invalide")]
    public string? ImageUrl { get; set; }
    
    [EmailAddress(ErrorMessage = "Email invalide")]
    public string? Email { get; set; }
    
    [Phone(ErrorMessage = "Numéro de téléphone invalide")]
    public string? Phone { get; set; }
    
    [Required(ErrorMessage = "Les frais de consultation sont requis")]
    [Range(0, 100000, ErrorMessage = "Les frais doivent être entre 0 et 100000")]
    public decimal ConsultationFee { get; set; }
    
    [Range(0, 60, ErrorMessage = "L'expérience doit être entre 0 et 60 ans")]
    public int YearsOfExperience { get; set; }
    
    public bool IsAvailable { get; set; } = true;
    
    [Required(ErrorMessage = "Le département est requis")]
    public int DepartmentId { get; set; }
    
    public List<int> SelectedServiceIds { get; set; } = new();
    
    public List<Department> Departments { get; set; } = new();
    public List<MedicalService> AvailableServices { get; set; } = new();
}

// Service Management ViewModels
public class AdminServiceListViewModel
{
    public List<MedicalService> Services { get; set; } = new();
    public List<Department> Departments { get; set; } = new();
    public string? SearchQuery { get; set; }
    public int? DepartmentFilter { get; set; }
}

public class CreateServiceViewModel
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "Le prix est requis")]
    [Range(0, 100000, ErrorMessage = "Le prix doit être entre 0 et 100000")]
    public decimal Price { get; set; }
    
    [Range(5, 600, ErrorMessage = "La durée doit être entre 5 et 600 minutes")]
    public int DurationMinutes { get; set; } = 30;
    
    [StringLength(255)]
    [Url(ErrorMessage = "URL invalide")]
    public string? ImageUrl { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    [Required(ErrorMessage = "Le département est requis")]
    public int DepartmentId { get; set; }
    
    public List<Department> Departments { get; set; } = new();
}

public class EditServiceViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "Le prix est requis")]
    [Range(0, 100000, ErrorMessage = "Le prix doit être entre 0 et 100000")]
    public decimal Price { get; set; }
    
    [Range(5, 600, ErrorMessage = "La durée doit être entre 5 et 600 minutes")]
    public int DurationMinutes { get; set; } = 30;
    
    [StringLength(255)]
    [Url(ErrorMessage = "URL invalide")]
    public string? ImageUrl { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    [Required(ErrorMessage = "Le département est requis")]
    public int DepartmentId { get; set; }
    
    public List<Department> Departments { get; set; } = new();
}

// Department Management ViewModels
public class AdminDepartmentListViewModel
{
    public List<Department> Departments { get; set; } = new();
    public string? SearchQuery { get; set; }
}

public class CreateDepartmentViewModel
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [StringLength(255)]
    [Url(ErrorMessage = "URL invalide")]
    public string? ImageUrl { get; set; }
}

public class EditDepartmentViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [StringLength(255)]
    [Url(ErrorMessage = "URL invalide")]
    public string? ImageUrl { get; set; }
}

// Appointment Management ViewModels
public class AdminAppointmentListViewModel
{
    public List<Appointment> Appointments { get; set; } = new();
    public List<Doctor> Doctors { get; set; } = new();
    public AppointmentStatus? StatusFilter { get; set; }
    public int? DoctorFilter { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string? SearchQuery { get; set; }
}

public class EditAppointmentViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "La date est requise")]
    [DataType(DataType.Date)]
    public DateTime AppointmentDate { get; set; }
    
    [Required(ErrorMessage = "L'heure de début est requise")]
    public TimeSpan StartTime { get; set; }
    
    [Required(ErrorMessage = "L'heure de fin est requise")]
    public TimeSpan EndTime { get; set; }
    
    [StringLength(500)]
    public string? Notes { get; set; }
    
    public AppointmentStatus Status { get; set; }
    
    [Required(ErrorMessage = "Le médecin est requis")]
    public int DoctorId { get; set; }
    
    public int? ServiceId { get; set; }
    
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser? User { get; set; }
    
    public List<Doctor> Doctors { get; set; } = new();
    public List<MedicalService> Services { get; set; } = new();
}

// Order Management ViewModels
public class AdminOrderListViewModel
{
    public List<Order> Orders { get; set; } = new();
    public OrderStatus? StatusFilter { get; set; }
    public PaymentStatus? PaymentStatusFilter { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string? SearchQuery { get; set; }
}

public class EditOrderViewModel
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser? User { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    
    [StringLength(500)]
    public string? Notes { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public List<Appointment> Appointments { get; set; } = new();
}

