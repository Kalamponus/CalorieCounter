using MediatR;
using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Infrastructure.Contexts;
using ErrorOr;
using CalorieCounter.Application.ErrorCodes;
using CalorieCounter.Application.DTO;
using CalorieCounter.Application.Mapping;

namespace CalorieCounter.Application.UseCases.UserCases.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<UserDto>>
    {
        private readonly UserContext _userContext;

        public CreateUserCommandHandler(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<ErrorOr<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            FluentResults.Result<User> userCreationResult = User.RegisterNewUserData(Guid.NewGuid(), request.name, request.age, request.gender, request.height, request.weight);

            if (userCreationResult.IsFailed)
            {
                List<Error> errors = userCreationResult.Errors
                    .Select(e => Error.Validation(UserErrorCodes.CreateFailed, e.Message))
                    .ToList();

                return errors;
            }

            User user = userCreationResult.Value;

            await _userContext.AddAsync(user, cancellationToken);
            int changes = await _userContext.SaveChangesAsync(cancellationToken);

            return changes > 0 ? user.MapToDto() : Error.Unexpected(UserErrorCodes.Unexpected, $"Couldn't create user {user.Id} even though the data was validated)");
        }
    }
}
