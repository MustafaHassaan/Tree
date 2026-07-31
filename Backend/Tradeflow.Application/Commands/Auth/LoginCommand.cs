using MediatR;

namespace Tradeflow.Application.Commands.Auth;

public record LoginCommand : IRequest<LoginResponseDto>
{
    public string Phone { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

public record LoginResponseDto
{
    public string Token { get; init; } = string.Empty;
    public int EmployeeId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public int? WarehouseId { get; init; }
}
