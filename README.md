# StoreAPI — E‑Commerce Backend API 🛒

> A clean-architecture ASP.NET Core API for a small-to-medium e-commerce system (Catalog, Basket, Orders, Identity, Payments). Designed for learning, demos, and production-ready practices.

---

## Quick overview

**E‑Commerce API** is a backend system for an online store, fully connected to a **Frontend application** and exposing RESTful endpoints consumed by the client.

The project covers real e‑commerce flows such as catalog browsing, basket management, order creation, and payment processing.

---

## Features ✅

* RESTful API consumed by a Frontend application
* Product catalog with categories and search
* Shopping basket stored in **Redis** for fast access
* Order creation from basket with validation
* **Stripe payment gateway integration** using Payment Intents
* Secure payment flow connected with the Frontend
* Authentication & Authorization (JWT + Identity)
* Admin endpoints for product & order management
* Clean architecture with clear separation of concerns

---

## Tech stack 🧰

* **Backend:** ASP.NET Core (recommended: .NET 8)
* **ORM:** Entity Framework Core
* **Database:** SQL Server (example), can adapt to Postgres/MySQL
* **Cache:** Redis
* **Auth:** ASP.NET Identity + JWT
* **Payments:** Stripe
* **Docs:** Swagger / Swashbuckle
* **Containerization:** Docker + docker-compose

---

## Repository layout

```
/src
  /Store.API            -> ASP.NET Core Web API (entry point)
  /Store.Application    -> Business logic, DTOs, interfaces
  /Store.Domain         -> Core entities and domain rules
  /Store.Infrastructure -> EF Core, Redis, Stripe integration
/tests
  /Store.UnitTests
  /Store.IntegrationTests
/README.md
/LICENSE
```

/src
/Store.API            -> ASP.NET Core Web API (entry)
/Store.Application    -> Use-cases, DTOs, interfaces
/Store.Domain         -> Entities, value objects, domain logic
/Store.Infrastructure -> EF Core, Repositories, Stripe integration
/tests
/Store.UnitTests
/Store.IntegrationTests
/docker-compose.yml
/README.md

````

---

## Getting started (local)

**Prerequisites**
- .NET SDK 8.x
- SQL Server (or Dockerized SQL) or any DB you choose
- Redis
- Docker (optional but recommended)

**1. Clone the repo**
```bash
git clone https://github.com/Alsayedkabeil/E-Commerce.git
cd E-Commerce
````

**2. Configuration**

Copy `appsettings.json` or `appsettings.Development.json` and replace the `your_...` placeholders with real values. Example environment variables / settings to set:

* `ConnectionStrings:DefaultConnection` = `Server=your_server_name;Database=your_store_database;User Id=your_db_user;Password=your_db_password;`
* `RedisConnection` = `your_redis_host:6379`
* `BaseUrl` = `https://your-domain.com`
* `JwtOptions:Issuer` / `Audience` / `SecretKey`
* `Stripe:Secretkey` = `sk_test_your_stripe_secret_key`

> **Security tip:** Never commit secrets. Use environment variables, user secrets, or a secrets manager in CI/CD.

**3. Run database migrations & seed**

```bash
# from /src/Store.Infrastructure (or the project containing DbContext)
dotnet ef database update
# (optional) run seeder to add sample data
dotnet run --project src/Store.API
```

**4. Start the API**

```bash
cd src/Store.API
dotnet run
# API should be available at https://localhost:7290 (or the BaseUrl you set)
```

Open `https://localhost:7290/swagger` to explore endpoints.

---

## Docker (development)

`docker-compose.yml` includes services for `api`, `sqlserver`, and `redis`. Example usage:

```bash
# build and run
docker-compose up --build
```

The API will wait for SQL Server to be ready, run migrations, then start.

---

## Common commands

* Run tests:

```bash
dotnet test
```

* Add EF Core migration:

```bash
dotnet ef migrations add AddXyz -p src/Store.Infrastructure -s src/Store.API
```

* Apply migrations:

```bash
dotnet ef database update -p src/Store.Infrastructure -s src/Store.API
```

---

## Payment Gateway (Stripe) 💳

The project uses **Stripe** as the payment gateway to process online payments securely.

### Payment workflow

1. The Frontend requests payment initialization.
2. The API creates a **Stripe Payment Intent**.
3. The `clientSecret` is returned to the Frontend.
4. The Frontend confirms the payment using Stripe.js.
5. The order payment status is updated in the system.

### Stripe endpoints (examples)

* `POST /api/payments/create-payment-intent`
* `POST /api/payments/webhook` *(optional)*

> Stripe keys are stored securely and never committed to source control.

---

## API documentation & testing

* **Swagger UI:** `https://{BaseUrl}/swagger`
* **Postman:** A Postman collection is available to test all API endpoints (auth, basket, orders, payments).

  * Import the collection and set environment variables such as `baseUrl` and `jwtToken`.

---

## Modules & important endpoints (examples)

> Replace or expand the list below with your actual routes.

### Catalog

* `GET /api/products`
* `GET /api/products/{id}`
* `POST /api/products` (admin)

### Basket

* `GET /api/basket/{buyerId}`
* `POST /api/basket` (add item)
* `DELETE /api/basket/{buyerId}/items/{itemId}`

### Orders

* `POST /api/orders` (create order from basket)
* `GET /api/orders/{id}`
* `GET /api/orders/user/{userId}`

### Payments

* `POST /api/payments/checkout` (creates Stripe payment intent)
* `POST /api/payments/webhook` (Stripe webhook endpoint)

### Identity

* `POST /api/account/register`
* `POST /api/account/login`

---

## Webhooks & Stripe

* Configure your Stripe webhook endpoint (e.g. `/api/payments/webhook`) and set the signing secret in configuration.
* Use Stripe CLI for local webhook forwarding during development.

---

## CI / CD (suggestion)

Include a GitHub Actions workflow that:

* Runs `dotnet restore`, `dotnet build`, `dotnet test`
* Builds Docker image and pushes on tag/merge to `main`
* Deploys to target environment (Azure App Service, Kubernetes, etc.)

You can add a `/.github/workflows/ci.yml` with a simple pipeline — say if you want, I can generate this file.

---

## Contributing 💬

1. Fork the repo
2. Create a feature branch (`feature/your-feature`)
3. Open a pull request with a clear description
4. Ensure tests pass and add new tests where applicable

---

## Environment variables & example placeholders

Use the `your_...` placeholders in `appsettings` or env vars. Example:

```
# Environment variables
ConnectionStrings__DefaultConnection=Server=your_server_name;Database=your_store_database;User Id=your_db_user;Password=your_db_password;
RedisConnection=your_redis_host:6379
JwtOptions__SecretKey=your_jwt_secret_key_min_32_chars
Stripe__Secretkey=sk_test_your_stripe_secret_key
```

---

## What I still need from you (to make README perfect)

* Preferred language: **English** or **Arabic** (or both)?
* Repo name and GitHub URL you want used in examples
* Do you want a short demo/banner image? (logo or screenshot)
* Main branch name (e.g. `main` or `master`)
* Do you use Docker and GitHub Actions? Want workflow files added?
* Any public demo URL (staging/production)?
* License choice (MIT / Apache-2.0 / Proprietary)
* Do you want a `POSTMAN` collection / `OpenAPI` export included?

---

## License

This project is licensed under the **MIT License**.

You are free to use, modify, and distribute this project for learning and development purposes.

See the `LICENSE` file for more details.

---

## Maintainer

**Alsayed Kabeil**
GitHub: [https://github.com/Alsayedkabeil](https://github.com/Alsayedkabeil)

---

> This repository is part of my **Backend .NET portfolio** and demonstrates real-world e‑commerce concepts, clean architecture, and API best practices.
