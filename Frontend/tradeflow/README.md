# Tradeflow - Wholesale Management System

A modern, full-stack wholesale management system built with SvelteKit (frontend) and .NET 9 Web API (backend).

## 🏗️ Architecture

### Frontend (SvelteKit)
- **Framework**: SvelteKit 2.63.0 with Svelte 5 Runes
- **Styling**: Tailwind CSS 4.3.3
- **Language**: TypeScript 6.0.3
- **State Management**: Svelte 5 Runes ($state, $props, $derived)
- **HTTP Client**: Axios with interceptors for JWT authentication
- **Authentication**: JWT-based with cookie storage

### Backend (.NET 9)
- **Framework**: .NET 9 Web API
- **Architecture**: Clean Architecture (Domain, Application, Infrastructure, Presentation)
- **Patterns**: CQRS with MediatR, Repository Pattern, Unit of Work
- **Database**: SQL Server 2022 with Entity Framework Core 9
- **Authentication**: JWT Bearer Token Authentication

## 🚀 Quick Start

### Prerequisites
- Node.js 18+ and npm
- .NET 9 SDK
- SQL Server 2022 (for local development)

### Environment Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd Tree
   ```

2. **Backend Setup**
   ```bash
   cd Backend
   dotnet restore
   # Update connection string in Tradeflow.API/appsettings.json
   dotnet run --project Tradeflow.API
   ```
   The API will be available at http://localhost:5157

3. **Frontend Setup**
   ```bash
   cd Frontend/tradeflow
   npm install
   cp .env.example .env
   # Update PUBLIC_API_BASE_URL in .env if needed
   npm run dev
   ```
   The frontend will be available at http://localhost:5173

### Docker Setup

To run both backend and frontend with Docker:

```bash
cd Backend
docker compose up --build
```

- Backend API: http://localhost:8080
- Frontend: http://localhost:5173
- Swagger UI: http://localhost:8080/swagger

## 🔐 Authentication

The application uses JWT-based authentication:

1. Navigate to `/login`
2. Enter credentials (demo: `+1234567890` / `password123`)
3. JWT token is stored in cookies
4. All API requests include the token automatically
5. Protected routes redirect to login if not authenticated

## 📁 Project Structure

### Frontend
```
Frontend/tradeflow/
├── src/
│   ├── lib/
│   │   ├── components/ui/dashboard/  # Reusable dashboard components
│   │   ├── services/                 # API service layers
│   │   ├── stores/                   # State management (auth)
│   │   └── types/                    # TypeScript type definitions
│   ├── routes/
│   │   ├── dashboard/               # Dashboard pages
│   │   │   ├── warehouses/          # Warehouse management
│   │   │   ├── customers/           # Customer directory
│   │   │   ├── sales-reps/          # Sales rep performance
│   │   │   ├── reports/             # Reports & analytics
│   │   │   ├── products/            # Product catalog
│   │   │   └── orders/              # Order management
│   │   └── login/                   # Authentication page
│   └── hooks.server.ts              # Server-side auth hooks
└── .env.example                     # Environment variables template
```

### Backend
```
Backend/
├── Tradeflow.Domain/          # Core business logic
├── Tradeflow.Application/     # CQRS commands/queries
├── Tradeflow.Infrastructure/   # Data access layer
└── Tradeflow.API/              # REST API controllers
```

## 🔌 API Integration

The frontend is fully integrated with the backend API:

### Services
- `warehouses.ts` - Warehouse management
- `customers.ts` - Customer CRUD operations
- `employees.ts` - Employee & sales rep performance
- `products.ts` - Product catalog management

### Authentication Flow
1. Login calls `/api/auth/login`
2. JWT token stored in cookies
3. Axios interceptor adds token to all requests
4. 401 errors trigger automatic logout

### Data Loading
All dashboard pages use `onMount` to fetch data from the API:
- Loading states with spinners
- Error handling with user-friendly messages
- Data transformation to match UI requirements

## 📊 Features

### Dashboard Pages
- **Warehouses**: Real-time inventory monitoring, stock alerts, transfers
- **Customers**: Customer directory, order history, account details
- **Sales Reps**: Performance tracking, commission calculations, activity logs
- **Reports**: Sales analytics, inventory reports, team performance
- **Products**: Product catalog, stock management, warehouse assignment
- **Orders**: Order creation, status tracking, customer management

### UI Components
- Modular, reusable components
- Responsive design (mobile-first)
- Loading states and error handling
- Active navigation states
- Dark mode support

## 🛠️ Development

### Frontend Development
```bash
cd Frontend/tradeflow
npm run dev          # Start dev server
npm run build        # Build for production
npm run preview      # Preview production build
npm run lint         # Run ESLint
npm run check        # Run TypeScript checks
```

### Backend Development
```bash
cd Backend
dotnet run --project Tradeflow.API
dotnet build
dotnet test
```

## 🔧 Configuration

### Environment Variables

Frontend (`.env`):
```
PUBLIC_API_BASE_URL=http://localhost:5157/api
```

Backend (`appsettings.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TradeflowDb;Trusted_Connection=True;"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key",
    "Issuer": "Tradeflow",
    "Audience": "TradeflowUsers"
  }
}
```

## 📝 API Documentation

Once the backend is running, access Swagger UI at:
- Local: http://localhost:5157/swagger
- Docker: http://localhost:8080/swagger

## 🧪 Testing

### Backend Testing
```bash
cd Backend
dotnet test
```

### Frontend Testing
```bash
cd Frontend/tradeflow
npm run test
```

## 📦 Deployment

### Frontend Deployment
```bash
cd Frontend/tradeflow
npm run build
# Deploy the `build/` directory to your hosting provider
```

### Backend Deployment
```bash
cd Backend
docker build -t tradeflow-api .
docker run -p 8080:8080 tradeflow-api
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## 📄 License

This project is provided as-is for educational and commercial use.

## 🆘 Support

For issues or questions, please refer to the Swagger UI documentation at `/swagger` when running the application.
