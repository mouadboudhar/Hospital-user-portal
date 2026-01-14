using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using projet_hopital.Data;
using projet_hopital.Models;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configure cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(14);
    options.SlidingExpiration = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Create database and apply migrations with retry logic for Docker
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var context = services.GetRequiredService<ApplicationDbContext>();
    
    var maxRetries = 10;
    var delay = TimeSpan.FromSeconds(5);
    
    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            logger.LogInformation("Attempting to connect to database (attempt {Attempt}/{MaxRetries})...", i + 1, maxRetries);
            context.Database.Migrate();
            logger.LogInformation("Database migration completed successfully.");
            break;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Database connection attempt {Attempt}/{MaxRetries} failed. Retrying in {Delay} seconds...", i + 1, maxRetries, delay.TotalSeconds);
            
            if (i == maxRetries - 1)
            {
                logger.LogError(ex, "Failed to connect to database after {MaxRetries} attempts.", maxRetries);
                throw;
            }
            
            Thread.Sleep(delay);
        }
    }

    // Seed roles and admin user
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roles = { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
            logger.LogInformation("Created role: {Role}", role);
        }
    }

    // Create admin user if not exists
    var adminEmail = "admin@hopital.ma";
    var adminPassword = "Admin123!";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "Admin",
            LastName = "System",
            EmailConfirmed = true,
            CreatedAt = DateTime.UtcNow
        };
        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
            logger.LogInformation("Admin user created successfully.");
        }
        else
        {
            logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
    else
    {
        // Ensure admin has the Admin role
        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
            logger.LogInformation("Added Admin role to existing admin user.");
        }
        
        // Reset password to ensure it's correct
        var token = await userManager.GeneratePasswordResetTokenAsync(adminUser);
        var resetResult = await userManager.ResetPasswordAsync(adminUser, token, adminPassword);
        if (resetResult.Succeeded)
        {
            logger.LogInformation("Admin password reset successfully.");
        }
        else
        {
            logger.LogError("Failed to reset admin password: {Errors}", string.Join(", ", resetResult.Errors.Select(e => e.Description)));
        }
    }
    
    // Fix any doctors and services that are not visible on user portal
    var invisibleDoctors = await context.Doctors.Where(d => !d.IsAvailable).ToListAsync();
    if (invisibleDoctors.Any())
    {
        foreach (var doc in invisibleDoctors)
        {
            doc.IsAvailable = true;
        }
        await context.SaveChangesAsync();
        logger.LogInformation("Fixed {Count} doctors that were not visible.", invisibleDoctors.Count);
    }
    
    var invisibleServices = await context.MedicalServices.Where(s => !s.IsActive).ToListAsync();
    if (invisibleServices.Any())
    {
        foreach (var svc in invisibleServices)
        {
            svc.IsActive = true;
        }
        await context.SaveChangesAsync();
        logger.LogInformation("Fixed {Count} services that were not visible.", invisibleServices.Count);
    }
}

app.Run();