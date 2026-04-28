using CalorieCounter.Domain.AggregatesModels;
using MediatR;

namespace CalorieCounter.Application.Queries.UserQueries.GetInfoQuery
{
    public record GetUserInfoQuery(long id) : IRequest<User>;
}
