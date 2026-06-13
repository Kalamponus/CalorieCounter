using CalorieCounter.Domain.AggregatesModels;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.UseCases.UserCases.Commands
{
    public record UpdateUserWeightCommand(
        Guid id,
        float weight) : IRequest<ErrorOr<User>>;
}
