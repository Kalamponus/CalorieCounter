using MediatR;
using CalorieCounter.Domain.AggregatesModels;

namespace CalorieCounter.Application.Commands.UserCommands.CreateCommand
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new(request.name, request.age, request.gender, request.height, request.weight);
        }
    }
}
