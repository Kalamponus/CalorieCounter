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
            try
            {
                User user = new(Guid.NewGuid(), request.name, request.age, request.gender, request.height, request.weight);
                await _userContext.AddAsync(user, cancellationToken);
                int changes = await _userContext.SaveChangesAsync(cancellationToken);

                return user;
            }
            catch (DomainException e)
            {
                List<Error> errors = e.Message
                    .Split(DomainException.MessageDelimeter)
                    .Select(message => Error.Validation(description: message))
                    .ToList();

                return errors;
            }
        }
    }
}
