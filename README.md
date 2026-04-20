# 🛒 ECommerce Web API

A production-ready **ASP.NET Core 10** RESTful Web API for an e-commerce platform, built with **Clean Architecture** principles. Features JWT authentication, OAuth2 (Google), Redis caching, Stripe & Fawaterak payment integration, and full Docker support.

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Architecture](#-architecture)
- [Project Structure](#-project-structure)
- [Tech Stack](#-tech-stack)
- [Key Features](#-key-features)
- [Design Patterns](#-design-patterns)
- [API Endpoints](#-api-endpoints)
- [Getting Started](#-getting-started)
  - [Prerequisites](#prerequisites)
  - [Run with Docker (Recommended)](#run-with-docker-recommended)
  - [Run Locally](#run-locally)
- [Configuration](#-configuration)
- [Architecture Diagrams](#-architecture-diagrams)
- [Database Schema](#-database-schema)

---

## 🧭 Overview

This project is a full-featured e-commerce REST API designed as a learning and revision resource for modern backend development patterns. It covers everything from clean layered architecture to Docker orchestration, from JWT refresh tokens to payment webhooks.

| Property | Value |
|---|---|
| Framework | ASP.NET Core 10 |
| Language | C# |
| Database | SQL Server 2022 |
| Cache | Redis 7 |
| Auth | JWT + Refresh Tokens + Google OAuth2 |
| Payments | Stripe + Fawaterak |
| Containerized | Yes — Docker + Docker Compose |
| API Docs | Swagger / OpenAPI |

---

## 🏛️ Architecture

The solution follows **Clean Architecture** (also known as Onion Architecture), strictly separating concerns into layers that depend only inward — never outward.

```
┌─────────────────────────────────────────────┐
│              ECommerce.Web                  │  ← Entry point, DI wiring, Middleware
├─────────────────────────────────────────────┤
│           ECommerce.Presentation            │  ← Controllers, Attributes
├─────────────────────────────────────────────┤
│             ECommerce.Service               │  ← Business logic implementation
│      ECommerce.Services.Abstraction         │  ← Service interfaces (contracts)
├─────────────────────────────────────────────┤
│           ECommerce.Persistence             │  ← EF Core, Repositories, Migrations
├─────────────────────────────────────────────┤
│             ECommerce.Domain                │  ← Entities, Domain Interfaces
├─────────────────────────────────────────────┤
│          ECommerce.SharedLibrary            │  ← DTOs, Shared models, Result pattern
└─────────────────────────────────────────────┘
```

**Dependency Rule:** Outer layers depend on inner layers. The Domain layer has zero dependencies on any other project.

---

## 📁 Project Structure

```
ECommerceSolution/
│
├── ECommerce.Domain/                  # Core domain — no external dependencies
│   ├── Entities/
│   │   ├── BaseEntity.cs             # Shared base with Id property
│   │   ├── ProductModule/            # Product, Category, ProductBrand
│   │   ├── OrderModule/              # Order, OrderItem, DeliveryMethod
│   │   ├── BasketModule/             # CustomerBasket, BasketItem
│   │   └── IdentityModule/           # AppUser (extends IdentityUser)
│   └── Interfaces/                   # IGenericRepository, IUnitOfWork, IBasketRepository, ISpecification
│
├── ECommerce.Persistence/             # Data access layer
│   ├── Data/                         # EF Core DbContext, Migrations, Seeding
│   ├── Repositories/                 # GenericRepository, BasketRepository
│   └── Specifications/               # Specification pattern implementations
│
├── ECommerce.Services.Abstraction/    # Service interface contracts
│   └── IProductService, IOrderService, IBasketService, IAuthService, IPaymentService, etc.
│
├── ECommerce.Service/                 # Business logic implementations
│   └── ProductService, OrderService, BasketService, AuthService, PaymentService, etc.
│
├── ECommerce.Presentation/            # API layer
│   ├── Controllers/
│   │   ├── ApiBaseController.cs      # Shared base controller with Result → HTTP mapping
│   │   ├── ProductsController.cs     # GET /api/products (paginated, filtered, sorted)
│   │   ├── AuthenticationController.cs # Register, Login, Refresh, Google OAuth
│   │   ├── BasketController.cs       # GET/POST/DELETE basket
│   │   ├── OrderController.cs        # Create order, get orders
│   │   ├── PaymentController.cs      # Stripe checkout, webhook handler
│   │   └── UploadController.cs       # File/image upload
│   └── Attributes/                   # Custom action filter attributes
│
├── ECommerce.SharedLibrary/           # Cross-cutting shared models
│   └── DTOs, Result<T>, Error types, Pagination models
│
├── ECommerce.Web/                     # Application host / entry point
│   ├── Program.cs                    # Service registration + middleware pipeline
│   ├── Extensions/                   # IServiceCollection extension methods
│   ├── CustomMiddleWares/            # Global exception handler, etc.
│   ├── Factories/                    # Custom problem details factory
│   ├── appsettings.json              # Base configuration
│   ├── appsettings.Development.json  # Development overrides
│   └── appsettings.Docker.json       # Docker-specific connection strings
│
├── Dockerfile                        # Multi-stage Docker build
├── docker-compose.yml                # Orchestrates API + SQL Server + Redis
└── *.excalidraw.png                  # Architecture & flow diagrams
```

---

## 🛠️ Tech Stack

| Category | Technology |
|---|---|
| Runtime | .NET 10 / ASP.NET Core 10 |
| ORM | Entity Framework Core 10 |
| Database | Microsoft SQL Server 2022 |
| Caching | Redis 7 (StackExchange.Redis) |
| Authentication | ASP.NET Core Identity + JWT Bearer |
| Social Login | Google OAuth2 (via ASP.NET Core OAuth) |
| Payments | Stripe + Fawaterak |
| API Docs | Swashbuckle (Swagger / OpenAPI) |
| Containerization | Docker + Docker Compose |
| Mapping | AutoMapper |
| Validation | Data Annotations + FluentValidation |

---

## ✨ Key Features

- **Clean Architecture** — strict separation between Domain, Persistence, Service, and Presentation layers
- **JWT Authentication** with access tokens + refresh token rotation
- **Google OAuth2** social login flow
- **Role-based Authorization** — `Admin` and `User` roles
- **Generic Repository + Unit of Work** — reusable, testable data access
- **Specification Pattern** — composable, reusable query logic without leaking EF Core into services
- **Result Pattern** — typed `Result<T>` / `Error` returns instead of raw exceptions across all service methods
- **Redis Basket** — shopping cart stored in Redis (TTL-based, per-user)
- **Response Caching** — product listing cached in Redis to reduce DB load
- **Stripe Webhooks** — payment confirmation via Stripe webhook events
- **Fawaterak Integration** — Egyptian payment gateway support
- **File Upload** — product image upload endpoint
- **Custom Problem Details** — consistent RFC 7807 error responses across all endpoints
- **Global Exception Middleware** — catches unhandled exceptions, returns structured errors
- **Docker Compose** — one command to spin up API + SQL Server + Redis, with health checks and dependency ordering
- **Multi-stage Dockerfile** — small production image using `dotnet publish`
- **Auto DB Migration** — EF Core migrations applied automatically on startup

---

## 🧩 Design Patterns

### Repository + Unit of Work

All database operations go through a `IGenericRepository<T>` backed by EF Core. The `IUnitOfWork` aggregates all repositories and commits changes atomically.

```
IUnitOfWork
  └── Repository<Product>
  └── Repository<Order>
  └── Repository<DeliveryMethod>
```

### Specification Pattern

Query logic (filtering, sorting, pagination, includes) is encapsulated in Specification classes rather than scattered across service methods.

```csharp
// Example — products with brand + category, filtered & paginated
var spec = new ProductWithFiltersAndCountSpecification(productParams);
var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
```

See the `Specification design pattern.excalidraw.png` diagram in the repo root for a visual explanation.

### Result Pattern

Services never throw exceptions for expected failures. They return `Result<T>` which wraps either a success value or a typed `Error`.

```csharp
// Service layer
public async Task<Result<ProductDto>> GetProductByIdAsync(int id)
{
    var product = await _repo.GetByIdAsync(id);
    if (product is null)
        return Result.Failure<ProductDto>(ProductErrors.NotFound(id));
    return Result.Success(_mapper.Map<ProductDto>(product));
}

// Controller layer — ApiBaseController maps Result → HTTP response automatically
var result = await _productService.GetProductByIdAsync(id);
return result.Match(Ok, Problem);
```

See `ResultPattern.excalidraw.png` for the full flow diagram.

---

## 🌐 API Endpoints

### Authentication — `/api/auth`

| Method | Endpoint | Auth Required | Description |
|---|---|---|---|
| `POST` | `/register` | ❌ | Create a new user account |
| `POST` | `/login` | ❌ | Login, returns access + refresh tokens |
| `POST` | `/refresh-token` | ❌ | Exchange refresh token for new access token |
| `POST` | `/google-login` | ❌ | Login / register via Google OAuth2 |
| `GET` | `/current-user` | ✅ | Get authenticated user's profile |

### Products — `/api/products`

| Method | Endpoint | Auth Required | Description |
|---|---|---|---|
| `GET` | `/` | ❌ | Get all products (paginated, filterable, sortable) |
| `GET` | `/{id}` | ❌ | Get product by ID |
| `POST` | `/` | ✅ Admin | Create a new product |

**Query Parameters for `GET /api/products`:**

| Parameter | Type | Description |
|---|---|---|
| `pageIndex` | int | Page number (default: 1) |
| `pageSize` | int | Items per page (default: 6) |
| `brandId` | int? | Filter by brand |
| `typeId` | int? | Filter by category/type |
| `sort` | string | `priceAsc`, `priceDesc`, `name` |
| `search` | string | Search by product name |

### Basket — `/api/basket`

| Method | Endpoint | Auth Required | Description |
|---|---|---|---|
| `GET` | `/{basketId}` | ❌ | Get basket by ID |
| `POST` | `/` | ❌ | Create or update basket |
| `DELETE` | `/{basketId}` | ❌ | Delete basket |

### Orders — `/api/orders`

| Method | Endpoint | Auth Required | Description |
|---|---|---|---|
| `POST` | `/` | ✅ | Create order from basket |
| `GET` | `/` | ✅ | Get all orders for the current user |
| `GET` | `/{id}` | ✅ | Get order by ID |

### Payments — `/api/payments`

| Method | Endpoint | Auth Required | Description |
|---|---|---|---|
| `POST` | `/{basketId}` | ✅ | Create or update Stripe payment intent |
| `POST` | `/webhook` | ❌ | Stripe webhook handler (payment confirmation) |

### Upload — `/api/upload`

| Method | Endpoint | Auth Required | Description |
|---|---|---|---|
| `POST` | `/` | ✅ Admin | Upload a product image |

---

## 🚀 Getting Started

### Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) — for the Docker setup (recommended)
- OR [.NET 10 SDK](https://dotnet.microsoft.com/download) + a running SQL Server + Redis instance — for local setup

---

### Run with Docker (Recommended)

This is the easiest way to run the project. A single command starts **3 containers** — SQL Server, Redis, and the API — all pre-configured and networked together.

**1. Clone the repository**
```bash
git clone https://github.com/sefffo/Web-API-Revision.git
cd Web-API-Revision
```

**2. Start all services**
```bash
docker-compose up --build
```

> The first run takes a few minutes to download base images and build the API. Subsequent runs are fast since Docker caches layers.

**3. Access the API**

| URL | Description |
|---|---|
| `http://localhost:5262/swagger` | Swagger UI (interactive API docs) |
| `http://localhost:5262/api/products` | Products endpoint |
| `localhost,1433` | SQL Server (connect via SSMS with `sa` / `YourStrong@Passw0rd`) |
| `localhost:6379` | Redis |

**4. Stop everything**
```bash
docker-compose down          # stop containers, keep database volume
docker-compose down -v       # stop containers AND wipe the database (data loss!)
```

> ⚠️ The API waits for SQL Server and Redis to pass their health checks before starting. SQL Server needs ~30 seconds to fully initialize on first boot — this is handled automatically by the `depends_on: condition: service_healthy` configuration.

---

### Run Locally

**1. Clone the repository**
```bash
git clone https://github.com/sefffo/Web-API-Revision.git
cd Web-API-Revision
```

**2. Start SQL Server and Redis (via Docker, easiest)**
```bash
# SQL Server
docker run -e "SA_PASSWORD=YourStrong@Passw0rd" -e "ACCEPT_EULA=Y" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest

# Redis
docker run -p 6379:6379 -d redis:7-alpine
```

**3. Update `appsettings.json` if needed**

Edit `ECommerce.Web/appsettings.json` to point to your local SQL Server and Redis instances.

**4. Run the API**
```bash
cd ECommerce.Web
dotnet run
```

> EF Core migrations are applied automatically on startup. The databases and all tables will be created on the first run.

---

## ⚙️ Configuration

### Key sections in `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=ECommRevDb;...",
    "IdentityConnection": "Server=...;Database=ECommRevIdentityDb;...",
    "RedisConnection": "localhost:6379"
  },
  "JwtOptions": {
    "Issuer": "https://localhost:7261/",
    "Audience": "https://localhost:7261/api",
    "securityKey": "your-secret-key-min-32-chars",
    "TokenExpirationInDays": 1,
    "RefreshTokenExpirationInDays": 14
  },
  "Authentication": {
    "Google": {
      "ClientId": "your-google-client-id",
      "ClientSecret": "your-google-client-secret"
    }
  },
  "Stripe": {
    "PublishableKey": "pk_test_...",
    "SecretKey": "sk_test_...",
    "WebhookSecret": "whsec_..."
  }
}
```

### Environment-Specific Files

| File | Purpose |
|---|---|
| `appsettings.json` | Base configuration shared across all environments |
| `appsettings.Development.json` | Local development overrides (e.g., detailed logging) |
| `appsettings.Docker.json` | Docker-specific overrides — uses service names (`sqlserver`, `redis`) instead of `localhost` |

> In Docker, connection strings use **Docker service names** as hostnames (e.g., `Server=sqlserver,1433`). Docker's internal DNS resolves these names to the correct container IPs inside the `ecomm_network` bridge network.

---

## 🗂️ Architecture Diagrams

The repository root contains Excalidraw diagrams visually explaining the key concepts used in this project:

| Diagram File | Concept Explained |
|---|---|
| `JWT Auth.excalidraw.png` | JWT access token flow — how tokens are issued and validated |
| `ExplainningRefreshToken.excalidraw.png` | Refresh token rotation — how token renewal works securely |
| `Identity Module.excalidraw.png` | ASP.NET Core Identity setup and user management |
| `OAuth Steps.excalidraw.png` | Google OAuth2 authorization code flow step by step |
| `ResultPattern.excalidraw.png` | Result pattern — typed error returns replacing exceptions in service layer |
| `Specification design pattern.excalidraw.png` | Specification pattern — composable, encapsulated query logic |
| `Adding Create Product Endpoint for admin.excalidraw.png` | Admin product creation endpoint walkthrough |

---

## 🗄️ Database Schema

The solution uses **two separate databases** for clean separation between business data and identity data.

### `ECommRevDb` — Main Database

| Table | Description |
|---|---|
| `Products` | Product catalog with name, price, description, image URL, brand, and category |
| `ProductBrands` | Brand lookup table (e.g., Nike, Apple) |
| `ProductTypes` | Category/type lookup table (e.g., Shoes, Electronics) |
| `Orders` | Customer orders with shipping address, status, and chosen delivery method |
| `OrderItems` | Line items inside an order — product snapshot, quantity, and unit price |
| `DeliveryMethods` | Available shipping options with names, descriptions, and prices |

### `ECommRevIdentityDb` — Identity Database

| Table | Description |
|---|---|
| `AspNetUsers` | Extended `IdentityUser` — includes `DisplayName`, `RefreshToken`, `RefreshTokenExpiry` |
| `AspNetRoles` | Application roles: `Admin`, `User` |
| `AspNetUserRoles` | User ↔ Role many-to-many join table |

### Redis — Basket Storage

| Key Pattern | Type | Description |
|---|---|---|
| `basket:{basketId}` | String (JSON) | Shopping cart serialized as JSON, TTL = 30 days |

---

## 👤 Author

**Saif Lotfy** — Backend Engineer
[GitHub @sefffo](https://github.com/sefffo)
