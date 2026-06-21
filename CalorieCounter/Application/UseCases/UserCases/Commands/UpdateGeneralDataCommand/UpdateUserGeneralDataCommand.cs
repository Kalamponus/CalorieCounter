using CalorieCounter.Application.DTO;
using CalorieCounter.Domain.AggregatesModels.UserAggregate;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.UseCases.UserCases.Commands
{
    public record UpdateUserGeneralDataCommand(
        Guid Id,
        string Name,
        int Age,
        Gender Gender,
        float Weight,
        float Height,
        PhysicalActivityLevel PhysicalActivityLevel) : IRequest<ErrorOr<UserDto>>;
}
