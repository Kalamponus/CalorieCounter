using MediatR;

namespace CalorieCounter.Application.Commands.UserCommands.UpdateCommand
{
    public class UpdateWeightCommandHandler : IRequestHandler<UpdateWeightCommand>
    {
        public Task Handle(UpdateWeightCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
