using Microsoft.EntityFrameworkCore;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Enums;

namespace Tradeflow.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task Initialize(AppDbContext context)
    {
        // Create database if it doesn't exist (don't delete existing data)
        await context.Database.EnsureCreatedAsync();

        // Check if data already exists to avoid duplicate seeding
        if (await context.Categories.AnyAsync())
        {
            return; // Database already seeded, skip seeding
        }

        // Seed Categories
        var categories = new List<Category>
        {
            new() { Name = "Beverages" },
            new() { Name = "Food Items" },
            new() { Name = "Dairy Products" },
            new() { Name = "Snacks" },
            new() { Name = "Cleaning Supplies" }
        };
        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();

        // Seed Warehouses
        var warehouses = new List<Warehouse>
        {
            new() { Name = "Main Warehouse", Location = "Industrial Zone A" },
            new() { Name = "East Warehouse", Location = "Industrial Zone B" },
            new() { Name = "West Warehouse", Location = "Industrial Zone C" }
        };
        await context.Warehouses.AddRangeAsync(warehouses);
        await context.SaveChangesAsync();

        // Seed Products
        var products = new List<Product>
        {
            new() { Name = "Cola Soda 2L", Barcode = "1001", Price = 2.50m, Cost = 1.20m, CategoryId = categories[0].Id },
            new() { Name = "Orange Juice 1L", Barcode = "1002", Price = 3.00m, Cost = 1.50m, CategoryId = categories[0].Id },
            new() { Name = "Mineral Water 500ml", Barcode = "1003", Price = 0.50m, Cost = 0.20m, CategoryId = categories[0].Id },
            new() { Name = "Pasta 500g", Barcode = "2001", Price = 1.80m, Cost = 0.90m, CategoryId = categories[1].Id },
            new() { Name = "Rice 1kg", Barcode = "2002", Price = 2.20m, Cost = 1.10m, CategoryId = categories[1].Id },
            new() { Name = "Canned Tomatoes 400g", Barcode = "2003", Price = 1.50m, Cost = 0.75m, CategoryId = categories[1].Id },
            new() { Name = "Milk 1L", Barcode = "3001", Price = 1.80m, Cost = 0.90m, CategoryId = categories[2].Id },
            new() { Name = "Cheese 200g", Barcode = "3002", Price = 3.50m, Cost = 1.80m, CategoryId = categories[2].Id },
            new() { Name = "Yogurt 500g", Barcode = "3003", Price = 2.00m, Cost = 1.00m, CategoryId = categories[2].Id },
            new() { Name = "Potato Chips 150g", Barcode = "4001", Price = 2.50m, Cost = 1.20m, CategoryId = categories[3].Id },
            new() { Name = "Chocolate Bar 100g", Barcode = "4002", Price = 1.50m, Cost = 0.75m, CategoryId = categories[3].Id },
            new() { Name = "Cookies 200g", Barcode = "4003", Price = 2.00m, Cost = 1.00m, CategoryId = categories[3].Id },
            new() { Name = "Dish Soap 1L", Barcode = "5001", Price = 3.00m, Cost = 1.50m, CategoryId = categories[4].Id },
            new() { Name = "Laundry Detergent 2L", Barcode = "5002", Price = 5.00m, Cost = 2.50m, CategoryId = categories[4].Id },
            new() { Name = "All-Purpose Cleaner 1L", Barcode = "5003", Price = 4.00m, Cost = 2.00m, CategoryId = categories[4].Id }
        };
        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();

        // Seed Stock
        var stocks = new List<Stock>();
        foreach (var product in products)
        {
            foreach (var warehouse in warehouses)
            {
                stocks.Add(new Stock
                {
                    ProductId = product.Id,
                    WarehouseId = warehouse.Id,
                    Quantity = new Random().Next(50, 500)
                });
            }
        }
        await context.Stocks.AddRangeAsync(stocks);
        await context.SaveChangesAsync();

        // Seed Commissions
        var commissions = new List<Commission>
        {
            new() { TargetAmount = 50000m, Percentage = 5m, Notes = "Standard commission for sales reps" },
            new() { TargetAmount = 75000m, Percentage = 7m, Notes = "High performance commission" },
            new() { TargetAmount = 100000m, Percentage = 10m, Notes = "Elite commission tier" }
        };
        await context.Commissions.AddRangeAsync(commissions);
        await context.SaveChangesAsync();

        // Seed Employees
        var employees = new List<Employee>
        {
            new() { Name = "John Smith", Role = EmployeeRole.SalesRepresentative, Phone = "+1234567890", PasswordHash = "password123", CommissionId = commissions[0].Id },
            new() { Name = "Jane Doe", Role = EmployeeRole.SalesRepresentative, Phone = "+1234567891", PasswordHash = "password123", CommissionId = commissions[0].Id },
            new() { Name = "Mike Johnson", Role = EmployeeRole.Manager, Phone = "+1234567892", PasswordHash = "password123", CommissionId = commissions[1].Id },
            new() { Name = "Sarah Williams", Role = EmployeeRole.Worker, Phone = "+1234567893", PasswordHash = "password123", CommissionId = null },
            new() { Name = "David Brown", Role = EmployeeRole.Engineer, Phone = "+1234567894", PasswordHash = "password123", CommissionId = null },
            new() { Name = "Emily Davis", Role = EmployeeRole.Accountant, Phone = "+1234567895", PasswordHash = "password123", CommissionId = null }
        };
        await context.Employees.AddRangeAsync(employees);
        await context.SaveChangesAsync();

        // Seed EmployeeWarehouse assignments
        var employeeWarehouses = new List<EmployeeWarehouse>
        {
            new() { EmployeeId = employees[0].Id, WarehouseId = warehouses[0].Id },
            new() { EmployeeId = employees[1].Id, WarehouseId = warehouses[1].Id },
            new() { EmployeeId = employees[0].Id, WarehouseId = warehouses[2].Id },
            new() { EmployeeId = employees[2].Id, WarehouseId = warehouses[0].Id },
            new() { EmployeeId = employees[3].Id, WarehouseId = warehouses[1].Id }
        };
        await context.EmployeeWarehouses.AddRangeAsync(employeeWarehouses);
        await context.SaveChangesAsync();

        // Seed Customers
        var customers = new List<Customer>
        {
            new() { Name = "Grand Hotel", Type = CustomerType.Hotel, Address = "123 Main St", Phone = "+1987654321" },
            new() { Name = "City Restaurant", Type = CustomerType.Restaurant, Address = "456 Oak Ave", Phone = "+1987654322" },
            new() { Name = "Corner Shop", Type = CustomerType.Shop, Address = "789 Pine Rd", Phone = "+1987654323" },
            new() { Name = "Luxury Resort", Type = CustomerType.Hotel, Address = "321 Beach Blvd", Phone = "+1987654324" },
            new() { Name = "Family Diner", Type = CustomerType.Restaurant, Address = "654 Mountain Ln", Phone = "+1987654325" },
            new() { Name = "Super Mart", Type = CustomerType.Shop, Address = "987 Valley Dr", Phone = "+1987654326" }
        };
        await context.Customers.AddRangeAsync(customers);
        await context.SaveChangesAsync();

        // Seed Orders
        var orders = new List<Order>();
        for (int i = 0; i < 10; i++)
        {
            var random = new Random();
            var order = new Order
            {
                CustomerId = customers[random.Next(customers.Count)].Id,
                SalesRepId = employees[random.Next(2)].Id, // Only sales reps
                WarehouseId = warehouses[random.Next(warehouses.Count)].Id,
                Status = (OrderStatus)random.Next(4),
                TotalAmount = 0,
                CreatedAt = DateTime.UtcNow.AddDays(-random.Next(1, 30))
            };
            orders.Add(order);
        }
        await context.Orders.AddRangeAsync(orders);
        await context.SaveChangesAsync();

        // Seed OrderDetails
        var orderDetails = new List<OrderDetail>();
        foreach (var order in orders)
        {
            var random = new Random();
            var numItems = random.Next(1, 5);
            var selectedProducts = products.OrderBy(x => random.Next()).Take(numItems).ToList();
            
            foreach (var product in selectedProducts)
            {
                var quantity = random.Next(1, 10);
                var totalPrice = product.Price * quantity;
                
                orderDetails.Add(new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = quantity,
                    UnitPrice = product.Price,
                    TotalPrice = totalPrice
                });
                
                order.TotalAmount += totalPrice;
            }
        }
        await context.OrderDetails.AddRangeAsync(orderDetails);
        await context.SaveChangesAsync();
    }
}
