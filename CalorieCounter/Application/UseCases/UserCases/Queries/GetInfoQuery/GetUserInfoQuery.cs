using CalorieCounter.Application.DTO;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.UseCases.UserCases.Queries
{
    public record GetUserInfoQuery(Guid id) : IRequest<ErrorOr<UserDto>>;
}
