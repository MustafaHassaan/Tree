using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tradeflow.Application.Commands.Commissions;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CommissionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public CommissionsController(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Get all commissions
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<Commission>>> GetAll()
    {
        var commissions = await _unitOfWork.Repository<Commission>().GetAllAsync();
        return Ok(commissions);
    }

    /// <summary>
    /// Get commission by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Commission>> GetById(int id)
    {
        var commission = await _unitOfWork.Repository<Commission>().GetByIdAsync(id);
        if (commission == null)
            return NotFound($"Commission {id} not found");

        return Ok(commission);
    }

    /// <summary>
    /// Create a new commission
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateCommissionCommand command)
    {
        var commissionId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = commissionId }, commissionId);
    }

    /// <summary>
    /// Update an existing commission
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> Update(int id, [FromBody] UpdateCommissionCommand command)
    {
        if (id != command.Id)
            return BadRequest("Commission ID mismatch");

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete a commission
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var command = new DeleteCommissionCommand { Id = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
