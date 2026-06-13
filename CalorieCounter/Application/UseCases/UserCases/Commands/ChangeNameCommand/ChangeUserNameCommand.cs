using CalorieCounter.Domain.AggregatesModels;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.UseCases.UserCases.Commands
{
    public record ChangeUserNameCommand(
        Guid id,
        string newName) : IRequest<ErrorOr<User>>;
}
