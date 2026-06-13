using CalorieCounter.Domain.AggregatesModels;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.UseCases.UserCases.Queries
{
    public record GetUserInfoQuery(Guid id) : IRequest<ErrorOr<User>>;
}
