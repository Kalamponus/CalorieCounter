using CalorieCounter.Domain.AggregatesModels;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.UseCases.UserCases.Commands
{
    public record ChangeUserTargetWeightCommand(
        Guid id,
        float targetWeight) : IRequest<ErrorOr<User>>;
}
