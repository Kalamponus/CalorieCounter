using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.Commands.UserCommands.ChangeNameCommand
{
    public record ChangeNameCommand(
        Guid id,
        string newName) : IRequest<ErrorOr<Updated>>;
}
