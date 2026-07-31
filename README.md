# Tradeflow - Wholesale Management System

A comprehensive B2B wholesale management system built with modern technologies, featuring order management, inventory tracking, employee performance monitoring, and customer relationship management.

## 🏗️ Architecture

This project follows a **monorepo structure** with separate Backend and Frontend applications:

```
Tree/
├── Backend/          # .NET 9 Web API (Clean Architecture)
└── Frontend/         # SvelteKit Application
```

### Backend Architecture

The backend follows **Clean Architecture** principles with clear separation of concerns:

```
Backend/
├── Tradeflow.Domain/          # Core business logic (Entities, Enums, Interfaces)
├── Tradeflow.Application/     # Application logic (CQRS Commands/Queries, Validators)
├── Tradeflow.Infrastructure/   # External concerns (EF Core, Repository, Unit of Work)
└── Tradeflow.API/              # Presentation layer (Controllers, Exception Handling)
```

**Architecture Layers:**
- **Domain Layer**: Contains business entities, enums, and core interfaces (IRepository, IUnitOfWork). No external dependencies.
- **Application Layer**: Implements CQRS pattern with MediatR, FluentValidation validators, and business logic handlers.
- **Infrastructure Layer**: Handles data access with EF Core, implements Repository and Unit of Work patterns.
- **Presentation Layer**: REST API controllers with global exception handling and Swagger documentation.

**Design Patterns Used:**
- **CQRS (Command Query Responsibility Segregation)**: Separates read and write operations using MediatR
- **Repository Pattern**: Abstracts data access logic
- **Unit of Work**: Manages database transactions
- **Validation Pipeline**: FluentValidation with MediatR pipeline behavior
- **Dependency Injection**: Built-in .NET DI container

### Frontend Architecture

The frontend is built with **SvelteKit** following modern component-based architecture:

```
Frontend/
├── tradeflow/              # Main SvelteKit application
│   ├── src/
│   │   ├── lib/
│   │   │   ├── components/    # Reusable UI components
│   │   │   ├── services/      # API service layer
│   │   │   ├── stores/        # Svelte stores for state management
│   │   │   └── types/         # TypeScript type definitions
│   │   └── routes/            # File-based routing
│   └── static/               # Static assets
└── ux/                       # UX/UI design references
```

## 🚀 Tech Stack

### Backend
- **Framework**: .NET 9 Web API
- **Architecture**: Clean Architecture (Domain, Application, Infrastructure, Presentation)
- **Patterns**: CQRS with MediatR, Repository Pattern, Unit of Work
- **Database**: SQL Server 2022 with Entity Framework Core 9
- **Validation**: FluentValidation with MediatR ValidationPipeline
- **Error Handling**: Global Exception Handler (ProblemDetails RFC 7807)
- **Logging**: Serilog with Console and File sinks
- **Documentation**: Swagger/OpenAPI with JWT Authentication support
- **Authentication**: JWT Bearer Token Authentication
- **Containerization**: Docker with multi-stage Dockerfile
- **Orchestration**: Docker Compose

### Frontend
- **Framework**: SvelteKit
- **Language**: TypeScript
- **Styling**: Tailwind CSS with Material Design principles
- **State Management**: Svelte stores ($state, $derived)
- **HTTP Client**: Axios
- **Icons**: Material Symbols
- **Build Tool**: Vite
- **Code Quality**: ESLint, Prettier

## 📊 Database Schema

### Entities and Relationships

- **Category**: Product categories (Id, Name)
- **Product**: Products with barcode uniqueness (Id, Name, Barcode, Price, Cost, CategoryId, IsDeleted)
- **Warehouse**: Storage locations (Id, Name, Location)
- **Stock**: Product inventory per warehouse (ProductId, WarehouseId, Quantity) - Composite PK
- **Employee**: Staff members with roles (Id, Name, Role, Phone, Target)
- **EmployeeWarehouse**: Junction table for employee-warehouse assignments (EmployeeId, WarehouseId) - Composite PK
- **Customer**: Business customers (Id, Name, Type, Address, Phone)
- **Order**: Sales orders (Id, CustomerId, SalesRepId, WarehouseId, Status, TotalAmount, CreatedAt)
- **OrderDetail**: Order line items (OrderId, ProductId, Quantity, UnitPrice, TotalPrice) - Composite PK
- **Commission**: Commission structures for sales representatives (Id, TargetAmount, Percentage, Notes)

### Enums

- **EmployeeRole**: SalesRepresentative, Manager, Worker, Engineer, Accountant
- **CustomerType**: Restaurant, Hotel, Shop
- **OrderStatus**: Pending, Approved, Completed, Cancelled

## 🔌 API Endpoints

### Authentication
- `POST /api/auth/login` - Employee authentication with JWT token

### Orders
- `POST /api/orders` - Create order with stock validation
- `GET /api/orders/{id}` - Get order by ID with details
- `PUT /api/orders/{orderId}/status` - Update order status
- `GET /api/orders/sales-rep/{salesRepId}` - Get orders by sales representative

### Categories
- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get category by ID
- `POST /api/categories` - Create category
- `PUT /api/categories/{id}` - Update category
- `DELETE /api/categories/{id}` - Delete category

### Customers
- `GET /api/customers` - Get all customers
- `GET /api/customers/{id}` - Get customer by ID
- `POST /api/customers` - Create customer
- `PUT /api/customers/{id}` - Update customer
- `DELETE /api/customers/{id}` - Delete customer

### Products
- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create product
- `PUT /api/products/{id}` - Update product
- `PATCH /api/products/{id}/soft-delete` - Soft delete product
- `DELETE /api/products/{id}` - Permanent delete product

### Warehouses
- `GET /api/warehouses` - Get all warehouses
- `GET /api/warehouses/{id}` - Get warehouse by ID
- `POST /api/warehouses` - Create warehouse
- `PUT /api/warehouses/{id}` - Update warehouse
- `DELETE /api/warehouses/{id}` - Delete warehouse
- `GET /api/warehouses/{warehouseId}/products` - Get products by warehouse
- `POST /api/warehouses/assign-employee` - Assign employee to warehouse

### Employees
- `GET /api/employees` - Get all employees
- `GET /api/employees/{id}` - Get employee by ID
- `POST /api/employees` - Create employee
- `PUT /api/employees/{id}` - Update employee
- `DELETE /api/employees/{id}` - Delete employee
- `GET /api/employees/{salesRepId}/performance` - Get sales representative performance

### Commissions
- `GET /api/commissions` - Get all commissions
- `GET /api/commissions/{id}` - Get commission by ID
- `POST /api/commissions` - Create commission
- `PUT /api/commissions/{id}` - Update commission
- `DELETE /api/commissions/{id}` - Delete commission

## 🎨 Frontend Features

### Dashboard
- **KPI Grid**: Key performance indicators (Total Sales, Orders, Customers, Warehouses)
- **Sales Chart**: Visual sales performance over time
- **Sales Representatives List**: Team performance overview
- **Top Products Table**: Best-selling products
- **Warehouse Status**: Inventory levels across warehouses

### Order Management
- **Customer Selection**: Choose from registered customers
- **Warehouse Selection**: Select warehouse for order
- **Product Catalog**: Browse and add products to cart
- **Order Summary**: Review cart, calculate totals, confirm order
- **Recent Orders**: View order history

### Product & Inventory
- **Product Table**: List all products with details
- **Add Product Modal**: Create new products
- **Category Management**: Manage product categories
- **Stock Tracking**: Monitor inventory levels

### Warehouse Management
- **Warehouse Table**: List all warehouses
- **Add/Edit Warehouse**: Create and update warehouse information
- **Stock Overview**: View product availability per warehouse

### Sales Team Performance
- **Employee List**: View all employees
- **Performance Metrics**: Track sales targets and achievements
- **Commission Management**: Manage commission structures
- **Activity Tracking**: Monitor sales activities

### Customer Management
- **Customer Directory**: View all customers
- **Customer Details**: View customer information
- **Add Customer Modal**: Register new customers

### Reports & Analytics
- **Net Sales Chart**: Revenue trends
- **Revenue Segmentation**: Sales by category
- **Warehouse Turnover**: Inventory movement
- **Sales Rep Performance**: Team analytics

## 🚀 Quick Start

### Prerequisites

- **Backend**: .NET 9 SDK, SQL Server 2022, Docker (optional)
- **Frontend**: Node.js 18+, npm

### Backend Setup

1. **Navigate to Backend directory:**
   ```bash
   cd Backend
   ```

2. **Update connection string** in `Tradeflow.API/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=TradeflowDb;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
     }
   }
   ```

3. **Run the application:**
   ```bash
   dotnet run --project Tradeflow.API
   ```

4. **Access Swagger UI** at http://localhost:5157/swagger

### Backend with Docker

```bash
cd Backend
docker compose up --build
```

Access the API at http://localhost:8080/swagger

### Frontend Setup

1. **Navigate to Frontend directory:**
   ```bash
   cd Frontend/tradeflow
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Run development server:**
   ```bash
   npm run dev
   ```

4. **Access the application** at http://localhost:5173

### Environment Variables

Create a `.env` file in `Frontend/tradeflow/`:

```env
VITE_API_URL=http://localhost:5157
```

## 📝 Sample Data

The backend automatically seeds the database on startup with:

- **5 Categories**: Beverages, Food Items, Dairy Products, Snacks, Cleaning Supplies
- **15 Products**: Various items across all categories with barcodes
- **3 Warehouses**: Main Warehouse, East Warehouse, West Warehouse
- **Stock**: Random quantities (50-500) for each product in each warehouse
- **3 Commissions**: Standard (5%), High Performance (7%), Elite (10%)
- **6 Employees**: Various roles including Sales Representatives, Managers, Workers
- **6 Customers**: Hotels, Restaurants, and Shops
- **10 Orders**: Sample orders with various statuses
- **Order Details**: Line items for each order

## 🔒 Error Handling

The API uses global exception handling with RFC 7807 ProblemDetails:

- **Validation Errors**: Returns 400 Bad Request with detailed validation errors
- **Operation Errors**: Returns 400 Bad Request for business logic violations
- **Server Errors**: Returns 500 Internal Server Error for unexpected exceptions

Example error response:
```json
{
  "type": "ValidationException",
  "title": "Validation Error",
  "status": 400,
  "detail": "One or more validation errors occurred.",
  "instance": "/api/orders",
  "errors": {
    "Items": ["Order must contain at least one item"]
  }
}
```

## 🔐 Authentication

The system uses JWT Bearer Token Authentication:

1. **Login**: Send credentials to `/api/auth/login`
2. **Receive Token**: Get JWT token and employee information
3. **Use Token**: Include token in Authorization header for protected endpoints

**Login Request:**
```json
{
  "phone": "+1234567890",
  "password": "password123"
}
```

**Login Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "employeeId": 1,
  "name": "John Smith",
  "role": "SalesRepresentative",
  "warehouseId": 1
}
```

## 📦 Project Structure

### Backend Structure
```
Backend/
├── Tradeflow.Domain/
│   ├── Entities/
│   ├── Enums/
│   └── Interfaces/
├── Tradeflow.Application/
│   ├── Commands/
│   ├── Queries/
│   ├── Handlers/
│   ├── Validators/
│   └── Behaviors/
├── Tradeflow.Infrastructure/
│   ├── Configurations/
│   ├── Data/
│   └── Repositories/
├── Tradeflow.API/
│   ├── Controllers/
│   ├── Exceptions/
│   └── Program.cs
├── docker-compose.yml
└── README.md
```

### Frontend Structure
```
Frontend/
├── tradeflow/
│   ├── src/
│   │   ├── lib/
│   │   │   ├── components/
│   │   │   │   └── ui/
│   │   │   │       └── dashboard/
│   │   │   ├── services/
│   │   │   ├── stores/
│   │   │   └── types/
│   │   └── routes/
│   │       └── dashboard/
│   ├── static/
│   └── package.json
└── ux/ (Design references)
```

## 🧪 Development

### Backend Development

**Running Locally:**
```bash
cd Backend
dotnet run --project Tradeflow.API
```

**Adding New Commands/Queries:**
1. Create command/query in `Application/Commands` or `Application/Queries`
2. Create validator in `Application/Validators`
3. Create handler in `Application/Handlers`
4. Add controller endpoint in `API/Controllers`

### Frontend Development

**Running Locally:**
```bash
cd Frontend/tradeflow
npm run dev
```

**Building for Production:**
```bash
npm run build
```

**Preview Production Build:**
```bash
npm run preview
```

## 📄 License

This project is provided as-is for educational and commercial use.

## 👥 Support

For issues or questions, please refer to the Swagger UI documentation at `/swagger` when running the backend application.

## 🤝 Contributing

Contributions are welcome! Please follow these guidelines:

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## 📞 Contact

For any inquiries or support, please contact the development team.

---

**Built with ❤️ using .NET 9 and SvelteKit**
