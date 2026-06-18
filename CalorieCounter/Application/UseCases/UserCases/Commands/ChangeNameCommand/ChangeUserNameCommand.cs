using CalorieCounter.Application.DTO;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.UseCases.UserCases.Commands
{
    public record ChangeUserNameCommand(
        Guid id,
        string name) : IRequest<ErrorOr<UserDto>>;
}
