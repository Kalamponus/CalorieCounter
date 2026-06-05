using CalorieCounter.Domain.AggregatesModels;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.Commands.UserCommands
{
    public record ChangeUserNameCommand(
        Guid id,
        string newName) : IRequest<ErrorOr<User>>;
}
