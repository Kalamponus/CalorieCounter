using CalorieCounter.Application.ErrorCodes;
using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Infrastructure.Contexts;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.Commands.UserCommands.UpdateGeneralDataCommand
{
    public class UpdateUserGeneralDataCommandHandler : IRequestHandler<UpdateUserGeneralDataCommand, ErrorOr<Updated>>
    {
        private readonly UserContext _userContext;

        public UpdateUserGeneralDataCommandHandler(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<ErrorOr<Updated>> Handle(UpdateUserGeneralDataCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userContext.Users.FindAsync(request.id, cancellationToken);

            if (user is null)
                return Error.NotFound(UserErrorCodes.NotFound, $"Couldn't find user {request.id}");

            FluentResults.Result result = user.UpdateGeneralUserData(request.name, request.age, request.gender, request.height, request.weight);

            if (result.IsFailed)
            {
                List<Error> errors = result.Errors
                    .Select(e => Error.Validation(UserErrorCodes.UpdateFailed, e.Message))
                    .ToList();

                return errors;
            }

            bool areChangesSaved = await _userContext.SaveChangesAsync(cancellationToken) > 0;

            return areChangesSaved ? Result.Updated : Error.Unexpected(UserErrorCodes.Unexpected, $"Couldn't save general data changes to user {user.Id} even though the data was validated");
        }
    }
}
