# Choice — Service Marketplace Platform

**Choice** is a full-stack marketplace platform that connects **clients** (who post service requests) with **companies** (who respond to those requests). It features real-time chat, geo-based search, payments, subscriptions, and an admin panel — all accessible through a React Native mobile app backed by a modular ASP.NET Core API.

---

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Modules](#modules)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Backend Setup](#backend-setup)
  - [Production First-Time Setup](#production-first-time-setup)
  - [Mobile App Setup](#mobile-app-setup)
- [Configuration](#configuration)
- [Running Tests](#running-tests)

---

## Overview

The platform supports three user roles:

- **Client** — creates service orders, enrolls with a company, leaves reviews, chats.
- **Company** — browses and responds to orders, confirms enrollment dates, manages profile data and social media links.
- **Admin** — manages categories, reviews, users, and review templates.

Key capabilities:

- Geo-coded order requests with radius-based search and map view
- Full order lifecycle: create → respond → enroll → confirm → finish/cancel
- Prepayment (10–25% of order price) via YooKassa
- Wallet system with deposit, withdrawal, and peer-to-peer transfer
- Company subscription plans
- Real-time chat (text, images, order status messages) via SignalR
- Push notifications via Firebase Cloud Messaging
- Phone-number authentication with SMS verification codes
- Media file storage via MinIO (S3-compatible)

---

## Architecture

The backend follows **Modular Monolith** architecture with Domain-Driven Design, CQRS, and the Outbox pattern for reliable inter-module messaging.

```
┌───────────────────────────────────────────────────────┐
│                   Mobile App (React Native)           │
└────────────────────────┬──────────────────────────────┘
                         │ HTTP / SignalR
┌────────────────────────▼──────────────────────────────┐
│                  ASP.NET Core Web API                  │
│  ┌──────────┐ ┌──────────┐ ┌───────────┐ ┌────────┐  │
│  │ Identity │ │  Users   │ │ Payments  │ │  Chat  │  │
│  └──────────┘ └──────────┘ └───────────┘ └────────┘  │
│  ┌───────────────────────────────────────────────────┐ │
│  │            BuildingBlocks (shared kernel)         │ │
│  └───────────────────────────────────────────────────┘ │
└────────────────────────┬──────────────────────────────┘
                         │
        ┌────────────────┼──────────────┐
        ▼                ▼              ▼
   PostgreSQL         MinIO         Firebase / YooKassa
```

Inter-module communication uses **MassTransit** (event bus) with integration events published through the **Outbox pattern**. Scheduled jobs run via **Quartz.NET**.

---

## Modules

| Module | Responsibility |
|---|---|
| **Identity** | Authentication (OpenIddict + password/phone grant), user creation, roles, permissions, SMS codes |
| **Users** | Client and company profiles, order requests, order responses, reviews, geo-distance queries |
| **Payments** | Wallets, subscriptions, subscription payments, payouts via YooKassa |
| **Chat** | Real-time messaging (SignalR), push notifications (Firebase), order-linked messages |
| **Administration** | Categories, review templates, user moderation (ban/unban), content editing |

Each module is internally structured as:

```
Module/
  Domain/          # Entities, value objects, domain rules
  Application/     # Commands, queries, DTOs, contracts
  Infrastructure/  # EF Core, MassTransit consumers, configuration
  IntegrationEvents/
```

---

## Tech Stack

### Backend

| Layer | Technology |
|---|---|
| Runtime | .NET 8, ASP.NET Core |
| ORM | Entity Framework Core |
| Database | PostgreSQL |
| Auth | OpenIddict (OAuth2 password & phone grant) |
| Messaging | MassTransit |
| Real-time | SignalR |
| IoC | Autofac |
| Validation | FluentValidation |
| Background jobs | Quartz.NET |
| Logging | Serilog |
| Object storage | MinIO (S3) |
| Payments gateway | YooKassa |
| Push notifications | Firebase Admin SDK |
| Geocoding | Yandex Geocoding API |
| Reverse proxy | Nginx |
| Containerisation | Docker / Docker Compose |

### Mobile App

| Layer | Technology |
|---|---|
| Framework | React Native (TypeScript) |
| Navigation | React Navigation (stack + bottom tabs) |
| Real-time | `@microsoft/signalr` |
| Maps | `react-native-yamap` (Yandex Maps) |
| Push notifications | `@react-native-firebase/messaging` |
| UI components | `@rneui/themed`, `@gorhom/bottom-sheet` |
| Storage | `react-native-mmkv-storage` |
| File handling | `@dr.pogodin/react-native-fs`, `react-native-image-picker` |
| Animations | `react-native-reanimated`, Gesture Handler |

---

## Project Structure

```
choice/
├── App/                        # React Native mobile app
│   ├── screens/                # All screens (client, company, admin, chat)
│   ├── components/             # Reusable UI components
│   ├── navigation/             # Stack navigators and tab bars
│   ├── services/               # API service layer and auth
│   ├── realTime/               # SignalR connection management
│   ├── managers/               # State, account, and connection managers
│   └── contexts/               # React context providers
│
├── Backend/
│   ├── src/
│   │   ├── Modules/            # Feature modules (Identity, Users, Payments, Chat, Administration)
│   │   ├── BuildingBlocks/     # Shared domain/application/infrastructure primitives
│   │   ├── WebApi/             # ASP.NET Core host, controllers, Startup
│   │   └── Database/           # SQL migration scripts and migrator tool
│   ├── tests/
│   │   ├── UnitTests/
│   │   └── IntegrationTests/
│   ├── nginx/                  # Nginx reverse proxy config and Dockerfile
│   └── docker-compose/         # Docker Compose files and environment config
│
└── Architecture/               # C4 diagrams and use-case diagrams (PlantUML)
```

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/) and npm
- [Docker & Docker Compose](https://docs.docker.com/get-docker/)
- [React Native environment](https://reactnative.dev/docs/environment-setup) (Android Studio / Xcode)
- A Firebase project with a service account credentials JSON file
- YooKassa merchant credentials (for payments)
- A Yandex API key (for geocoding / maps)
- An SMS gateway account

### Backend Setup

1. **Copy the environment file and fill in your values:**

   ```bash
   cd Backend/docker-compose
   cp .env.example .env
   # Edit .env with your credentials (see Configuration section below)
   ```

2. **Start the infrastructure and application:**

   ```bash
   # Development (Windows)
   ./setup.development.ps1

   # Development (Linux/macOS)
   ./setup.development.sh
   ```

   This starts: `webapi`, `minio`, `nginx`, `db` (PostgreSQL), and `pihole` containers. Nginx listens on `8080` (HTTP) and `8443` (HTTPS) in development. The dev hostnames are `choice.ru` and `minio.choice.ru`.

3. **Configure Pi-hole DNS for mobile device debugging**

   The stack includes a Pi-hole container running a local DNS server on port 53. To make `choice.ru` and `minio.choice.ru` resolve to your dev machine on any device on the same network:

   - Open the Pi-hole admin UI at `http://<your-machine-ip>:8084`
   - Go to **Local DNS → DNS Records** and add two entries pointing both hostnames at your machine's local IP:
     ```
     choice.ru       →  <your-machine-ip>
     minio.choice.ru →  <your-machine-ip>
     ```
   - In your router's settings, set the primary DNS server to `<your-machine-ip>`.

   All devices on the network will now resolve both hostnames through Pi-hole and reach the local Nginx instance directly.

4. **Run database migrations:**

   ```bash
   cd Backend/src/Database
   ./migrate.development.sh   # Linux/macOS
   # or
   ./migrate.development.ps1  # Windows
   ```

4. The API will be available at `https://localhost` (via Nginx) or `http://localhost:8082` directly.

### Production First-Time Setup

On a fresh production server, Nginx is configured to listen on port 443 (HTTPS) and redirect all port-80 traffic to HTTPS. This creates a chicken-and-egg problem: Certbot needs to serve an ACME challenge over plain HTTP to issue the TLS certificate, but the redirect is already in place before any certificate exists.

To break the cycle, temporarily reconfigure Nginx to serve plain HTTP, obtain the certificate, then restore the full HTTPS config.

**Step 1 — Temporarily switch Nginx to HTTP-only**

Edit `Backend/nginx/templates/default.conf.template` and replace the entire file content with a minimal HTTP-only config that lets Certbot reach the webroot:

```nginx
server {
    listen 80;
    server_name ${HOST_NAME} www.${HOST_NAME} ${MINIO_HOST_NAME};

    location /.well-known/acme-challenge/ {
        root /var/www/certbot;
    }

    location / {
        return 200 'ok';
    }
}
```

**Step 2 — Start the stack with the temporary config**

```bash
cd Backend/docker-compose
sudo docker compose -f docker-compose.yml -f docker-compose.production.yml up -d
```

**Step 3 — Run the production setup script**

The script generates the signing certificate, runs DB migrations, and issues the Let's Encrypt certificate via the Certbot webroot method:

```bash
cd Backend/docker-compose
./setup.production.sh
```

Certbot will place the issued certificates into `$NGINX_CERTIFICATE_FOLDER` as `certificate.pem` and `private_key.pem`.

**Step 4 — Restore the original Nginx config**

Revert `Backend/nginx/templates/default.conf.template` to its original content (the full HTTPS config with `listen 443 ssl` servers and the HTTP→HTTPS redirect).

**Step 5 — Rebuild and restart only the Nginx container**

```bash
sudo docker compose -f docker-compose.yml -f docker-compose.production.yml up -d --build nginx
```

Nginx will now start with the real TLS certificates in place and HTTPS fully operational.

---

### Mobile App Setup

1. **Install dependencies:**

   ```bash
   cd App
   npm install
   ```

2. **iOS (macOS only):**

   ```bash
   cd ios && pod install && cd ..
   npx react-native run-ios
   ```

3. **Android:**

   ```bash
   npx react-native run-android
   ```

> Make sure to place your `google-services.json` (Firebase) in `App/android/app/` and configure the API base URL in the app's HTTP service.

---

## Configuration

All backend secrets are provided via the `Backend/docker-compose/.env` file:

| Variable | Description |
|---|---|
| `HOST_NAME` | Public hostname (e.g. `example.com`) |
| `MINIO_HOST_NAME` | MinIO public hostname |
| `SECRET_KEY` | JWT / token signing secret |
| `CERTIFICATE_PASSWORD` | TLS certificate password |
| `CLIEN_ID` / `CLIENT_SECRET` | OpenIddict OAuth2 client credentials |
| `YANDEX_API_KEY` | Yandex Geocoding API key |
| `YOO_KASSA_BASE_URL` | YooKassa API base URL |
| `YOO_KASSA_PAYMENTS_SECRET_KEY` | YooKassa payments secret |
| `YOO_KASSA_PAYMENTS_APP_ID` | YooKassa payments app ID |
| `YOO_KASSA_PAYOUTS_SECRET_KEY` | YooKassa payouts secret |
| `YOO_KASSA_PAYOUTS_APP_ID` | YooKassa payouts app ID |
| `SMS_API_SETTINGS_LOGIN` | SMS gateway login |
| `SMS_API_SETTINGS_PASSWORD` | SMS gateway password |
| `SMS_API_SETTINGS_BASE_URL` | SMS gateway base URL |
| `CONNECTION_STRING` | PostgreSQL connection string |
| `NGINX_CERTIFICATE_FOLDER` | Path to TLS certificate folder on host |
| `GOOGLE_APPLICATION_CREDENTIALS` | Path to Firebase service account JSON |

---

## Running Tests

### Unit Tests

```bash
cd Backend
dotnet test tests/UnitTests/UnitTests.csproj
```

### Integration Tests

Integration tests require a running PostgreSQL database. Configure connection details in `Backend/tests/IntegrationTests/testsettings.json`, then:

```bash
dotnet test tests/IntegrationTests/IntegrationTests.csproj
```

The integration test suite covers full business process flows including company registration and end-to-end order processing.
