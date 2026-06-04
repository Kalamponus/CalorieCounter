using CalorieCounter.Application.ErrorCodes;
using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Infrastructure.Contexts;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.Commands.UserCommands.ChangeNameCommand
{
    public class ChangeUserNameCommandHandler : IRequestHandler<ChangeUserNameCommand, ErrorOr<Updated>>
    {
        private readonly UserContext _userContext;

        public ChangeUserNameCommandHandler(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<ErrorOr<Updated>> Handle(ChangeUserNameCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userContext.Users.FindAsync(request.id, cancellationToken);

            if (user is null)
                return Error.NotFound(UserErrorCodes.NotFound, $"Couldn't find user {request.id}");

            FluentResults.Result result = user.ChangeName(request.newName);

            if (result.IsFailed)
            {
                List<Error> errors = result.Errors
                    .Select(e => Error.Validation(UserErrorCodes.UpdateFailed, e.Message))
                    .ToList();
                return errors;
            }

            bool areChangesSaved = await _userContext.SaveChangesAsync(cancellationToken) > 0;

            return areChangesSaved ? Result.Updated : Error.Unexpected(UserErrorCodes.Unexpected, $"Couldn't save name changes to user {user.Id} even though the data was validated");
        }
    }
}
