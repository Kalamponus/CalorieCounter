using CalorieCounter.Application.DTO;
using CalorieCounter.Domain.AggregatesModels.UserAggregate;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.UseCases.UserCases.Commands
{
    public record ChangeUserActivityLevelCommand(
        Guid Id,
        PhysicalActivityLevel PhysicalActivityLevel) : IRequest<ErrorOr<UserDto>>;
}
