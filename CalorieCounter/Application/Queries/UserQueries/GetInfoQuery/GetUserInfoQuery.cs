using CalorieCounter.Domain.AggregatesModels;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.Queries.UserQueries.GetInfoQuery
{
    public record GetUserInfoQuery(Guid id) : IRequest<ErrorOr<User>>;
}
