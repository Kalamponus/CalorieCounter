using MediatR;
using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Infrastructure.Contexts;
using ErrorOr;
using CalorieCounter.Application.ErrorCodes;
using Microsoft.EntityFrameworkCore;

namespace CalorieCounter.Application.Commands.UserCommands.CreateCommand
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<User>>
    {
        private readonly UserContext _userContext;

        public CreateUserCommandHandler(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<ErrorOr<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (user is not null)
                return Error.Conflict("User.AlreadyExists", $"User {user.Id} already exists");

            FluentResults.Result<User> userCreationResult = User.RegisterNewUserData(Guid.NewGuid(), request.name, request.age, request.gender, request.height, request.weight);

            if (userCreationResult.IsFailed)
            {
                List<Error> errors = userCreationResult.Errors
                    .Select(e => Error.Validation(UserErrorCodes.CreateFailed, e.Message))
                    .ToList();

                return errors;
            }

            user = userCreationResult.Value;

            await _userContext.AddAsync(user, cancellationToken);
            int changes = await _userContext.SaveChangesAsync(cancellationToken);

            return changes > 0 ? user : Error.Unexpected(UserErrorCodes.Unexpected, $"Couldn't create user {user.Id} even though the data was validated)");
        }
    }
}
