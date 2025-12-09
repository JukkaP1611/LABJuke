# Cycling Trip Management System

A comprehensive web application for managing cycling trips across Europe, built for Adventure Partners Oy.

## Features

- **Landing Page**: Showcases upcoming cycling trips with detailed information
- **Trip Catalog**: Browse all available cycling adventures with filtering
- **Trip Details**: Comprehensive information about each trip including:
  - Route details (distance, vertical meters)
  - Accommodation information
  - Pricing (base price and single room supplement)
  - Strava route links and GPX file downloads
- **Registration System**: Easy-to-use registration form collecting:
  - Personal information (name, birthday, contact details)
  - Strava account link
  - Special dietary requirements
  - Single room preference
- **RESTful API**: Complete API for managing trips, participants, and registrations
- **Database**: SQLite database with Entity Framework Core
- **Authentication Ready**: Configured for Microsoft Entra ID (Azure AD) integration

## Technology Stack

- **Backend**: ASP.NET Core 10.0 with Blazor Server
- **Database**: SQLite with Entity Framework Core 9.0
- **Frontend**: Blazor Components with Bootstrap 5
- **Authentication**: Microsoft Identity Web (ready for Entra ID)
- **API**: RESTful controllers for CRUD operations

## Getting Started

### Prerequisites

- .NET 10.0 SDK or later
- Visual Studio 2022, VS Code, or JetBrains Rider (optional)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/JukkaP1611/LABJuke.git
   cd LABJuke
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the application:
   ```bash
   dotnet build
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Open your browser and navigate to:
   - https://localhost:5001 (or the URL shown in the console)

### Database

The application uses SQLite and the database is automatically created on first run with sample data including:
- Alpine Adventure - Dolomites trip
- French Alps Explorer trip

The database file (`cyclingtrips.db`) is created in the project root directory.

## API Endpoints

### Trips
- `GET /api/trips` - Get all active trips
- `GET /api/trips/{id}` - Get a specific trip
- `POST /api/trips` - Create a new trip
- `PUT /api/trips/{id}` - Update a trip
- `DELETE /api/trips/{id}` - Soft delete a trip

### Participants
- `GET /api/participants` - Get all participants
- `GET /api/participants/{id}` - Get a specific participant
- `POST /api/participants` - Create a new participant
- `PUT /api/participants/{id}` - Update a participant

### Registrations
- `GET /api/registrations` - Get all registrations
- `GET /api/registrations/{id}` - Get a specific registration
- `POST /api/registrations` - Create a new registration
- `PUT /api/registrations/{id}` - Update a registration
- `DELETE /api/registrations/{id}` - Cancel a registration

## Configuration

### Microsoft Entra ID (Azure AD) Setup

To enable authentication with Microsoft Entra ID:

1. Register an application in Azure Portal
2. Update `appsettings.json` with your Azure AD credentials:
   ```json
   "AzureAd": {
     "Instance": "https://login.microsoftonline.com/",
     "Domain": "your-domain.onmicrosoft.com",
     "TenantId": "your-tenant-id",
     "ClientId": "your-client-id",
     "ClientSecret": "your-client-secret",
     "CallbackPath": "/signin-oidc"
   }
   ```

3. Uncomment the authentication code in `Program.cs`

### Database Connection

To use a different database (SQL Server, PostgreSQL, etc.):

1. Update the connection string in `appsettings.json`
2. Change the database provider in `Program.cs`
3. Run migrations:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

## Project Structure

```
LABJuke/
├── Components/
│   ├── Layout/          # Layout components (navigation, etc.)
│   └── Pages/           # Blazor pages
│       ├── Home.razor           # Landing page
│       ├── Trips.razor          # Trip catalog
│       ├── TripDetails.razor    # Individual trip details
│       └── Register.razor       # Registration form
├── Controllers/         # API controllers
│   ├── TripsController.cs
│   ├── ParticipantsController.cs
│   └── RegistrationsController.cs
├── Data/               # Database context
│   └── ApplicationDbContext.cs
├── Models/             # Data models
│   ├── Trip.cs
│   ├── Participant.cs
│   ├── TripRegistration.cs
│   └── Hotel.cs
├── Program.cs          # Application entry point
└── appsettings.json    # Configuration
```

## Data Models

### Participant
- First Name, Last Name
- Birthday
- Email Address
- Phone Number
- Strava Account Link (optional)
- Special Diets (optional)
- Single Room Request

### Trip
- Name, Description
- Start/End Dates
- Location
- Daily Distance & Vertical Meters
- Pricing (base + single room supplement)
- Strava/GPX links
- Max Participants

### Hotel
- Name, Address
- City, Country
- Contact Information
- Night Number (which night of the trip)

### Trip Registration
- Links Participant to Trip
- Registration Date
- Status (Pending/Confirmed/Cancelled)
- Single Room Request
- Total Price

## Development

### Adding New Trips

Use the API or directly add data to the database. Sample trips are automatically seeded on first run.

### Customization

- Modify `Components/Layout/MainLayout.razor` to change the overall layout
- Update styles in `wwwroot/css` directory
- Customize colors in Bootstrap theme

## Security Notes

⚠️ **Important**: The Microsoft.Identity.Web packages have known vulnerabilities (GHSA-rpq8-q44m-2rpg). These are moderate severity issues related to token validation. Since authentication is currently commented out and optional, this doesn't affect the current deployment. When enabling authentication in production:

1. Update to the latest patched versions
2. Review and implement proper token validation
3. Follow Microsoft's security best practices

## Future Enhancements

- [ ] Add user authentication with Entra ID
- [ ] Implement payment processing
- [ ] Add email notifications
- [ ] Create admin dashboard
- [ ] Add trip reviews and ratings
- [ ] Integrate real-time GPS tracking
- [ ] Mobile application

## License

This project is created for Adventure Partners Oy.

## Support

For questions or issues, please create an issue in the GitHub repository.