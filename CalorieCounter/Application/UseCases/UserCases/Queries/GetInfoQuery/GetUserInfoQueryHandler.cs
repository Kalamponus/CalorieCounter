using CalorieCounter.Application.DTO;
using CalorieCounter.Application.Mapping;
using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Infrastructure.Contexts;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CalorieCounter.Application.UseCases.UserCases.Queries
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, ErrorOr<UserDto>>
    {
        private readonly UserContext _userContext;

        public GetUserInfoQueryHandler(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<ErrorOr<UserDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(usr => usr.Id == request.Id, cancellationToken);

            if (user is null)
                return Error.NotFound($"User.NotFound", $"Couldn't find user with id {request.Id}");
            else
                return user.MapToDto();
        }
    }
}
