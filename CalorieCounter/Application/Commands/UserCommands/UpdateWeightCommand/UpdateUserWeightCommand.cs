using CalorieCounter.Domain.AggregatesModels;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.Commands.UserCommands
{
    public record UpdateUserWeightCommand(
        Guid id,
        float weight) : IRequest<ErrorOr<User>>;
}
