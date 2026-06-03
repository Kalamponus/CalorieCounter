using MediatR;
using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Infrastructure.Contexts;
using CalorieCounter.Domain.Common;

namespace CalorieCounter.Application.Commands.UserCommands.CreateCommand
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User?>
    {
        private readonly UserContext _userContext;

        public CreateUserCommandHandler(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<User?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = new(Guid.NewGuid(), request.name, request.age, request.gender, request.height, request.weight);
                await _userContext.AddAsync(user, cancellationToken);
                int changes = await _userContext.SaveChangesAsync(cancellationToken);
                
                return user;
            }
            catch (DomainException)
            {
                return null;
            }
        }
    }
}
