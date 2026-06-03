using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Infrastructure.Contexts;
using MediatR;

namespace CalorieCounter.Application.Commands.UserCommands.UpdateWeightCommand
{
    public class UpdateWeightCommandHandler : IRequestHandler<UpdateWeightCommand, bool>
    {
        private readonly UserContext _userContext;

        public UpdateWeightCommandHandler(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<bool> Handle(UpdateWeightCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userContext.Users.FindAsync(request.id, cancellationToken);

            if (user is null)
                return false;

            FluentResults.Result result = user.ChangeWeight(request.weight);

            if (result.IsFailed)
                return false;

            return await _userContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
