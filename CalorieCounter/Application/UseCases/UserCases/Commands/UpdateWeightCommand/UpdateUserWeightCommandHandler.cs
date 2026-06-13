using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Infrastructure.Contexts;
using CalorieCounter.Application.ErrorCodes;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.UseCases.UserCases.Commands
{
    public class UpdateUserWeightCommandHandler : IRequestHandler<UpdateUserWeightCommand, ErrorOr<User>>
    {
        private readonly UserContext _userContext;

        public UpdateUserWeightCommandHandler(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<ErrorOr<User>> Handle(UpdateUserWeightCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userContext.Users.FindAsync(request.id, cancellationToken);

            if (user is null)
                return Error.NotFound(UserErrorCodes.NotFound, $"Couldn't find user {request.id}");

            FluentResults.Result result = user.UpdateCurrentWeight(request.weight);

            if (result.IsFailed)
            {
                List<Error> errors = result.Errors
                    .Select(e => Error.Validation(UserErrorCodes.UpdateFailed, e.Message))
                    .ToList();

                return errors;
            }

            bool areChangesSaved = await _userContext.SaveChangesAsync(cancellationToken) > 0;

            return areChangesSaved ? user : Error.Unexpected(UserErrorCodes.Unexpected, $"Couldn't save weight changes to user {user.Id} even though the data was validated");
        }
    }
}
