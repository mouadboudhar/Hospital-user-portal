using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_hopital.Data;
using projet_hopital.Models;
using projet_hopital.ViewModels;

namespace projet_hopital.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AdminController> _logger;

    public AdminController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<AdminController> logger)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    #region Dashboard

    public async Task<IActionResult> Index()
    {
        var model = new AdminDashboardViewModel
        {
            TotalUsers = await _userManager.Users.CountAsync(),
            TotalDoctors = await _context.Doctors.CountAsync(),
            TotalServices = await _context.MedicalServices.CountAsync(),
            TotalDepartments = await _context.Departments.CountAsync(),
            TotalAppointments = await _context.Appointments.CountAsync(),
            PendingAppointments = await _context.Appointments.CountAsync(a => a.Status == AppointmentStatus.Pending),
            TodayAppointments = await _context.Appointments.CountAsync(a => a.AppointmentDate.Date == DateTime.Today),
            TotalOrders = await _context.Orders.CountAsync(),
            TotalRevenue = await _context.Orders
                .Where(o => o.PaymentStatus == PaymentStatus.Paid)
                .SumAsync(o => o.TotalAmount),
            RecentAppointments = await _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Doctor)
                .OrderByDescending(a => a.CreatedAt)
                .Take(5)
                .ToListAsync(),
            RecentOrders = await _context.Orders
                .Include(o => o.User)
                .OrderByDescending(o => o.CreatedAt)
                .Take(5)
                .ToListAsync()
        };

        return View(model);
    }

    #endregion

    #region User Management

    public async Task<IActionResult> Users(string? search, string? roleFilter)
    {
        var users = _userManager.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            users = users.Where(u =>
                u.Email!.ToLower().Contains(search) ||
                u.FirstName.ToLower().Contains(search) ||
                u.LastName.ToLower().Contains(search));
        }

        var userList = await users.ToListAsync();
        var userViewModels = new List<UserViewModel>();

        foreach (var user in userList)
        {
            var roles = await _userManager.GetRolesAsync(user);
            
            if (!string.IsNullOrWhiteSpace(roleFilter) && !roles.Contains(roleFilter))
                continue;

            userViewModels.Add(new UserViewModel
            {
                Id = user.Id,
                Email = user.Email ?? "",
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt,
                Roles = roles.ToList(),
                AppointmentsCount = await _context.Appointments.CountAsync(a => a.UserId == user.Id),
                OrdersCount = await _context.Orders.CountAsync(o => o.UserId == user.Id)
            });
        }

        var model = new UserListViewModel
        {
            Users = userViewModels,
            SearchQuery = search,
            RoleFilter = roleFilter,
            AvailableRoles = await _roleManager.Roles.Select(r => r.Name!).ToListAsync()
        };

        return View(model);
    }

    public async Task<IActionResult> CreateUser()
    {
        var model = new CreateUserViewModel
        {
            AvailableRoles = await _roleManager.Roles.Select(r => r.Name!).ToListAsync()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(CreateUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address ?? string.Empty,
                DateOfBirth = model.DateOfBirth ?? DateTime.Today,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (model.SelectedRoles.Any())
                {
                    await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                }

                TempData["Success"] = "Utilisateur créé avec succès.";
                return RedirectToAction(nameof(Users));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        model.AvailableRoles = await _roleManager.Roles.Select(r => r.Name!).ToListAsync();
        return View(model);
    }

    public async Task<IActionResult> EditUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var roles = await _userManager.GetRolesAsync(user);

        var model = new EditUserViewModel
        {
            Id = user.Id,
            Email = user.Email ?? "",
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            DateOfBirth = user.DateOfBirth,
            SelectedRoles = roles.ToList(),
            AvailableRoles = await _roleManager.Roles.Select(r => r.Name!).ToListAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(EditUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.Email = model.Email;
            user.UserName = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address ?? string.Empty;
            user.DateOfBirth = model.DateOfBirth ?? DateTime.Today;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (model.SelectedRoles.Any())
                {
                    await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                }

                TempData["Success"] = "Utilisateur mis à jour avec succès.";
                return RedirectToAction(nameof(Users));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        model.AvailableRoles = await _roleManager.Roles.Select(r => r.Name!).ToListAsync();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var currentUserId = _userManager.GetUserId(User);
        if (user.Id == currentUserId)
        {
            TempData["Error"] = "Vous ne pouvez pas supprimer votre propre compte.";
            return RedirectToAction(nameof(Users));
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            TempData["Success"] = "Utilisateur supprimé avec succès.";
        }
        else
        {
            TempData["Error"] = "Erreur lors de la suppression de l'utilisateur.";
        }

        return RedirectToAction(nameof(Users));
    }

    #endregion

    #region Doctor Management

    public async Task<IActionResult> Doctors(string? search, int? departmentId)
    {
        var query = _context.Doctors.Include(d => d.Department).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(d =>
                d.FirstName.ToLower().Contains(search) ||
                d.LastName.ToLower().Contains(search) ||
                d.Specialization.ToLower().Contains(search));
        }

        if (departmentId.HasValue)
        {
            query = query.Where(d => d.DepartmentId == departmentId.Value);
        }

        var model = new AdminDoctorListViewModel
        {
            Doctors = await query.OrderBy(d => d.LastName).ToListAsync(),
            Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync(),
            SearchQuery = search,
            DepartmentFilter = departmentId
        };

        return View(model);
    }

    public async Task<IActionResult> CreateDoctor()
    {
        var model = new CreateDoctorViewModel
        {
            IsAvailable = true,
            Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync(),
            AvailableServices = await _context.MedicalServices.Where(s => s.IsActive).OrderBy(s => s.Name).ToListAsync()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDoctor(CreateDoctorViewModel model)
    {
        if (ModelState.IsValid)
        {
            var doctor = new Doctor
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Specialization = model.Specialization,
                Bio = model.Bio,
                ImageUrl = model.ImageUrl,
                Email = model.Email,
                Phone = model.Phone,
                ConsultationFee = model.ConsultationFee,
                YearsOfExperience = model.YearsOfExperience,
                IsAvailable = true, // Always visible on user portal by default
                DepartmentId = model.DepartmentId
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            // Add doctor services
            if (model.SelectedServiceIds.Any())
            {
                foreach (var serviceId in model.SelectedServiceIds)
                {
                    _context.DoctorServices.Add(new DoctorService
                    {
                        DoctorId = doctor.Id,
                        ServiceId = serviceId
                    });
                }
                await _context.SaveChangesAsync();
            }

            TempData["Success"] = "Médecin créé avec succès.";
            return RedirectToAction(nameof(Doctors));
        }

        model.Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync();
        model.AvailableServices = await _context.MedicalServices.Where(s => s.IsActive).OrderBy(s => s.Name).ToListAsync();
        return View(model);
    }

    public async Task<IActionResult> EditDoctor(int id)
    {
        var doctor = await _context.Doctors
            .Include(d => d.DoctorServices)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (doctor == null)
        {
            return NotFound();
        }

        var model = new EditDoctorViewModel
        {
            Id = doctor.Id,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Specialization = doctor.Specialization,
            Bio = doctor.Bio,
            ImageUrl = doctor.ImageUrl,
            Email = doctor.Email,
            Phone = doctor.Phone,
            ConsultationFee = doctor.ConsultationFee,
            YearsOfExperience = doctor.YearsOfExperience,
            IsAvailable = doctor.IsAvailable,
            DepartmentId = doctor.DepartmentId,
            SelectedServiceIds = doctor.DoctorServices.Select(ds => ds.ServiceId).ToList(),
            Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync(),
            AvailableServices = await _context.MedicalServices.Where(s => s.IsActive).OrderBy(s => s.Name).ToListAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditDoctor(EditDoctorViewModel model)
    {
        if (ModelState.IsValid)
        {
            var doctor = await _context.Doctors
                .Include(d => d.DoctorServices)
                .FirstOrDefaultAsync(d => d.Id == model.Id);

            if (doctor == null)
            {
                return NotFound();
            }

            doctor.FirstName = model.FirstName;
            doctor.LastName = model.LastName;
            doctor.Specialization = model.Specialization;
            doctor.Bio = model.Bio;
            doctor.ImageUrl = model.ImageUrl;
            doctor.Email = model.Email;
            doctor.Phone = model.Phone;
            doctor.ConsultationFee = model.ConsultationFee;
            doctor.YearsOfExperience = model.YearsOfExperience;
            doctor.IsAvailable = model.IsAvailable;
            doctor.DepartmentId = model.DepartmentId;

            // Update services
            _context.DoctorServices.RemoveRange(doctor.DoctorServices);
            if (model.SelectedServiceIds.Any())
            {
                foreach (var serviceId in model.SelectedServiceIds)
                {
                    _context.DoctorServices.Add(new DoctorService
                    {
                        DoctorId = doctor.Id,
                        ServiceId = serviceId
                    });
                }
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Médecin mis à jour avec succès.";
            return RedirectToAction(nameof(Doctors));
        }

        model.Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync();
        model.AvailableServices = await _context.MedicalServices.Where(s => s.IsActive).OrderBy(s => s.Name).ToListAsync();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        var doctor = await _context.Doctors
            .Include(d => d.DoctorServices)
            .FirstOrDefaultAsync(d => d.Id == id);
            
        if (doctor == null)
        {
            return NotFound();
        }

        var hasAppointments = await _context.Appointments.AnyAsync(a => a.DoctorId == id);
        if (hasAppointments)
        {
            TempData["Error"] = "Impossible de supprimer ce médecin car il a des rendez-vous associés.";
            return RedirectToAction(nameof(Doctors));
        }

        // Remove doctor services first
        _context.DoctorServices.RemoveRange(doctor.DoctorServices);
        
        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Médecin supprimé avec succès.";
        return RedirectToAction(nameof(Doctors));
    }

    #endregion

    #region Service Management

    public async Task<IActionResult> Services(string? search, int? departmentId)
    {
        var query = _context.MedicalServices.Include(s => s.Department).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(s => s.Name.ToLower().Contains(search));
        }

        if (departmentId.HasValue)
        {
            query = query.Where(s => s.DepartmentId == departmentId.Value);
        }

        var model = new AdminServiceListViewModel
        {
            Services = await query.OrderBy(s => s.Name).ToListAsync(),
            Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync(),
            SearchQuery = search,
            DepartmentFilter = departmentId
        };

        return View(model);
    }

    public async Task<IActionResult> CreateService()
    {
        var model = new CreateServiceViewModel
        {
            IsActive = true,
            Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateService(CreateServiceViewModel model)
    {
        if (ModelState.IsValid)
        {
            var service = new MedicalService
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                DurationMinutes = model.DurationMinutes,
                ImageUrl = model.ImageUrl,
                IsActive = true, // Always visible on user portal by default
                DepartmentId = model.DepartmentId
            };

            _context.MedicalServices.Add(service);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Service créé avec succès.";
            return RedirectToAction(nameof(Services));
        }

        model.Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync();
        return View(model);
    }

    public async Task<IActionResult> EditService(int id)
    {
        var service = await _context.MedicalServices.FindAsync(id);
        if (service == null)
        {
            return NotFound();
        }

        var model = new EditServiceViewModel
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description,
            Price = service.Price,
            DurationMinutes = service.DurationMinutes,
            ImageUrl = service.ImageUrl,
            IsActive = service.IsActive,
            DepartmentId = service.DepartmentId,
            Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditService(EditServiceViewModel model)
    {
        if (ModelState.IsValid)
        {
            var service = await _context.MedicalServices.FindAsync(model.Id);
            if (service == null)
            {
                return NotFound();
            }

            service.Name = model.Name;
            service.Description = model.Description;
            service.Price = model.Price;
            service.DurationMinutes = model.DurationMinutes;
            service.ImageUrl = model.ImageUrl;
            service.IsActive = model.IsActive;
            service.DepartmentId = model.DepartmentId;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Service mis à jour avec succès.";
            return RedirectToAction(nameof(Services));
        }

        model.Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteService(int id)
    {
        var service = await _context.MedicalServices
            .Include(s => s.DoctorServices)
            .FirstOrDefaultAsync(s => s.Id == id);
            
        if (service == null)
        {
            return NotFound();
        }

        var hasOrders = await _context.OrderItems.AnyAsync(oi => oi.ServiceId == id);
        var hasAppointments = await _context.Appointments.AnyAsync(a => a.ServiceId == id);
        
        if (hasOrders || hasAppointments)
        {
            TempData["Error"] = "Impossible de supprimer ce service car il a des commandes ou rendez-vous associés.";
            return RedirectToAction(nameof(Services));
        }

        // Remove doctor services first
        _context.DoctorServices.RemoveRange(service.DoctorServices);
        
        _context.MedicalServices.Remove(service);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Service supprimé avec succès.";
        return RedirectToAction(nameof(Services));
    }

    #endregion

    #region Department Management

    public async Task<IActionResult> Departments(string? search)
    {
        var query = _context.Departments
            .Include(d => d.Doctors)
            .Include(d => d.Services)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(d => d.Name.ToLower().Contains(search));
        }

        var model = new AdminDepartmentListViewModel
        {
            Departments = await query.OrderBy(d => d.Name).ToListAsync(),
            SearchQuery = search
        };

        return View(model);
    }

    public IActionResult CreateDepartment()
    {
        return View(new CreateDepartmentViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDepartment(CreateDepartmentViewModel model)
    {
        if (ModelState.IsValid)
        {
            var department = new Department
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Département créé avec succès.";
            return RedirectToAction(nameof(Departments));
        }

        return View(model);
    }

    public async Task<IActionResult> EditDepartment(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        var model = new EditDepartmentViewModel
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
            ImageUrl = department.ImageUrl
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditDepartment(EditDepartmentViewModel model)
    {
        if (ModelState.IsValid)
        {
            var department = await _context.Departments.FindAsync(model.Id);
            if (department == null)
            {
                return NotFound();
            }

            department.Name = model.Name;
            department.Description = model.Description;
            department.ImageUrl = model.ImageUrl;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Département mis à jour avec succès.";
            return RedirectToAction(nameof(Departments));
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        var hasDoctors = await _context.Doctors.AnyAsync(d => d.DepartmentId == id);
        var hasServices = await _context.MedicalServices.AnyAsync(s => s.DepartmentId == id);

        if (hasDoctors || hasServices)
        {
            TempData["Error"] = "Impossible de supprimer ce département car il a des médecins ou services associés.";
            return RedirectToAction(nameof(Departments));
        }

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Département supprimé avec succès.";
        return RedirectToAction(nameof(Departments));
    }

    #endregion

    #region Appointment Management

    public async Task<IActionResult> Appointments(AppointmentStatus? status, int? doctorId, DateTime? dateFrom, DateTime? dateTo, string? search)
    {
        var query = _context.Appointments
            .Include(a => a.User)
            .Include(a => a.Doctor)
            .Include(a => a.Service)
            .AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(a => a.Status == status.Value);
        }

        if (doctorId.HasValue)
        {
            query = query.Where(a => a.DoctorId == doctorId.Value);
        }

        if (dateFrom.HasValue)
        {
            query = query.Where(a => a.AppointmentDate >= dateFrom.Value);
        }

        if (dateTo.HasValue)
        {
            query = query.Where(a => a.AppointmentDate <= dateTo.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(a =>
                (a.User != null && (a.User.FirstName.ToLower().Contains(search) || a.User.LastName.ToLower().Contains(search))) ||
                (a.Doctor != null && (a.Doctor.FirstName.ToLower().Contains(search) || a.Doctor.LastName.ToLower().Contains(search))));
        }

        var model = new AdminAppointmentListViewModel
        {
            Appointments = await query.OrderByDescending(a => a.AppointmentDate).ThenBy(a => a.StartTime).ToListAsync(),
            Doctors = await _context.Doctors.OrderBy(d => d.LastName).ToListAsync(),
            StatusFilter = status,
            DoctorFilter = doctorId,
            DateFrom = dateFrom,
            DateTo = dateTo,
            SearchQuery = search
        };

        return View(model);
    }

    public async Task<IActionResult> EditAppointment(int id)
    {
        var appointment = await _context.Appointments
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (appointment == null)
        {
            return NotFound();
        }

        var model = new EditAppointmentViewModel
        {
            Id = appointment.Id,
            AppointmentDate = appointment.AppointmentDate,
            StartTime = appointment.StartTime,
            EndTime = appointment.EndTime,
            Notes = appointment.Notes,
            Status = appointment.Status,
            DoctorId = appointment.DoctorId,
            ServiceId = appointment.ServiceId,
            UserId = appointment.UserId,
            User = appointment.User,
            Doctors = await _context.Doctors.OrderBy(d => d.LastName).ToListAsync(),
            Services = await _context.MedicalServices.Where(s => s.IsActive).OrderBy(s => s.Name).ToListAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAppointment(EditAppointmentViewModel model)
    {
        if (ModelState.IsValid)
        {
            var appointment = await _context.Appointments.FindAsync(model.Id);
            if (appointment == null)
            {
                return NotFound();
            }

            // Check for overlapping appointments (excluding the current one)
            var hasOverlap = await CheckAppointmentOverlap(
                model.DoctorId, 
                model.AppointmentDate, 
                model.StartTime, 
                model.EndTime, 
                model.Id);

            if (hasOverlap)
            {
                TempData["Error"] = "Ce créneau horaire chevauche un autre rendez-vous pour ce médecin.";
                model.Doctors = await _context.Doctors.OrderBy(d => d.LastName).ToListAsync();
                model.Services = await _context.MedicalServices.Where(s => s.IsActive).OrderBy(s => s.Name).ToListAsync();
                model.User = await _userManager.FindByIdAsync(model.UserId);
                return View(model);
            }

            appointment.AppointmentDate = model.AppointmentDate;
            appointment.StartTime = model.StartTime;
            appointment.EndTime = model.EndTime;
            appointment.Notes = model.Notes;
            appointment.Status = model.Status;
            appointment.DoctorId = model.DoctorId;
            appointment.ServiceId = model.ServiceId;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Rendez-vous mis à jour avec succès.";
            return RedirectToAction(nameof(Appointments));
        }

        model.Doctors = await _context.Doctors.OrderBy(d => d.LastName).ToListAsync();
        model.Services = await _context.MedicalServices.Where(s => s.IsActive).OrderBy(s => s.Name).ToListAsync();
        model.User = await _userManager.FindByIdAsync(model.UserId);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateAppointmentStatus(int id, AppointmentStatus status)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }

        appointment.Status = status;
        await _context.SaveChangesAsync();
        TempData["Success"] = "Statut du rendez-vous mis à jour avec succès.";
        return RedirectToAction(nameof(Appointments));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Rendez-vous supprimé avec succès.";
        return RedirectToAction(nameof(Appointments));
    }

    #endregion

    #region Order Management

    public async Task<IActionResult> Orders(OrderStatus? status, PaymentStatus? paymentStatus, DateTime? dateFrom, DateTime? dateTo, string? search)
    {
        var query = _context.Orders
            .Include(o => o.User)
            .Include(o => o.Items)
            .AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(o => o.Status == status.Value);
        }

        if (paymentStatus.HasValue)
        {
            query = query.Where(o => o.PaymentStatus == paymentStatus.Value);
        }

        if (dateFrom.HasValue)
        {
            query = query.Where(o => o.CreatedAt >= dateFrom.Value);
        }

        if (dateTo.HasValue)
        {
            query = query.Where(o => o.CreatedAt <= dateTo.Value.AddDays(1));
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(o =>
                o.OrderNumber.ToLower().Contains(search) ||
                (o.User != null && (o.User.FirstName.ToLower().Contains(search) || o.User.LastName.ToLower().Contains(search))));
        }

        var model = new AdminOrderListViewModel
        {
            Orders = await query.OrderByDescending(o => o.CreatedAt).ToListAsync(),
            StatusFilter = status,
            PaymentStatusFilter = paymentStatus,
            DateFrom = dateFrom,
            DateTo = dateTo,
            SearchQuery = search
        };

        return View(model);
    }

    public async Task<IActionResult> OrderDetails(int id)
    {
        var order = await _context.Orders
            .Include(o => o.User)
            .Include(o => o.Items)
                .ThenInclude(i => i.Service)
            .Include(o => o.Appointments)
                .ThenInclude(a => a.Doctor)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            return NotFound();
        }

        var model = new EditOrderViewModel
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            UserId = order.UserId,
            User = order.User,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            PaymentStatus = order.PaymentStatus,
            Notes = order.Notes,
            CreatedAt = order.CreatedAt,
            Items = order.Items.ToList(),
            Appointments = order.Appointments.ToList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateOrderStatus(int id, OrderStatus status)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        order.Status = status;
        if (status == OrderStatus.Completed)
        {
            order.CompletedAt = DateTime.UtcNow;
        }
        await _context.SaveChangesAsync();
        TempData["Success"] = "Statut de la commande mis à jour avec succès.";
        return RedirectToAction(nameof(Orders));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdatePaymentStatus(int id, PaymentStatus paymentStatus)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        order.PaymentStatus = paymentStatus;
        await _context.SaveChangesAsync();
        TempData["Success"] = "Statut de paiement mis à jour avec succès.";
        return RedirectToAction(nameof(Orders));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .Include(o => o.Appointments)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            return NotFound();
        }

        // Remove related items and appointments
        _context.OrderItems.RemoveRange(order.Items);
        foreach (var appointment in order.Appointments)
        {
            appointment.OrderId = null;
        }
        
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Commande supprimée avec succès.";
        return RedirectToAction(nameof(Orders));
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// Checks if there's an overlapping appointment for the same doctor at the same time
    /// </summary>
    public async Task<bool> CheckAppointmentOverlap(int doctorId, DateTime date, TimeSpan startTime, TimeSpan endTime, int? excludeAppointmentId = null)
    {
        var query = _context.Appointments
            .Where(a => a.DoctorId == doctorId &&
                        a.AppointmentDate.Date == date.Date &&
                        a.Status != AppointmentStatus.Cancelled);

        if (excludeAppointmentId.HasValue)
        {
            query = query.Where(a => a.Id != excludeAppointmentId.Value);
        }

        // Check if any existing appointment overlaps with the requested time slot
        return await query.AnyAsync(a =>
            (startTime >= a.StartTime && startTime < a.EndTime) ||  // New start is within existing
            (endTime > a.StartTime && endTime <= a.EndTime) ||       // New end is within existing
            (startTime <= a.StartTime && endTime >= a.EndTime));     // New appointment encompasses existing
    }

    /// <summary>
    /// API endpoint to check for appointment overlap
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> CheckOverlap(int doctorId, DateTime date, string startTime, string endTime, int? excludeId)
    {
        if (!TimeSpan.TryParse(startTime, out var start) || !TimeSpan.TryParse(endTime, out var end))
        {
            return Json(new { hasOverlap = false, error = "Invalid time format" });
        }

        var hasOverlap = await CheckAppointmentOverlap(doctorId, date, start, end, excludeId);
        return Json(new { hasOverlap });
    }

    #endregion

    #region Quick Toggle Actions

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleDoctorAvailability(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null)
        {
            return NotFound();
        }

        doctor.IsAvailable = !doctor.IsAvailable;
        await _context.SaveChangesAsync();

        TempData["Success"] = doctor.IsAvailable 
            ? $"Dr. {doctor.FullName} est maintenant disponible." 
            : $"Dr. {doctor.FullName} est maintenant indisponible.";
        
        return RedirectToAction(nameof(Doctors));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleServiceActive(int id)
    {
        var service = await _context.MedicalServices.FindAsync(id);
        if (service == null)
        {
            return NotFound();
        }

        service.IsActive = !service.IsActive;
        await _context.SaveChangesAsync();

        TempData["Success"] = service.IsActive 
            ? $"Le service '{service.Name}' est maintenant actif." 
            : $"Le service '{service.Name}' est maintenant inactif.";
        
        return RedirectToAction(nameof(Services));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ActivateAllDoctors()
    {
        var inactiveDoctors = await _context.Doctors.Where(d => !d.IsAvailable).ToListAsync();
        foreach (var doctor in inactiveDoctors)
        {
            doctor.IsAvailable = true;
        }
        await _context.SaveChangesAsync();

        TempData["Success"] = $"{inactiveDoctors.Count} médecin(s) ont été activés.";
        return RedirectToAction(nameof(Doctors));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ActivateAllServices()
    {
        var inactiveServices = await _context.MedicalServices.Where(s => !s.IsActive).ToListAsync();
        foreach (var service in inactiveServices)
        {
            service.IsActive = true;
        }
        await _context.SaveChangesAsync();

        TempData["Success"] = $"{inactiveServices.Count} service(s) ont été activés.";
        return RedirectToAction(nameof(Services));
    }

    #endregion
}

