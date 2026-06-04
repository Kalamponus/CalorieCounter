using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.Commands.UserCommands.ChangeNameCommand
{
    public record ChangeUserNameCommand(
        Guid id,
        string newName) : IRequest<ErrorOr<Updated>>;
}
