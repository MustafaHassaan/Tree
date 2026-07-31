using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tradeflow.Application.Commands.Employees;
using Tradeflow.Application.Queries;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public EmployeesController(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Get all employees
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetAll()
    {
        var employees = await _unitOfWork.Repository<Employee>().GetAllAsync();
        return Ok(employees);
    }

    /// <summary>
    /// Get employee by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetById(int id)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(id);
        if (employee == null)
            return NotFound($"Employee {id} not found");

        return Ok(employee);
    }

    /// <summary>
    /// Create a new employee
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateEmployeeCommand command)
    {
        var employeeId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = employeeId }, employeeId);
    }

    /// <summary>
    /// Update an existing employee
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> Update(int id, [FromBody] UpdateEmployeeCommand command)
    {
        if (id != command.Id)
            return BadRequest("Employee ID mismatch");

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete an employee
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var command = new DeleteEmployeeCommand { Id = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Get sales representative performance metrics
    /// </summary>
    [HttpGet("{salesRepId}/performance")]
    public async Task<ActionResult<SalesRepPerformanceDetailDto>> GetSalesRepPerformance(int salesRepId)
    {
        var query = new GetSalesRepPerformanceQuery { SalesRepId = salesRepId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
