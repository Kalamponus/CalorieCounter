using CalorieCounter.Application.DTO;
using CalorieCounter.Application.ErrorCodes;
using CalorieCounter.Application.Mapping;
using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Infrastructure.Contexts;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.UseCases.UserCases.Commands
{
    public class ChangeUserActivityLevelCommandHandler : IRequestHandler<ChangeUserActivityLevelCommand, ErrorOr<UserDto>>
    {
        private readonly UserContext _userContext;

        public ChangeUserActivityLevelCommandHandler(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<ErrorOr<UserDto>> Handle(ChangeUserActivityLevelCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userContext.Users.FindAsync(request.Id, cancellationToken);

            if (user is null)
                return Error.NotFound(UserErrorCodes.NotFound, $"Couldn't find user {request.Id}");

            FluentResults.Result result = user.ChangePhysicalActivity(request.PhysicalActivityLevel);

            bool areChangesSaved = await _userContext.SaveChangesAsync(cancellationToken) > 0;

            return areChangesSaved ? user.MapToDto() : Error.Unexpected(UserErrorCodes.Unexpected, $"Couldn't save general data changes to user {user.Id} even though the data was validated");
        }
    }
}
