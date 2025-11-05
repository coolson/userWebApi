# User Web API - File Management System

A full-stack web application built with ASP.NET Core and React for managing text files through a web interface.

## Overview

This application provides a simple yet functional file management system that allows users to create, read, update, and delete text files. It also includes special functionality for numbering and formatting text content.

## Technology Stack

### Backend
- **ASP.NET Core 3.1** - Web API framework
- **C#** - Primary programming language
- **.NET Core 3.1** - Runtime environment

### Frontend
- **React 16.11.0** - UI library
- **TypeScript 3.6.4** - Type-safe JavaScript
- **Redux** - State management
- **React Router** - Navigation
- **Bootstrap & Reactstrap** - UI styling

## Features

- **File Operations**
  - Create new text files
  - Open and read existing files
  - Update file content
  - Delete files

- **Text Processing**
  - Automatic symbol/character numbering
  - Line numbering functionality
  - Text formatting capabilities

- **API Endpoints**
  - `GET /api/datafile/health` - Health check endpoint
  - `GET /api/datafile?filename={name}` - Get or create file
  - `POST /api/datafile?filename={name}` - Update file content
  - `DELETE /api/datafile?filename={name}` - Delete file
  - `POST /api/datafile/setsymbols?filename={name}` - Number symbols in file

## Project Structure

```
/workspace
├── userWebApi/                    # Main application directory
│   ├── Controllers/               # API controllers
│   │   └── DataFileController.cs  # File operations API
│   ├── Model/                     # Data models
│   │   └── UserData.cs            # User and data models
│   ├── ClientApp/                 # React frontend
│   │   ├── src/
│   │   │   ├── App.tsx            # Root component
│   │   │   ├── components/
│   │   │   │   └── Home.tsx       # Main file management UI
│   │   │   └── custom.css         # Custom styles
│   │   ├── public/                # Static assets
│   │   └── package.json           # Frontend dependencies
│   ├── Program.cs                 # Application entry point
│   ├── Startup.cs                 # Application configuration
│   └── userWebApi.csproj          # Project file
└── userWebApi.sln                 # Solution file
```

## Prerequisites

- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet/3.1)
- [Node.js](https://nodejs.org/) (v12 or higher recommended)
- npm (comes with Node.js)

## Getting Started

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd workspace
   ```

2. **Restore .NET dependencies**
   ```bash
   dotnet restore
   ```

3. **Install frontend dependencies**
   ```bash
   cd userWebApi/ClientApp
   npm install
   cd ../..
   ```

### Running the Application

#### Development Mode

1. **Start the backend server**
   ```bash
   cd userWebApi
   dotnet run
   ```

2. **In a separate terminal, start the React development server**
   ```bash
   cd userWebApi/ClientApp
   npm start
   ```

The application will be available at:
- Backend API: `https://localhost:5001`
- Frontend: `http://localhost:3000`

#### Production Build

```bash
cd userWebApi
dotnet publish -c Release
```

The built files will be in `userWebApi/bin/Release/netcoreapp3.1/publish/`

## API Usage

### Health Check
```bash
curl http://localhost:51001/api/datafile/health
```

### Create/Get File
```bash
curl "http://localhost:51001/api/datafile?filename=myfile"
```

### Update File
```bash
curl -X POST "http://localhost:51001/api/datafile?filename=myfile" \
  -H "Content-Type: application/json" \
  -d '{"fileName":"myfile","fileContent":"Hello World"}'
```

### Delete File
```bash
curl -X DELETE "http://localhost:51001/api/datafile?filename=myfile"
```

## Configuration

### Backend Configuration
Configuration files are located in the `userWebApi` directory:
- `appsettings.json` - Production settings
- `appsettings.Development.json` - Development settings

### File Storage
Files are stored in the `\temp` directory by default. This can be modified in the `DataFileController.cs` file.

## Development

### Running Tests
```bash
cd userWebApi/ClientApp
npm test
```

### Linting
```bash
cd userWebApi/ClientApp
npm run lint
```

### Building Frontend for Production
```bash
cd userWebApi/ClientApp
npm run build
```

## Architecture

The application follows these principles:
- **SOLID principles** - Clean, maintainable code structure
- **DRY (Don't Repeat Yourself)** - Reusable components and functions
- **KISS (Keep It Simple, Stupid)** - Straightforward, readable code
- **RESTful API design** - Standard HTTP methods and status codes
- **Component-based architecture** - Modular React components

## Contributing

When contributing to this project, please ensure:
1. Code follows SOLID, DRY, and KISS principles
2. Proper error handling is implemented
3. Code is well-documented
4. Tests are written for new features
5. Best practices for C# and TypeScript/React are followed

## License

This project is for educational/demonstration purposes.

## Support

For issues, questions, or contributions, please refer to the project repository.
