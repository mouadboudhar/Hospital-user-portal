using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projet_hopital.Models;

namespace projet_hopital.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<MedicalService> MedicalServices { get; set; }
    public DbSet<DoctorService> DoctorServices { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure DoctorService many-to-many relationship - use NoAction to avoid cascade conflicts
        modelBuilder.Entity<DoctorService>()
            .HasKey(ds => new { ds.DoctorId, ds.ServiceId });

        modelBuilder.Entity<DoctorService>()
            .HasOne(ds => ds.Doctor)
            .WithMany(d => d.DoctorServices)
            .HasForeignKey(ds => ds.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<DoctorService>()
            .HasOne(ds => ds.Service)
            .WithMany(s => s.DoctorServices)
            .HasForeignKey(ds => ds.ServiceId)
            .OnDelete(DeleteBehavior.NoAction);

        // Configure Basket one-to-one with User
        modelBuilder.Entity<Basket>()
            .HasOne(b => b.User)
            .WithOne(u => u.Basket)
            .HasForeignKey<Basket>(b => b.UserId);

        // Configure Order
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);

        // Configure Appointment - use NoAction to avoid cascade conflicts
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.User)
            .WithMany(u => u.Appointments)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Service)
            .WithMany()
            .HasForeignKey(a => a.ServiceId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Order)
            .WithMany(o => o.Appointments)
            .HasForeignKey(a => a.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

        // Seed Departments
        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "Cardiologie", Description = "Soins du cœur et du système cardiovasculaire", ImageUrl = "https://images.unsplash.com/photo-1628348068343-c6a848d2b6dd?w=300" },
            new Department { Id = 2, Name = "Neurologie", Description = "Traitement du cerveau et du système nerveux", ImageUrl = "https://images.unsplash.com/photo-1559757175-5700dde675bc?w=300" },
            new Department { Id = 3, Name = "Pédiatrie", Description = "Soins médicaux pour les nourrissons, enfants et adolescents", ImageUrl = "https://images.unsplash.com/photo-1581594693702-fbdc51b2763b?w=300" },
            new Department { Id = 4, Name = "Orthopédie", Description = "Traitement du système musculo-squelettique", ImageUrl = "https://images.unsplash.com/photo-1579684453423-f84349ef60b0?w=300" },
            new Department { Id = 5, Name = "Dermatologie", Description = "Soins de la peau, des cheveux et des ongles", ImageUrl = "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=300" },
            new Department { Id = 6, Name = "Médecine Générale", Description = "Soins de santé primaires et services médicaux généraux", ImageUrl = "https://images.unsplash.com/photo-1666214280557-f1b5022eb634?w=300" }
        );

        // Seed Doctors
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { Id = 1, FirstName = "Yassine", LastName = "Lechgar", Specialization = "Cardiologue", DepartmentId = 1, ConsultationFee = 450.00m, YearsOfExperience = 15, Bio = "Expert en maladies cardiovasculaires avec 15 ans d'expérience", IsAvailable = true, ImageUrl = "https://images.unsplash.com/photo-1700041785712-649e859d9909?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
            new Doctor { Id = 2, FirstName = "Amal", LastName = "Alawi", Specialization = "Neurologue", DepartmentId = 2, ConsultationFee = 375.00m, YearsOfExperience = 12, Bio = "Spécialisée dans les troubles neurologiques et la santé cérébrale", IsAvailable = true, ImageUrl = "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
            new Doctor { Id = 3, FirstName = "Anas", LastName = "Debagh", Specialization = "Pédiatre", DepartmentId = 3, ConsultationFee = 400.00m, YearsOfExperience = 10, Bio = "Dédié à la santé et au bien-être des enfants", IsAvailable = true, ImageUrl = "https://images.unsplash.com/photo-1622253692010-333f2da6031d?q=80&w=764&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
            new Doctor { Id = 4, FirstName = "Manar", LastName = "Sefawi", Specialization = "Chirurgien Orthopédiste", DepartmentId = 4, ConsultationFee = 500.00m, YearsOfExperience = 18, Bio = "Expert en chirurgie osseuse et articulaire", IsAvailable = true, ImageUrl = "https://images.unsplash.com/photo-1742436707321-33fed05590bb?q=80&w=851&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
            new Doctor { Id = 5, FirstName = "Ali", LastName = "Lakhder", Specialization = "Dermatologue", DepartmentId = 5, ConsultationFee = 420.00m, YearsOfExperience = 8, Bio = "Spécialiste des soins de la peau avec expertise dans diverses affections cutanées", IsAvailable = true, ImageUrl = "https://plus.unsplash.com/premium_photo-1723780925032-1ad5b130db77?q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
            new Doctor { Id = 6, FirstName = "Dawoud", LastName = "Mountasib", Specialization = "Médecin Généraliste", DepartmentId = 6, ConsultationFee = 380.00m, YearsOfExperience = 20, Bio = "Expert en médecine familiale offrant des soins primaires complets", IsAvailable = true, ImageUrl = "https://images.unsplash.com/photo-1582750433449-648ed127bb54?q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" }
        );

        // Seed Medical Services
        modelBuilder.Entity<MedicalService>().HasData(
            // Services de Cardiologie
            new MedicalService { Id = 1, Name = "ECG - Électrocardiogramme", Description = "Enregistre l'activité électrique du cœur", Price = 375.00m, DurationMinutes = 30, DepartmentId = 1, IsActive = true, ImageUrl = "https://www.ramsayservices.fr/sites/default/files/styles/original/public/2025-05/iStock-1464012950%20%281%29.jpg?itok=nrlgm6-M" },
            new MedicalService { Id = 2, Name = "Échocardiogramme", Description = "Imagerie par ultrasons du cœur", Price = 550.00m, DurationMinutes = 45, DepartmentId = 1, IsActive = true, ImageUrl = "https://www.medicaim.com/uploads/84ed39b0-5a46-11e8-8fc5-bf61b50ce761/echocardiogramme.jpg" },
            new MedicalService { Id = 3, Name = "Test d'Effort Cardiaque", Description = "Évalue la fonction cardiaque pendant l'activité physique", Price = 600.00m, DurationMinutes = 60, DepartmentId = 1, IsActive = true, ImageUrl = "https://sf1.topsante.com/wp-content/uploads/topsante/2023/09/maladies-coeur-choses-savoir-sur-test-effort.jpeg" },
            
            // Services de Neurologie
            new MedicalService { Id = 4, Name = "EEG - Électroencéphalogramme", Description = "Mesure l'activité électrique du cerveau", Price = 500.00m, DurationMinutes = 45, DepartmentId = 2, IsActive = true, ImageUrl = "https://bolgehospitalinternational.com/wp-content/uploads/2023/11/EEG.jpg" },
            new MedicalService { Id = 5, Name = "Examen Neurologique", Description = "Évaluation complète de la fonction du système nerveux", Price = 550.00m, DurationMinutes = 30, DepartmentId = 2, IsActive = true, ImageUrl = "https://atlantabrainandspine.com/wp-content/uploads/2022/02/iStock-1254897003.jpg" },
            
            // Services de Pédiatrie
            new MedicalService { Id = 6, Name = "Bilan de Santé Enfant", Description = "Examen de santé de routine pour les enfants", Price = 480.00m, DurationMinutes = 30, DepartmentId = 3, IsActive = true, ImageUrl = "https://www.solimut-mutuelle.fr/wp-content/uploads/2024/08/Format-670x410-1.jpg" },
            new MedicalService { Id = 7, Name = "Vaccination", Description = "Services d'immunisation pour les enfants", Price = 450.00m, DurationMinutes = 15, DepartmentId = 3, IsActive = true, ImageUrl = "https://blogs.worldbank.org/content/dam/sites/blogs/img/detail/mgr/vaccination.png" },
            
            // Services d'Orthopédie
            new MedicalService { Id = 8, Name = "Radiographie", Description = "Imagerie diagnostique pour les os et articulations", Price = 500.00m, DurationMinutes = 20, DepartmentId = 4, IsActive = true, ImageUrl = "https://images.philips.com/is/image/philipsconsumer/8801433f55664de7a17bb11400ea0b8a?extend=0,-280,0,-575&wid=1500&hei=844&fit=stretch,1&$jpglarge$" },
            new MedicalService { Id = 9, Name = "Séance de Kinésithérapie", Description = "Rééducation et exercices thérapeutiques", Price = 520.00m, DurationMinutes = 60, DepartmentId = 4, IsActive = true, ImageUrl = "https://www.centremjmedical.com/wp-content/uploads/2022/05/Medecine-Douce-Kinesitherapie-Centre-MJ-Medical-Marrakech-1.jpg" },
            
            // Services de Dermatologie
            new MedicalService { Id = 10, Name = "Examen Dermatologique", Description = "Évaluation complète de la santé de la peau", Price = 590.00m, DurationMinutes = 30, DepartmentId = 5, IsActive = true, ImageUrl = "https://images.ctfassets.net/nla4ils4bv6t/ZrA1T29UE5kaUfJhVuRjo/3907d77dae1904df0e77002f2b812aba/dermato.jpg" },
            new MedicalService { Id = 11, Name = "Traitement de l'Acné", Description = "Gestion et traitement professionnel de l'acné", Price = 550.00m, DurationMinutes = 45, DepartmentId = 5, IsActive = true, ImageUrl = "https://allure-derm.com/storage/2024/04/Acne-Treatment-by-Allure-Dermatology-in-Edinburg-TX.webp" },
            
            // Services de Médecine Générale
            new MedicalService { Id = 12, Name = "Consultation Générale", Description = "Consultation de soins primaires et évaluation de santé", Price = 560.00m, DurationMinutes = 30, DepartmentId = 6, IsActive = true, ImageUrl = "https://www.ecotowndiagnostics.com/wp-content/uploads/2024/06/818dce83d6.jpg" },
            new MedicalService { Id = 13, Name = "Bilan de Santé Annuel", Description = "Examen de santé annuel complet", Price = 200.00m, DurationMinutes = 600, DepartmentId = 6, IsActive = true, ImageUrl = "https://myhealthassistance.ma/wp-content/uploads/2024/04/BandeauBilanSante-1_0.jpg" },
            new MedicalService { Id = 14, Name = "Bilan Sanguin Complet", Description = "Numération formule sanguine complète et panel chimique", Price = 550.00m, DurationMinutes = 15, DepartmentId = 6, IsActive = true, ImageUrl = "https://cdn8.futura-sciences.com/a1280/images/prise%20de%20sang.jpg" }
        );

        // Seed DoctorServices (link doctors to services they can provide)
        modelBuilder.Entity<DoctorService>().HasData(
            new DoctorService { DoctorId = 1, ServiceId = 1 },
            new DoctorService { DoctorId = 1, ServiceId = 2 },
            new DoctorService { DoctorId = 1, ServiceId = 3 },
            new DoctorService { DoctorId = 2, ServiceId = 4 },
            new DoctorService { DoctorId = 2, ServiceId = 5 },
            new DoctorService { DoctorId = 3, ServiceId = 6 },
            new DoctorService { DoctorId = 3, ServiceId = 7 },
            new DoctorService { DoctorId = 4, ServiceId = 8 },
            new DoctorService { DoctorId = 4, ServiceId = 9 },
            new DoctorService { DoctorId = 5, ServiceId = 10 },
            new DoctorService { DoctorId = 5, ServiceId = 11 },
            new DoctorService { DoctorId = 6, ServiceId = 12 },
            new DoctorService { DoctorId = 6, ServiceId = 13 },
            new DoctorService { DoctorId = 6, ServiceId = 14 }
        );
    }
}

