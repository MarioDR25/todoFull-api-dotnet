# TodoList API

REST API built with **ASP.NET Core 9** following **Clean Architecture** principles, using **MySQL** and **JWT authentication**.

## Tech Stack

| Category | Technology |
|----------|------------|
| Runtime | .NET 9 |
| Database | MySQL |
| ORM | Entity Framework Core (Pomelo) |
| Auth | JWT Bearer + BCrypt |
| Docs | OpenAPI / Scalar |
| Patterns | Repository, Dependency Injection, DTOs |

## Project Structure

```
src/
├── TodoList.Api/             # Controllers, extensions, configuration
├── TodoList.Application/     # Services, DTOs, interfaces
├── TodoList.Domain/          # Entities, repository interfaces
└── TodoList.Infrastructure/  # DbContext, repositories, migrations
```

## API Endpoints

### Auth
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | `/api/auth/register` | No | Register new user |
| POST | `/api/auth/login` | No | Login, returns JWT |

### Users
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/users` | Yes | List all users |
| GET | `/api/users/{id}` | Yes | Get user by ID |
| PUT | `/api/users/{id}` | Yes | Update user |
| DELETE | `/api/users/{id}` | Yes | Delete user |

### Todo Items
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/todoitems` | Yes | List user's tasks |
| GET | `/api/todoitems/{id}` | Yes | Get task by ID |
| POST | `/api/todoitems` | Yes | Create task |
| PUT | `/api/todoitems/{id}` | Yes | Update task |
| DELETE | `/api/todoitems/{id}` | Yes | Delete task |

> Protected endpoints require header: `Authorization: Bearer <token>`

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [MySQL Server](https://dev.mysql.com/downloads/installer/)

### Setup

1. **Clone and configure database connection**
   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" \
     "Server=localhost;Port=3306;Database=TodoListDb;Uid=root;Pwd=your_password;"
   ```

2. **Configure JWT secrets** (optional, defaults in `appsettings.json`)
   ```bash
   dotnet user-secrets set "Jwt:Key" "your-secret-key-at-least-32-characters-long"
   ```

3. **Apply migrations**
   ```bash
   dotnet ef database update \
     --project src/TodoList.Infrastructure \
     --startup-project src/TodoList.Api
   ```

4. **Run the API**
   ```bash
   dotnet run --project src/TodoList.Api
   ```

## API Documentation

Once running, open the Scalar UI at:

```
http://localhost:5242/scalar/v1
```

Features: live endpoint testing, code snippets (C#, Python, JS), and OpenAPI spec download.
