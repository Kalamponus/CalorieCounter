using CalorieCounter.Application.DTO;
using CalorieCounter.Application.ErrorCodes;
using CalorieCounter.Application.Mapping;
using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Infrastructure.Contexts;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.UseCases.UserCases.Commands
{
    public class ChangeUserTargetWeightCommandHandler : IRequestHandler<ChangeUserTargetWeightCommand, ErrorOr<UserDto>>
    {
        private readonly UserContext _userContext;

        public ChangeUserTargetWeightCommandHandler(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<ErrorOr<UserDto>> Handle(ChangeUserTargetWeightCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userContext.Users.FindAsync(request.Id, cancellationToken);

            if (user is null)
                return Error.NotFound(UserErrorCodes.NotFound, $"Couldn't find user {request.Id}");

            FluentResults.Result result = user.SetTargetWeight(request.TargetWeight);

            if (result.IsFailed)
            {
                List<Error> errors = result.Errors
                    .Select(e => Error.Validation(UserErrorCodes.UpdateFailed, e.Message))
                    .ToList();

                return errors;
            }

            bool areChangesSaved = await _userContext.SaveChangesAsync(cancellationToken) > 0;

            return areChangesSaved ? user.MapToDto() : Error.Unexpected(UserErrorCodes.Unexpected, $"Couldn't save target weight changes to user {user.Id} even though the data was validated");
        }
    }
}
