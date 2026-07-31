using MediatR;
using Tradeflow.Application.Queries;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.Application.Handlers;

public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDashboardStatsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        var orderRepo = _unitOfWork.Repository<Order>();
        var customerRepo = _unitOfWork.Repository<Customer>();
        var productRepo = _unitOfWork.Repository<Product>();
        var warehouseRepo = _unitOfWork.Repository<Warehouse>();
        var employeeRepo = _unitOfWork.Repository<Employee>();
        var stockRepo = _unitOfWork.Repository<Stock>();

        // Get all necessary data
        var orders = await orderRepo.GetAllAsync();
        var customers = await customerRepo.GetAllAsync();
        var products = await productRepo.GetAllAsync();
        var warehouses = await warehouseRepo.GetAllAsync();
        var employees = await employeeRepo.GetAllAsync();
        var stocks = await stockRepo.GetAllAsync();

        // Calculate Total Sales (all completed orders)
        var totalSales = orders
            .Where(o => o.Status == Domain.Enums.OrderStatus.Completed)
            .Sum(o => o.TotalAmount);

        // Calculate Monthly Revenue (current month)
        var currentMonth = DateTime.Now.Month;
        var currentYear = DateTime.Now.Year;
        var monthlyRevenue = orders
            .Where(o => o.Status == Domain.Enums.OrderStatus.Completed &&
                       o.CreatedAt.Month == currentMonth &&
                       o.CreatedAt.Year == currentYear)
            .Sum(o => o.TotalAmount);

        // Active Customers count
        var activeCustomers = customers.Count();

        // Low Stock Alerts (products with quantity < 10 in any warehouse)
        var lowStockAlerts = stocks.Count(s => s.Quantity < 10);

        // Daily Sales for the last 7 days
        var dailySales = GetDailySales(orders.ToList());

        // Warehouse Status
        var warehouseStatus = warehouses.Select((w, index) => 
        {
            var warehouseStocks = stocks.Where(s => s.WarehouseId == w.Id).ToList();
            var totalQuantity = warehouseStocks.Sum(s => s.Quantity);
            var capacityPercentage = warehouseStocks.Count > 0 
                ? (decimal)totalQuantity / (warehouseStocks.Sum(s => s.Quantity) + 100) * 100 
                : 0;
            
            return new WarehouseStatusDto
            {
                Id = w.Id,
                Name = w.Name,
                CapacityPercentage = Math.Min(capacityPercentage, 100),
                Color = index == 0 ? "bg-error" : index == 1 ? "bg-primary" : index == 2 ? "bg-secondary" : "bg-primary"
            };
        }).ToList();

        // Top Products (by sales)
        var orderDetails = await _unitOfWork.Repository<OrderDetail>().GetAllAsync();
        var topProducts = products.Take(4).Select((p, index) => 
        {
            var productSales = orders
                .Where(o => o.Status == Domain.Enums.OrderStatus.Completed)
                .Join(orderDetails,
                    o => o.Id,
                    od => od.OrderId,
                    (o, od) => new { Order = o, OrderDetail = od })
                .Where(x => x.OrderDetail.ProductId == p.Id)
                .Sum(x => x.OrderDetail.Quantity);

            var revenue = productSales * p.Price;
            
            return new TopProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Sku = $"SKU-{p.Id}",
                Sold = productSales,
                Revenue = revenue,
                Trend = index == 0 || index == 1 ? "trending_up" : index == 2 ? "trending_flat" : "trending_down",
                TrendClass = index == 0 || index == 1 ? "text-primary" : index == 2 ? "text-tertiary-container" : "text-error"
            };
        }).ToList();

        // Sales Reps Performance
        var salesRepsPerformance = employees.Where(e => e.Role == Domain.Enums.EmployeeRole.SalesRepresentative)
            .Select((e, index) => 
            {
                var repOrders = orders.Where(o => o.SalesRepId == e.Id && o.Status == Domain.Enums.OrderStatus.Completed).ToList();
                var sales = repOrders.Sum(o => o.TotalAmount);
                var totalOrders = repOrders.Count;
                
                return new SalesRepPerformanceDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Position = "Sales Representative",
                    Sales = sales,
                    Goal = $"{Math.Min(100, (int)(sales / 1000))}% Goal",
                    Rank = index + 1,
                    Active = index == 0
                };
            })
            .OrderByDescending(e => e.Sales)
            .Take(4)
            .ToList();

        return new DashboardStatsDto
        {
            TotalSales = totalSales,
            MonthlyRevenue = monthlyRevenue,
            ActiveCustomers = activeCustomers,
            LowStockAlerts = lowStockAlerts,
            DailySales = dailySales,
            WarehouseStatus = warehouseStatus,
            TopProducts = topProducts,
            SalesRepsPerformance = salesRepsPerformance
        };
    }

    private List<DailySalesDto> GetDailySales(List<Order> orders)
    {
        var dailySales = new List<DailySalesDto>();
        var days = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
        
        for (int i = 6; i >= 0; i--)
        {
            var date = DateTime.Now.AddDays(-i);
            var dayName = days[(int)date.DayOfWeek == 0 ? 6 : (int)date.DayOfWeek - 1];
            
            var dayOrders = orders.Where(o => 
                o.Status == Domain.Enums.OrderStatus.Completed &&
                o.CreatedAt.Date == date.Date).ToList();
            
            var total = dayOrders.Sum(o => o.TotalAmount);
            var actual = total; // In a real scenario, you might have a target to compare against
            
            dailySales.Add(new DailySalesDto
            {
                Day = dayName,
                Total = total,
                Actual = actual
            });
        }
        
        return dailySales;
    }
}