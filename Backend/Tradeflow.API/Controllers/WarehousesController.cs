using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tradeflow.Application.Commands;
using Tradeflow.Application.Commands.Warehouses;
using Tradeflow.Application.Queries;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;
using static Tradeflow.Application.Queries.GetProductsByWarehouseQuery;

namespace Tradeflow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WarehousesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public WarehousesController(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Get all warehouses
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<Warehouse>>> GetAll()
    {
        var warehouses = await _unitOfWork.Repository<Warehouse>().GetAllAsync();
        return Ok(warehouses);
    }

    /// <summary>
    /// Get warehouse by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Warehouse>> GetById(int id)
    {
        var warehouse = await _unitOfWork.Repository<Warehouse>().GetByIdAsync(id);
        if (warehouse == null)
            return NotFound($"Warehouse {id} not found");

        return Ok(warehouse);
    }

    /// <summary>
    /// Create a new warehouse
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateWarehouseCommand command)
    {
        var warehouseId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = warehouseId }, warehouseId);
    }

    /// <summary>
    /// Update an existing warehouse
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> Update(int id, [FromBody] UpdateWarehouseCommand command)
    {
        if (id != command.Id)
            return BadRequest("Warehouse ID mismatch");

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete a warehouse
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var command = new DeleteWarehouseCommand { Id = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Get available products and quantities for a specific warehouse
    /// </summary>
    [HttpGet("{warehouseId}/products")]
    public async Task<ActionResult<List<ProductStockDto>>> GetProductsByWarehouse(int warehouseId)
    {
        var query = new GetProductsByWarehouseQuery { WarehouseId = warehouseId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Assign an employee to a warehouse
    /// </summary>
    [HttpPost("assign-employee")]
    public async Task<ActionResult<bool>> AssignEmployeeToWarehouse([FromBody] AssignEmployeeToWarehouseCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
