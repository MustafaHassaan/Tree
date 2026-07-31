using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tradeflow.Application.Commands;
using Tradeflow.Application.Queries;
using static Tradeflow.Application.Queries.GetOrdersBySalesRepQuery;
using static Tradeflow.Application.Queries.GetOrderByIdQuery;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all orders
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
    {
        var query = new GetAllOrdersQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Create a new order with stock validation
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<int>> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var orderId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, orderId);
    }

    /// <summary>
    /// Get order by ID with full details including order items
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDetailDto>> GetOrderById(int id)
    {
        var query = new GetOrderByIdQuery { OrderId = id };
        var result = await _mediator.Send(query);
        
        if (result == null)
            return NotFound($"Order {id} not found");

        return Ok(result);
    }

    /// <summary>
    /// Update order status (Pending -> Approved -> Completed)
    /// </summary>
    [HttpPut("{orderId}/status")]
    public async Task<ActionResult<bool>> UpdateOrderStatus(int orderId, [FromBody] UpdateOrderStatusCommand command)
    {
        if (orderId != command.OrderId)
            return BadRequest("Order ID mismatch");

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Get orders for a specific sales representative
    /// </summary>
    [HttpGet("sales-rep/{salesRepId}")]
    public async Task<ActionResult<List<OrderDto>>> GetOrdersBySalesRep(int salesRepId)
    {
        var query = new GetOrdersBySalesRepQuery { SalesRepId = salesRepId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
