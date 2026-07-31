# Tradeflow API

A production-ready Wholesale Management System built with .NET 9 Web API using Clean Architecture, CQRS pattern, and Entity Framework Core.

## 🏗️ Architecture

This project follows **Clean Architecture** principles with clear separation of concerns:

```
Tradeflow/
├── Tradeflow.Domain/          # Core business logic (Entities, Enums, Interfaces)
├── Tradeflow.Application/     # Application logic (CQRS Commands/Queries, Validators)
├── Tradeflow.Infrastructure/   # External concerns (EF Core, Repository, Unit of Work)
└── Tradeflow.API/              # Presentation layer (Controllers, Exception Handling)
```

### Architecture Layers

- **Domain Layer**: Contains business entities, enums, and core interfaces (IRepository, IUnitOfWork). No external dependencies.
- **Application Layer**: Implements CQRS pattern with MediatR, FluentValidation validators, and business logic handlers.
- **Infrastructure Layer**: Handles data access with EF Core, implements Repository and Unit of Work patterns.
- **Presentation Layer**: REST API controllers with global exception handling and Swagger documentation.

### Design Patterns Used

- **CQRS (Command Query Responsibility Segregation)**: Separates read and write operations using MediatR
- **Repository Pattern**: Abstracts data access logic
- **Unit of Work**: Manages database transactions
- **Validation Pipeline**: FluentValidation with MediatR pipeline behavior
- **Dependency Injection**: Built-in .NET DI container

## 🚀 Quick Start

### Prerequisites

- Docker and Docker Compose installed (for Docker deployment)
- .NET 9 SDK (for local development)
- SQL Server (for local development)

### Running Locally

1. Update connection string in `Tradeflow.API/appsettings.json`
2. Run the application:
   ```bash
   dotnet run --project Tradeflow.API
   ```
3. Access Swagger UI at http://localhost:5157/swagger

### Running with Docker

```bash
docker compose up --build
```

This command will:
1. Build the .NET API application
2. Start SQL Server 2022 container
3. Wait for SQL Server to be ready
4. Start the API container
5. Seed the database with sample data
6. Expose the API on port 8080

### Access the Application

- **Local API Base URL**: http://localhost:5157
- **Local Swagger UI**: http://localhost:5157/swagger
- **Docker API Base URL**: http://localhost:8080
- **Docker Swagger UI**: http://localhost:8080/swagger
- **SQL Server**: localhost:1433

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

### Enums

- **EmployeeRole**: SalesRepresentative, Manager, Worker, Engineer, Accountant
- **CustomerType**: Restaurant, Hotel, Shop
- **OrderStatus**: Pending, Approved, Completed, Cancelled

## 🔌 API Endpoints

### Orders

#### Create Order
Validates stock availability, deducts quantities, and creates order with details.

```http
POST /api/orders
Content-Type: application/json

{
  "customerId": 1,
  "salesRepId": 1,
  "warehouseId": 1,
  "items": [
    {
      "productId": 1,
      "quantity": 10
    },
    {
      "productId": 2,
      "quantity": 5
    }
  ]
}
```

**Response**: `201 Created` with Order ID

#### Get Order by ID
Retrieves order details with full order items.

```http
GET /api/orders/{id}
```

**Response**:
```json
{
  "orderId": 1,
  "customerName": "Grand Hotel",
  "salesRepName": "John Smith",
  "warehouseName": "Main Warehouse",
  "status": "Completed",
  "totalAmount": 45.50,
  "createdAt": "2024-01-15T10:30:00Z",
  "items": [
    {
      "productId": 1,
      "productName": "Cola Soda 2L",
      "productBarcode": "1001",
      "quantity": 10,
      "unitPrice": 2.50,
      "totalPrice": 25.00
    }
  ]
}
```

#### Update Order Status
Updates order status with validation of valid transitions (Pending → Approved → Completed).

```http
PUT /api/orders/{orderId}/status
Content-Type: application/json

{
  "orderId": 1,
  "status": "Approved"
}
```

**Response**: `200 OK`

#### Get Orders by Sales Representative
Retrieves all orders for a specific sales rep.

```http
GET /api/orders/sales-rep/{salesRepId}
```

**Response**:
```json
[
  {
    "orderId": 1,
    "customerName": "Grand Hotel",
    "warehouseName": "Main Warehouse",
    "status": "Completed",
    "totalAmount": 45.50,
    "createdAt": "2024-01-15T10:30:00Z",
    "items": [
      {
        "productId": 1,
        "quantity": 10
      }
    ]
  }
]
```

### Categories

#### Get All Categories
```http
GET /api/categories
```

#### Get Category by ID
```http
GET /api/categories/{id}
```

#### Create Category
```http
POST /api/categories
Content-Type: application/json

{
  "name": "Electronics"
}
```

#### Update Category
```http
PUT /api/categories/{id}
Content-Type: application/json

{
  "id": 1,
  "name": "Electronics"
}
```

#### Delete Category
```http
DELETE /api/categories/{id}
```

### Customers

#### Get All Customers
```http
GET /api/customers
```

#### Get Customer by ID
```http
GET /api/customers/{id}
```

#### Create Customer
```http
POST /api/customers
Content-Type: application/json

{
  "name": "New Hotel",
  "type": 1,
  "address": "123 Main St",
  "phone": "+1234567890"
}
```

#### Update Customer
```http
PUT /api/customers/{id}
Content-Type: application/json

{
  "id": 1,
  "name": "Updated Hotel",
  "type": 1,
  "address": "123 Main St",
  "phone": "+1234567890"
}
```

#### Delete Customer
```http
DELETE /api/customers/{id}
```

### Products

#### Get All Products
```http
GET /api/products
```

#### Get Product by ID
```http
GET /api/products/{id}
```

#### Create Product
```http
POST /api/products
Content-Type: application/json

{
  "name": "New Product",
  "barcode": "9999",
  "price": 10.00,
  "cost": 5.00,
  "categoryId": 1
}
```

#### Update Product
```http
PUT /api/products/{id}
Content-Type: application/json

{
  "id": 1,
  "name": "Updated Product",
  "barcode": "9999",
  "price": 10.00,
  "cost": 5.00,
  "categoryId": 1
}
```

#### Soft Delete Product
```http
PATCH /api/products/{id}/soft-delete
```

#### Delete Product (Permanent)
```http
DELETE /api/products/{id}
```

### Warehouses

#### Get All Warehouses
```http
GET /api/warehouses
```

#### Get Warehouse by ID
```http
GET /api/warehouses/{id}
```

#### Create Warehouse
```http
POST /api/warehouses
Content-Type: application/json

{
  "name": "New Warehouse",
  "location": "Industrial Zone D"
}
```

#### Update Warehouse
```http
PUT /api/warehouses/{id}
Content-Type: application/json

{
  "id": 1,
  "name": "Updated Warehouse",
  "location": "Industrial Zone D"
}
```

#### Delete Warehouse
```http
DELETE /api/warehouses/{id}
```

#### Get Products by Warehouse
Returns available products and quantities for a specific warehouse.

```http
GET /api/warehouses/{warehouseId}/products
```

**Response**:
```json
[
  {
    "productId": 1,
    "productName": "Cola Soda 2L",
    "barcode": "1001",
    "price": 2.50,
    "quantity": 150,
    "categoryName": "Beverages"
  }
]
```

#### Assign Employee to Warehouse
Links an employee to a warehouse.

```http
POST /api/warehouses/assign-employee
Content-Type: application/json

{
  "employeeId": 1,
  "warehouseId": 1
}
```

**Response**: `200 OK`

### Employees

#### Get All Employees
```http
GET /api/employees
```

#### Get Employee by ID
```http
GET /api/employees/{id}
```

#### Create Employee
```http
POST /api/employees
Content-Type: application/json

{
  "name": "New Employee",
  "role": 0,
  "phone": "+1234567890",
  "password": "password123",
  "commissionId": 1
}
```

#### Update Employee
```http
PUT /api/employees/{id}
Content-Type: application/json

{
  "id": 1,
  "name": "Updated Employee",
  "role": 0,
  "phone": "+1234567890",
  "password": "password123",
  "commissionId": 1
}
```

#### Delete Employee
```http
DELETE /api/employees/{id}
```

#### Get Sales Representative Performance
Calculates total sales, commission-based target, achievement percentage, and earned commission.

```http
GET /api/employees/{salesRepId}/performance
```

**Response**:
```json
{
  "salesRepId": 1,
  "salesRepName": "John Smith",
  "targetAmount": 50000.00,
  "commissionPercentage": 5.0,
  "totalSales": 32500.00,
  "achievementPercentage": 65.0,
  "earnedCommission": 1625.00,
  "totalOrders": 15
}
```

### Commissions

#### Get All Commissions
```http
GET /api/commissions
```

#### Get Commission by ID
```http
GET /api/commissions/{id}
```

#### Create Commission
```http
POST /api/commissions
Content-Type: application/json

{
  "targetAmount": 50000.00,
  "percentage": 5.0,
  "notes": "Standard commission for sales reps"
}
```

#### Update Commission
```http
PUT /api/commissions/{id}
Content-Type: application/json

{
  "id": 1,
  "targetAmount": 75000.00,
  "percentage": 7.0,
  "notes": "High performance commission"
}
```

#### Delete Commission
```http
DELETE /api/commissions/{id}
```

### Authentication

#### Login
Authenticates an employee and returns a JWT token.

```http
POST /api/auth/login
Content-Type: application/json

{
  "phone": "+1234567890",
  "password": "password123"
}
```

**Response**:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "employeeId": 1,
  "name": "John Smith",
  "role": "SalesRepresentative",
  "warehouseId": 1
}
```

## 🔧 Technology Stack

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

## 📝 Sample Data

The application automatically seeds the database on startup with:

- **5 Categories**: Beverages, Food Items, Dairy Products, Snacks, Cleaning Supplies
- **15 Products**: Various items across all categories with barcodes
- **3 Warehouses**: Main Warehouse, East Warehouse, West Warehouse
- **Stock**: Random quantities (50-500) for each product in each warehouse
- **3 Commissions**: Standard (5%), High Performance (7%), Elite (10%)
- **6 Employees**: Various roles including Sales Representatives, Managers, Workers (with assigned commissions)
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

## 📦 Project Structure

```
Tradeflow/
├── Tradeflow.Domain/
│   ├── Entities/
│   │   ├── Category.cs
│   │   ├── Product.cs
│   │   ├── Warehouse.cs
│   │   ├── Stock.cs
│   │   ├── Employee.cs
│   │   ├── EmployeeWarehouse.cs
│   │   ├── Customer.cs
│   │   ├── Order.cs
│   │   └── OrderDetail.cs
│   ├── Enums/
│   │   ├── EmployeeRole.cs
│   │   ├── CustomerType.cs
│   │   └── OrderStatus.cs
│   └── Interfaces/
│       ├── IRepository.cs
│       └── IUnitOfWork.cs
├── Tradeflow.Application/
│   ├── Commands/
│   │   ├── Categories/
│   │   │   ├── CreateCategoryCommand.cs
│   │   │   ├── UpdateCategoryCommand.cs
│   │   │   └── DeleteCategoryCommand.cs
│   │   ├── Customers/
│   │   │   ├── CreateCustomerCommand.cs
│   │   │   ├── UpdateCustomerCommand.cs
│   │   │   └── DeleteCustomerCommand.cs
│   │   ├── Products/
│   │   │   ├── CreateProductCommand.cs
│   │   │   ├── UpdateProductCommand.cs
│   │   │   ├── DeleteProductCommand.cs
│   │   │   └── SoftDeleteProductCommand.cs
│   │   ├── Warehouses/
│   │   │   ├── CreateWarehouseCommand.cs
│   │   │   ├── UpdateWarehouseCommand.cs
│   │   │   └── DeleteWarehouseCommand.cs
│   │   ├── Employees/
│   │   │   ├── CreateEmployeeCommand.cs
│   │   │   ├── UpdateEmployeeCommand.cs
│   │   │   └── DeleteEmployeeCommand.cs
│   │   ├── CreateOrderCommand.cs
│   │   ├── UpdateOrderStatusCommand.cs
│   │   └── AssignEmployeeToWarehouseCommand.cs
│   ├── Queries/
│   │   ├── GetProductsByWarehouseQuery.cs
│   │   ├── GetSalesRepPerformanceQuery.cs
│   │   ├── GetOrdersBySalesRepQuery.cs
│   │   └── GetOrderByIdQuery.cs
│   ├── Handlers/
│   │   ├── Categories/
│   │   │   ├── CreateCategoryCommandHandler.cs
│   │   │   ├── UpdateCategoryCommandHandler.cs
│   │   │   └── DeleteCategoryCommandHandler.cs
│   │   ├── Customers/
│   │   │   ├── CreateCustomerCommandHandler.cs
│   │   │   ├── UpdateCustomerCommandHandler.cs
│   │   │   └── DeleteCustomerCommandHandler.cs
│   │   ├── Products/
│   │   │   ├── CreateProductCommandHandler.cs
│   │   │   ├── UpdateProductCommandHandler.cs
│   │   │   ├── DeleteProductCommandHandler.cs
│   │   │   └── SoftDeleteProductCommandHandler.cs
│   │   ├── Warehouses/
│   │   │   ├── CreateWarehouseCommandHandler.cs
│   │   │   ├── UpdateWarehouseCommandHandler.cs
│   │   │   └── DeleteWarehouseCommandHandler.cs
│   │   ├── Employees/
│   │   │   ├── CreateEmployeeCommandHandler.cs
│   │   │   ├── UpdateEmployeeCommandHandler.cs
│   │   │   └── DeleteEmployeeCommandHandler.cs
│   │   ├── CreateOrderCommandHandler.cs
│   │   ├── UpdateOrderStatusCommandHandler.cs
│   │   ├── AssignEmployeeToWarehouseCommandHandler.cs
│   │   ├── GetProductsByWarehouseQueryHandler.cs
│   │   ├── GetSalesRepPerformanceQueryHandler.cs
│   │   ├── GetOrdersBySalesRepQueryHandler.cs
│   │   └── GetOrderByIdQueryHandler.cs
│   ├── Validators/
│   │   ├── Categories/
│   │   │   ├── CreateCategoryCommandValidator.cs
│   │   │   ├── UpdateCategoryCommandValidator.cs
│   │   │   └── DeleteCategoryCommandValidator.cs
│   │   ├── Customers/
│   │   │   ├── CreateCustomerCommandValidator.cs
│   │   │   ├── UpdateCustomerCommandValidator.cs
│   │   │   └── DeleteCustomerCommandValidator.cs
│   │   ├── Products/
│   │   │   ├── CreateProductCommandValidator.cs
│   │   │   ├── UpdateProductCommandValidator.cs
│   │   │   ├── DeleteProductCommandValidator.cs
│   │   │   └── SoftDeleteProductCommandValidator.cs
│   │   ├── Warehouses/
│   │   │   ├── CreateWarehouseCommandValidator.cs
│   │   │   ├── UpdateWarehouseCommandValidator.cs
│   │   │   └── DeleteWarehouseCommandValidator.cs
│   │   ├── Employees/
│   │   │   ├── CreateEmployeeCommandValidator.cs
│   │   │   ├── UpdateEmployeeCommandValidator.cs
│   │   │   └── DeleteEmployeeCommandValidator.cs
│   │   ├── CreateOrderCommandValidator.cs
│   │   ├── UpdateOrderStatusCommandValidator.cs
│   │   └── AssignEmployeeToWarehouseCommandValidator.cs
│   └── Behaviors/
│       └── ValidationBehavior.cs
├── Tradeflow.Infrastructure/
│   ├── Configurations/
│   │   ├── CategoryConfiguration.cs
│   │   ├── ProductConfiguration.cs
│   │   ├── WarehouseConfiguration.cs
│   │   ├── StockConfiguration.cs
│   │   ├── EmployeeConfiguration.cs
│   │   ├── EmployeeWarehouseConfiguration.cs
│   │   ├── CustomerConfiguration.cs
│   │   ├── OrderConfiguration.cs
│   │   └── OrderDetailConfiguration.cs
│   ├── Data/
│   │   ├── AppDbContext.cs
│   │   └── DbInitializer.cs
│   └── Repositories/
│       ├── Repository.cs
│       └── UnitOfWork.cs
├── Tradeflow.API/
│   ├── Controllers/
│   │   ├── OrdersController.cs
│   │   ├── CategoriesController.cs
│   │   ├── CustomersController.cs
│   │   ├── ProductsController.cs
│   │   ├── WarehousesController.cs
│   │   └── EmployeesController.cs
│   ├── Exceptions/
│   │   └── GlobalExceptionHandler.cs
│   ├── Program.cs
│   ├── appsettings.json
│   └── Dockerfile
├── docker-compose.yml
└── README.md
```

## 🧪 Development

### Running Locally

1. Update connection string in `appsettings.json`
2. Run migrations (if needed):
   ```bash
   dotnet ef database update
   ```
3. Run the application:
   ```bash
   dotnet run --project Tradeflow.API
   ```

### Adding New Commands/Queries

1. Create command/query in `Application/Commands` or `Application/Queries`
2. Create validator in `Application/Validators`
3. Create handler in `Application/Handlers`
4. Add controller endpoint in `API/Controllers`

## 📄 License

This project is provided as-is for educational and commercial use.

## 👥 Support

For issues or questions, please refer to the Swagger UI documentation at `/swagger` when running the application.
