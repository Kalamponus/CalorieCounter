using CalorieCounter.Domain;
using CalorieCounter.Domain.AggregatesModels;
using MediatR;

namespace CalorieCounter.Application.Commands.UserCommands.CreateCommand
{
    public record CreateUserCommand(
        string name,
        int age,
        Gender gender,
        float weight,
        float height) : IRequest<User>;
}
