using MediatR;

namespace CalorieCounter.Application.Commands.UserCommands.UpdateCommand
{
    public record UpdateWeightCommand(
        long id,
        float weight) : IRequest;
}
