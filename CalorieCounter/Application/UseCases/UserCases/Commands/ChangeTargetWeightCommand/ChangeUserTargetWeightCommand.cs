using CalorieCounter.Application.DTO;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.UseCases.UserCases.Commands
{
    public record ChangeUserTargetWeightCommand(
        Guid Id,
        float TargetWeight) : IRequest<ErrorOr<UserDto>>;
}
