using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Infrastructure.Contexts;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CalorieCounter.Application.UseCases.UserCases.Queries
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, ErrorOr<User>>
    {
        private readonly UserContext _userContext;

        public GetUserInfoQueryHandler(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<ErrorOr<User>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(usr => usr.Id == request.id, cancellationToken);

            if (user is null)
                return Error.NotFound($"User.NotFound", $"Couldn't find user with id {request.id}");
            else
                return user;
        }
    }
}
