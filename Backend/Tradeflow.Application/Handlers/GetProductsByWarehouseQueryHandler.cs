using MediatR;
using Tradeflow.Application.Queries;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.Application.Handlers;

public class GetProductsByWarehouseQueryHandler : IRequestHandler<GetProductsByWarehouseQuery, List<ProductStockDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsByWarehouseQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ProductStockDto>> Handle(GetProductsByWarehouseQuery request, CancellationToken cancellationToken)
    {
        var stockRepo = _unitOfWork.Repository<Stock>();
        var productRepo = _unitOfWork.Repository<Product>();
        var categoryRepo = _unitOfWork.Repository<Category>();

        var stocks = await stockRepo.GetAllAsync();
        var products = await productRepo.GetAllAsync();
        var categories = await categoryRepo.GetAllAsync();

        var result = stocks
            .Where(s => s.WarehouseId == request.WarehouseId && s.Quantity > 0)
            .Join(products, s => s.ProductId, p => p.Id, (s, p) => new { Stock = s, Product = p })
            .Join(categories, sp => sp.Product.CategoryId, c => c.Id, (sp, c) => new { sp.Stock, sp.Product, Category = c })
            .Select(x => new ProductStockDto
            {
                ProductId = x.Stock.ProductId,
                ProductName = x.Product.Name,
                Barcode = x.Product.Barcode,
                Price = x.Product.Price,
                Quantity = x.Stock.Quantity,
                CategoryName = x.Category.Name
            })
            .ToList();

        return result;
    }
}
