using CalorieCounter.Domain.AggregatesModels;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.Commands.UserCommands
{
    public record ChangeUserTargetWeightCommand(
        Guid id,
        float targetWeight) : IRequest<ErrorOr<User>>;
}
