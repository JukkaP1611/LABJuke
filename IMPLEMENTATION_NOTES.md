# Cycling Trip Management System - Implementation Notes

## Project Overview

This document provides detailed implementation notes for the Cycling Trip Management System developed for Adventure Partners Oy.

## System Architecture

### Technology Choices

1. **ASP.NET Core 10.0 with Blazor Server**
   - Modern .NET framework with latest features
   - Server-side rendering for better SEO and initial load times
   - Real-time UI updates via SignalR
   - No need for separate frontend framework

2. **SQLite Database**
   - Lightweight, file-based database
   - Perfect for development and small-to-medium deployments
   - Easy to backup and migrate
   - Can be upgraded to SQL Server/PostgreSQL if needed

3. **Entity Framework Core 9.0**
   - Code-first approach with migrations
   - LINQ query support
   - Automatic schema management
   - Built-in change tracking

## Data Model Design

### Participant Entity
Stores all required participant information:
- Personal details (name, birthday, contact)
- Strava integration (optional link to athlete profile)
- Dietary preferences for meal planning
- Single room preference flag

### Trip Entity
Comprehensive trip information:
- Basic details (name, description, location)
- Schedule (start/end dates, duration)
- Cycling specifics (daily distance, elevation gain)
- Pricing structure (base + single room supplement)
- Capacity management (max participants)
- Strava/GPX integration for route data

### TripRegistration Entity
Junction table with additional data:
- Links participants to trips (many-to-many)
- Tracks registration status (Pending/Confirmed/Cancelled)
- Stores final price calculation
- Records registration timestamp

### Hotel Entity
Accommodation management:
- Associated with specific trips
- Tracks nightly progression
- Contact and location information
- Supports multi-hotel itineraries

## Business Logic Implementation

### Registration Process
1. Validate trip availability (check max participants)
2. Create participant record
3. Calculate total price:
   - Base price from trip
   - Add single room supplement if requested
4. Create registration with status "Pending"
5. Return confirmation

### Price Calculation
- Base price: Twin room accommodation
- Single room: Base + Supplement
- Transparent pricing displayed throughout UI

## UI/UX Design Decisions

### Landing Page
- Hero section with gradient background
- Eye-catching call-to-action
- Featured trips in card layout
- Quick access to details and registration

### Trip Details Page
- Comprehensive trip overview
- Side-by-side layout (details + pricing)
- Sticky pricing card for easy access
- Clear call-to-action for registration

### Registration Form
- Progressive disclosure (show relevant fields)
- Real-time validation feedback
- Price summary updates dynamically
- Clear required field indicators
- Terms acceptance requirement

## API Design

### RESTful Principles
- Standard HTTP methods (GET, POST, PUT, DELETE)
- JSON responses
- Consistent route patterns
- Proper status codes

### Endpoint Structure
```
/api/trips          - Trip management
/api/participants   - Participant CRUD
/api/registrations  - Registration handling
```

## Security Considerations

### Current State
- Authentication infrastructure ready but disabled
- Allows flexible deployment without Azure AD setup
- All packages configured and tested

### Production Recommendations
1. Enable Microsoft Entra ID authentication
2. Add authorization policies:
   - Public: View trips and register
   - Authenticated: Manage own registrations
   - Admin: Full CRUD on all entities
3. Implement CSRF protection (already present)
4. Add rate limiting for API endpoints
5. Enable HTTPS enforcement
6. Implement input sanitization
7. Add SQL injection protection (EF Core provides this)

### Known Vulnerabilities
- Microsoft.Identity.Web 3.6.1 has GHSA-rpq8-q44m-2rpg
- Impact: Token validation weakness
- Mitigation: Authentication currently disabled
- Action: Update before enabling auth in production

## Database Seeding

### Sample Data
Two complete trips provided:
1. **Alpine Adventure - Dolomites**
   - 7 days, June 2026
   - 100km/day, 2500m elevation
   - €2500 base price
   
2. **French Alps Explorer**
   - 6 days, July 2026
   - 105km/day, 2600m elevation
   - €2800 base price

### Purpose
- Demonstrates system capabilities
- Provides test data for development
- Shows data model relationships
- Enables immediate testing

## Development Workflow

### Local Development
```bash
# Clone and setup
git clone <repo-url>
cd LABJuke
dotnet restore

# Run application
dotnet run

# Database is auto-created on first run
```

### Adding Migrations (if data model changes)
```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

## Deployment Considerations

### Environment Configuration
- `appsettings.json` - Base configuration
- `appsettings.Development.json` - Dev overrides
- `appsettings.Production.json` - Prod settings (create as needed)

### Azure Deployment Options
1. **Azure App Service**
   - Easy deployment from GitHub
   - Auto-scaling capabilities
   - Built-in SSL certificates

2. **Azure SQL Database**
   - Upgrade from SQLite
   - Better performance at scale
   - Automated backups

3. **Azure Entra ID**
   - Enterprise authentication
   - Single sign-on
   - Multi-factor authentication

### Configuration Steps for Production
1. Update connection string to cloud database
2. Configure Azure AD credentials
3. Uncomment authentication in Program.cs
4. Set up email service for notifications
5. Configure file storage for GPX files
6. Set up application insights for monitoring

## Future Enhancements

### Phase 2 Features
1. **Payment Integration**
   - Stripe or PayPal integration
   - Secure payment processing
   - Automatic confirmation emails

2. **Email Notifications**
   - Registration confirmations
   - Trip reminders
   - Updates and announcements

3. **Admin Dashboard**
   - Trip management interface
   - Participant management
   - Registration oversight
   - Reporting and analytics

4. **Advanced Features**
   - Strava API integration for live route preview
   - GPX file upload and storage
   - Interactive maps
   - Weather forecasts
   - Participant messaging
   - Trip reviews and ratings

### Scalability Improvements
- Implement caching (Redis)
- Add CDN for static assets
- Implement background jobs (Hangfire)
- Add search functionality
- Optimize database queries
- Implement pagination for large datasets

## Testing Strategy

### Current Testing
- Manual testing performed on all pages
- API endpoints validated
- Form validation verified
- Navigation flow tested

### Recommended Testing
1. **Unit Tests**
   - Business logic in controllers
   - Price calculation
   - Validation rules

2. **Integration Tests**
   - API endpoint testing
   - Database operations
   - Authentication flows

3. **UI Tests**
   - Blazor component testing
   - End-to-end scenarios
   - Cross-browser testing

## Maintenance Notes

### Regular Tasks
- Monitor application logs
- Review registration statistics
- Update trip information
- Manage participant data
- Backup database regularly

### Updates
- Keep .NET runtime updated
- Update NuGet packages regularly
- Review security advisories
- Test updates in staging environment

## Support and Documentation

### Key Resources
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Blazor Documentation](https://docs.microsoft.com/aspnet/core/blazor)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [Microsoft Identity Platform](https://docs.microsoft.com/azure/active-directory/develop)

### Getting Help
- Check README.md for setup instructions
- Review code comments for implementation details
- Consult .NET documentation for framework features
- Contact development team for custom modifications

## Conclusion

This implementation provides a solid foundation for managing cycling trips with room for growth and enhancement. The architecture is clean, maintainable, and follows .NET best practices. The system is ready for deployment with minimal additional configuration.
