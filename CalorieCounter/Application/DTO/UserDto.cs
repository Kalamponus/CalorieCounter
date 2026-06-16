using CalorieCounter.Domain.AggregatesModels.UserAggregate;

namespace CalorieCounter.Application.DTO
{
    public record UserDto(
        Guid Id,
        string Name,
        int Age,
        Gender Gender,
        float Weight,
        float Height,
        float TargetWeight);
}
