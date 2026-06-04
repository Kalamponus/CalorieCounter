using MediatR;
using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Infrastructure.Contexts;
using CalorieCounter.Domain.Common;
using ErrorOr;

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
            FluentResults.Result<User> userCreationResult = User.RegisterNewUserData(Guid.NewGuid(), request.name, request.age, request.gender, request.height, request.weight);

            if (userCreationResult.IsFailed)
            {
                List<Error> errors = userCreationResult.Errors
                    .Select(e => Error.Validation("User.NotCreated", e.Message))
                    .ToList();

                return errors;
            }

            User user = userCreationResult.Value;

            await _userContext.AddAsync(user, cancellationToken);
            int changes = await _userContext.SaveChangesAsync(cancellationToken);

            return user;
        }
    }
}
