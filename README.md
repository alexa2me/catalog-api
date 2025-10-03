# API Catalog

![.NET 8.0](https://img.shields.io/badge/.NET-8.0-blueviolet?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-Programming%20Language-239120?logo=csharp)
![MySQL](https://img.shields.io/badge/MySQL-Database-4479A1?logo=mysql)
![Docker](https://img.shields.io/badge/Docker-Container-2496ED?logo=docker)
![Railway](https://img.shields.io/badge/API%20Hosted%20on-Railway-0B0D0E?logo=railway)
![SonarCloud](https://img.shields.io/badge/Code%20Quality-SonarCloud-4E9BCD?logo=sonarcloud)
![CodeQL](https://img.shields.io/badge/Security-CodeQL-2EA44F?logo=github)


## Overview

APICatalog is a RESTful API built with ASP.NET Core (.NET 8.0) and C#.  
It provides endpoints to manage products and categories, using a MySQL database hosted on [Aiven](https://aiven.io/) and the API itself is deployed on [Railway](https://railway.app/).

## Features

- CRUD operations for Products and Categories
- Entity Framework Core for data access
- MySQL database (Aiven)
- Docker support
- Deployed on Railway
- Code quality analysis with SonarCloud
- Security analysis with CodeQL

## Technologies

- .NET 8.0 / ASP.NET Core
- C#
- Entity Framework Core
- MySQL (Aiven)
- Docker
- Railway
- SonarCloud
- CodeQL

## Getting Started

1. **Clone the repository:**
   ```bash
   git clone https://github.com/your-username/APICatalog.git
   cd APICatalog
   ```
2. **Set up environment variables:**
- Create a .env file with your MySQL connection details.

3. **Build and run with Docker:**
    ```bash
    docker build -t apicatalog .
    docker run --env-file .env -p 8080:8080 apicatalog
    ```
4. Access the API:
- Visit http://localhost:8080/swagger for Swagger UI (if running in Development mode).

## Database
- Type: MySQL
- Host: Aiven
  
## Deployment
- API Host: Railway

## Code Quality & Security
- SonarCloud: Static code analysis for code quality and maintainability.
- CodeQL: Automated security analysis for code vulnerabilities.
