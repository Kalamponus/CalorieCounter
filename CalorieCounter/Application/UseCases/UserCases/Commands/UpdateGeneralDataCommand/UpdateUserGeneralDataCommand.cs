using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Domain.AggregatesModels.UserAggregate;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.UseCases.UserCases.Commands
{
    public record UpdateUserGeneralDataCommand(
        Guid id,
        string name,
        int age,
        Gender gender,
        float weight,
        float height) : IRequest<ErrorOr<User>>;
}
