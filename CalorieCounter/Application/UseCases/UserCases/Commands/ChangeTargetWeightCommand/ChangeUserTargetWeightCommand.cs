using CalorieCounter.Application.DTO;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.UseCases.UserCases.Commands
{
    public record ChangeUserTargetWeightCommand(
        Guid id,
        float targetWeight) : IRequest<ErrorOr<UserDto>>;
}
