using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Infrastructure.Contexts;
using MediatR;

namespace CalorieCounter.Application.Commands.UserCommands.UpdateWeightCommand
{
    public class UpdateWeightCommandHandler : IRequestHandler<UpdateWeightCommand>
    {
        private readonly UserContext _userContext;

        public UpdateWeightCommandHandler(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task Handle(UpdateWeightCommand request, CancellationToken cancellationToken)
        {
            User user = await _userContext.Users.FindAsync(request.id, cancellationToken) ?? throw new Exception();
            user.ChangeWeight(request.weight);
            await _userContext.SaveChangesAsync(cancellationToken);
        }
    }
}
