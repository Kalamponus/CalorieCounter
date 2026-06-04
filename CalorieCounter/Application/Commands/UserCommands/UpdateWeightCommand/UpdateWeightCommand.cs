using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.Commands.UserCommands.UpdateWeightCommand
{
    public record UpdateWeightCommand(
        Guid id,
        float weight) : IRequest<ErrorOr<Updated>>;
}
