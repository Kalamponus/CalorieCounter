using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.Commands.UserCommands.UpdateWeightCommand
{
    public record UpdateWeightCommand(
        long id,
        float weight) : IRequest<ErrorOr<Updated>>;
}
