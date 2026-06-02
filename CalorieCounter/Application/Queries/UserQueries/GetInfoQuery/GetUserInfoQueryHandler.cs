using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CalorieCounter.Application.Queries.UserQueries.GetInfoQuery
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, User?>
    {
        private readonly UserContext _userContext;

        public GetUserInfoQueryHandler(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<User?> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(usr => usr.Id == request.id, cancellationToken);

            return user;
        }
    }
}
