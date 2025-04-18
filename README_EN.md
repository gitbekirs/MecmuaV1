# Mecmua Blog Platform

Mecmua is a modern, feature-rich blog platform built using ASP.NET Core. It provides a comprehensive content management system for publishing and managing blog articles with a clean, responsive user interface.

## Features

- **User Management**
  - Role-based authorization (Admin, Editor, Writer, Member)
  - User registration and authentication
  - User profiles with profile pictures and bios

- **Content Management**
  - Article creation and management
  - Category and tag organization
  - Rich text editor support
  - Media uploads
  - Featured articles
  - View count tracking

- **Engagement Features**
  - Comments and moderation
  - Like functionality
  - Notifications system
  - Article search

- **Responsive UI**
  - Modern, responsive design
  - Mobile-friendly interface
  - Clean typography

## Tech Stack

- **Backend**
  - ASP.NET Core 6+
  - Entity Framework Core
  - ASP.NET Core Identity
  - SQL Server

- **Frontend**
  - HTML5, CSS3, JavaScript
  - Responsive design from Abstract template

## Prerequisites

To run this project, you need the following:

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or newer
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or Developer edition is fine)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/)

## Getting Started

### Database Setup

1. Update the connection string in `appsettings.json` to point to your SQL Server instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=MecmuaDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

2. The application will automatically create the database and seed initial data when run for the first time.

### Running the Application

#### Using Visual Studio:

1. Open the solution file (`Mecmua.sln`) in Visual Studio
2. Ensure the connection string is properly configured
3. Press F5 to build and run the project
4. The application will start and open in your default web browser

#### Using Command Line:

1. Navigate to the project directory
2. Run the following commands:

```bash
dotnet restore
dotnet build
dotnet run
```

3. Open your web browser and navigate to `https://localhost:5001` or `http://localhost:5000`

### Default Users

The application is seeded with the following default users:

| Email | Password | Role |
|-------|----------|------|
| admin@mecmua.com | Admin123. | Admin |
| editor@mecmua.com | Editor123. | Editor |
| yazar@mecmua.com | Yazar123. | Writer |
| uye@mecmua.com | Uye123. | Member |

## Project Structure

- **Controllers/**: Contains all the MVC controllers
- **Models/**: Contains the data models
- **Views/**: Contains the Razor views
- **Data/**: Contains the database context and repositories
- **wwwroot/**: Contains static files (CSS, JS, images)
- **Areas/**: Contains area-specific features like Admin panel

## Application Flow

1. Users can browse articles without logging in
2. Writers can create and submit articles
3. Editors can review and publish articles
4. Admins have full control over the platform
5. Members can comment on articles and engage with content

## Customization

### Changing Theme

The site uses CSS from the Abstract theme. You can customize the appearance by modifying:

- `wwwroot/css/theme.css`
- `wwwroot/css/site.css`
- `wwwroot/css/abstract/styles.css`

### Adding Features

The modular architecture allows for easy extension. Add new controllers and views to implement additional features.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgements

- Based on the Abstract theme
- Built with ASP.NET Core
- Uses Entity Framework Core for data access
- Leverages ASP.NET Core Identity for authentication and authorization 