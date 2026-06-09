using CalorieCounter.Domain.AggregatesModels.UserAggregate;

namespace CalorieCounter.Application.DTO
{
    public record UserDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required int Age { get; set; }
        public required Gender Gender { get; set; }
        public required float Weight { get; set; }
        public required float Height { get; set; }
        public required float TargetWeight { get; set; }
    }
}
