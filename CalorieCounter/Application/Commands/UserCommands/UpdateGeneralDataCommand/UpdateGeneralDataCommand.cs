using CalorieCounter.Domain.AggregatesModels.UserAggregate;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Application.Commands.UserCommands.UpdateGeneralDataCommand
{
    public record UpdateGeneralDataCommand(
        Guid id,
        string name,
        int age,
        Gender gender,
        float weight,
        float height) : IRequest<ErrorOr<Updated>>;
}
