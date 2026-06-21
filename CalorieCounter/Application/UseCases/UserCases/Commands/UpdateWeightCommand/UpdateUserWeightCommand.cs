using CalorieCounter.Application.DTO;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.UseCases.UserCases.Commands
{
    public record UpdateUserWeightCommand(
        Guid Id,
        float Weight) : IRequest<ErrorOr<UserDto>>;
}
