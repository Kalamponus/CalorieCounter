using CalorieCounter.Domain.AggregatesModels;
using MediatR;

namespace CalorieCounter.Application.Queries.UserQueries.GetInfoQuery
{
    public record GetUserInfoQuery(Guid id) : IRequest<User?>;
}
