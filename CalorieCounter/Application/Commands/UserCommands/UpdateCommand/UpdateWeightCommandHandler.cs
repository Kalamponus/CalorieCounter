using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Infrastructure.Contexts;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.Commands.UserCommands.UpdateWeightCommand
{
    public class UpdateWeightCommandHandler : IRequestHandler<UpdateWeightCommand, ErrorOr<Updated>>
    {
        private readonly UserContext _userContext;

        public UpdateWeightCommandHandler(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<ErrorOr<Updated>> Handle(UpdateWeightCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userContext.Users.FindAsync(request.id, cancellationToken);

            if (user is null)
                return Error.NotFound();

            FluentResults.Result result = user.ChangeWeight(request.weight);

            if (result.IsFailed)
            {
                List<Error> errors = result.Errors
                    .Select(e => Error.Validation(description: e.Message))
                    .ToList();

                return errors;
            }

            bool areChangesSaved = await _userContext.SaveChangesAsync(cancellationToken) > 0;

            return areChangesSaved ? Result.Updated : Error.Unexpected();
        }
    }
}
