# 🏥 Portail Hôpital - Hospital User Portal

A comprehensive hospital user portal built with ASP.NET Core MVC, allowing users to browse medical services, view doctors, book appointments, and manage their healthcare journey.

## ✨ Features

- **User Authentication**: Secure registration and login system
- **Browse Services**: View all available medical services with pricing
- **Doctor Directory**: Find doctors by specialty and department
- **Appointment Booking**: Schedule appointments with doctors
- **Shopping Basket**: Add services and appointments to basket
- **Checkout System**: Complete orders with order history
- **Responsive Design**: Mobile-friendly interface with Bootstrap 5
- **French Localization**: Full French language support with MAD currency

## 🛠️ Technologies

- **Framework**: ASP.NET Core 9.0 MVC
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core 9.0
- **Authentication**: ASP.NET Core Identity
- **Frontend**: Bootstrap 5, Bootstrap Icons
- **Containerization**: Docker & Docker Compose

## 🚀 Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for containerized deployment)
- SQL Server (for local development)

### Local Development

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/projet-hopital.git
   cd projet-hopital
   ```

2. **Update connection string** in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Your-Connection-String-Here"
     }
   }
   ```

3. **Apply database migrations**
   ```bash
   cd projet-hopital
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

### Docker Deployment

1. **Build and run with Docker Compose**
   ```bash
   docker-compose up --build
   ```

2. Wait for the containers to start (SQL Server takes ~30-60 seconds to initialize)

3. The application will automatically:
   - Wait for SQL Server to be ready
   - Create the database
   - Apply all migrations
   - Seed initial data (departments, doctors, services)

4. The application will be available at `http://localhost:5000`

5. **Stop the containers**
   ```bash
   docker-compose down
   ```

6. **Stop and remove volumes** (clears database)
   ```bash
   docker-compose down -v
   ```

## 📁 Project Structure

```
projet-hopital/
├── Controllers/          # MVC Controllers
├── Data/                 # DbContext and data configuration
├── Migrations/           # EF Core migrations
├── Models/               # Entity models
├── ViewModels/           # View-specific models
├── Views/                # Razor views
│   ├── Account/          # Authentication views
│   ├── Appointments/     # Appointment management
│   ├── Basket/           # Shopping basket
│   ├── Doctors/          # Doctor directory
│   ├── Home/             # Home page
│   ├── Orders/           # Order management
│   ├── Services/         # Medical services
│   └── Shared/           # Layout and shared views
├── wwwroot/              # Static files (CSS, JS)
├── Dockerfile            # Docker image configuration
└── docker-compose.yml    # Multi-container orchestration
```

## 🔐 Default Configuration

When running with Docker, the application uses these default credentials:

- **SQL Server SA Password**: `HopitalPass123!`
- **Database Name**: `HopitalDB`

> ⚠️ **Important**: Change these credentials for production deployments!

## 📝 Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `ASPNETCORE_ENVIRONMENT` | Runtime environment | `Production` |
| `ConnectionStrings__DefaultConnection` | Database connection string | See docker-compose.yml |

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👤 Author

Built with ❤️ for healthcare management

