# Restaurants Management System 🍽️

## Project Overview

A comprehensive Restaurant Management System built with **ASP.NET Core** using **Clean Architecture** principles. This system provides a RESTful API for managing restaurants, their dishes, and related information.

## 🏗️ Architecture

This project follows **Clean Architecture** with the following layers:

```
📁 Restaurants8/
├── 🌐 Restaurants.API/          # Web API Layer (Presentation)
├── 💼 Restaurants.Application/  # Application Logic Layer
├── 🏛️ Restaurants.Domain/       # Domain Models Layer
├── 🔧 Restaurants.Infrastructure/ # Data Access Layer
└── 🧪 Test Projects/            # Unit & Integration Tests
```

### Project Dependencies

#### 🌐 **Restaurants.API** (Web API Layer)
- **Framework**: .NET 10.0 (Preview)
- **Dependencies**:
  - `Microsoft.AspNetCore.OpenApi` (v10.0.0-preview.4.25258.110) - OpenAPI support
  - `Microsoft.EntityFrameworkCore.SqlServer` (v9.0.5) - SQL Server provider
  - `Microsoft.EntityFrameworkCore.Tools` (v9.0.5) - EF Core tooling
  - `Microsoft.Extensions.DependencyInjection` (v9.0.5) - DI container
  - `Swashbuckle.AspNetCore` (v8.1.2) - Swagger documentation
- **Project References**: Application, Infrastructure

#### 💼 **Restaurants.Application** (Application Layer)
- **Framework**: .NET 10.0
- **Dependencies**:
  - `AutoMapper.Extensions.Microsoft.DependencyInjection` (v12.0.1) - Object mapping
  - `FluentValidation.AspNetCore` (v11.3.0) - Input validation
  - `Swashbuckle.AspNetCore` (v8.1.2) - API documentation
- **Project References**: Domain, Infrastructure

#### 🔧 **Restaurants.Infrastructure** (Infrastructure Layer)
- **Framework**: .NET 10.0
- **Dependencies**:
  - `Microsoft.EntityFrameworkCore.SqlServer` (v9.0.5) - Database provider
  - `Microsoft.EntityFrameworkCore.Tools` (v9.0.5) - Migration tools
- **Project References**: Domain

#### 🏛️ **Restaurants.Domain** (Domain Layer)
- **Framework**: .NET 10.0
- **Dependencies**: None (Pure domain layer)

## 🚀 Features

### Core Functionality

#### 🍽️ **Restaurant Management**
- **Create Restaurant**: Add new restaurants with detailed information
- **Read Restaurants**: 
  - Get all restaurants
  - Get restaurant by ID with full details
- **Update Restaurant**: Modify existing restaurant information
- **Delete Restaurant**: Remove restaurants from the system

#### 📋 **Restaurant Properties**
- Basic Information:
  - Name
  - Description
  - Category
  - Delivery availability
- Contact Information:
  - Email address
  - Phone number
- Address Information:
  - Complete address details
- Menu Management:
  - Associated dishes

#### 🍕 **Dish Management**
- Dish entities linked to restaurants
- Dish properties and details
- Menu item organization

### 🔧 **Technical Features**

#### API Features
- **RESTful API** with standard HTTP verbs (GET, POST, PUT, DELETE)
- **Swagger Documentation** - Interactive API documentation
- **OpenAPI Specification** - Standardized API description
- **Model Validation** - Input validation using FluentValidation
- **Error Handling** - Comprehensive error responses
- **Dependency Injection** - Built-in DI container

#### Data Layer Features
- **Entity Framework Core** - Object-relational mapping
- **SQL Server Database** - Production-ready database
- **Code First Migrations** - Database versioning
- **Repository Pattern** - Data access abstraction

#### Application Layer Features
- **AutoMapper** - Object-to-object mapping
- **Clean Architecture** - Separation of concerns
- **CQRS-ready Structure** - Command and Query separation support
- **Validation Layer** - Business rule validation

## 🗂️ Project Structure

### Domain Entities

#### Restaurant Entity
```csharp
- Id (int)
- Name (string)
- Description (string) 
- Category (string)
- HasDelivery (bool)
- ContactEmail (string?)
- ContactNumber (string?)
- Address (Address?)
- Dishes (List<Dish>)
```

#### Supporting Entities
- **Dish**: Menu items associated with restaurants
- **Address**: Location information for restaurants

### API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/restaurants` | Get all restaurants |
| GET | `/api/restaurants/{id}` | Get restaurant by ID |
| POST | `/api/restaurants` | Create new restaurant |
| PUT | `/api/restaurants/{id}` | Update existing restaurant |
| DELETE | `/api/restaurants/{id}` | Delete restaurant |

## 🧪 Testing Status

### Test Projects Structure
- **Restaurants.API.Tests**: API layer integration tests
- **Restaurants.Application.Tests**: Application logic unit tests  
- **Restaurants.Infrastructure.Tests**: Data access layer tests

### Latest Test Results
```
Status: ⚠️ BUILDING
Notes: Project is currently building with some warnings:
- Package version conflicts detected (AutoMapper dependency)
- Preview .NET version in use
- Test execution status pending build completion
```

### Build Warnings
- **NU1510**: Unnecessary Microsoft.Extensions.DependencyInjection package reference
- **NU1608**: AutoMapper version conflict (12.0.1 vs 14.0.0)

## 🚀 Getting Started

### Prerequisites
- .NET 10.0 SDK (Preview)
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd Restaurant8
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Update database**
   ```bash
   dotnet ef database update --project Restaurants.Infrastructure
   ```

4. **Run the application**
   ```bash
   dotnet run --project Restaurants.API
   ```

5. **Access Swagger UI**
   - Navigate to: `https://localhost:7xxx/swagger`

### Configuration

Update `appsettings.json` in the API project with your database connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=RestaurantsDB;Trusted_Connection=true;"
  }
}
```

## 📈 Development Timeline

### Commit History
- **Latest**: `02825bc` - Add project files (17 minutes ago)
- **Initial**: `b39bc5c` - Add .gitattributes and .gitignore (17 minutes ago)

### Current Version
- **Version**: 1.0.0-preview
- **Last Updated**: Recent (within last hour)
- **Status**: Active development

## 🛠️ Development Tools

### Required Extensions/Tools
- **Entity Framework Core Tools** - Database migrations
- **Swagger/OpenAPI** - API documentation
- **AutoMapper** - Object mapping
- **FluentValidation** - Input validation
- **xUnit** - Testing framework (in test projects)

## 📝 Notes

- This project uses **preview versions** of .NET 10.0
- Some package version conflicts exist and should be resolved
- Test suite is in development phase
- Database migrations are set up but may need initial run

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Submit a pull request

## 📄 License

This project is in development phase. License to be determined.

---

**Last Updated**: $(Get-Date)  
**Project Status**: 🚧 In Development  
**Test Status**: ⚠️ Pending 