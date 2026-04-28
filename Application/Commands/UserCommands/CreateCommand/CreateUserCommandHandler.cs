using MediatR;
using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Infrastructure.Contexts;

namespace CalorieCounter.Application.Commands.UserCommands.CreateCommand
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly UserContext _userContext;

        public CreateUserCommandHandler(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new(request.name, request.age, request.gender, request.height, request.weight);
            await _userContext.AddAsync(user, cancellationToken);
            await _userContext.SaveChangesAsync(cancellationToken);
        }
    }
}
