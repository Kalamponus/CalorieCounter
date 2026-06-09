using Contracts.Common;

namespace Contracts.Requests
{
    public sealed record UpdateUserGeneralDataRequest
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required int Age { get; init; }
        public required Gender Gender { get; init; }
        public required float Weight { get; init; }
        public required float Height { get; init; }
        public required float TargetWeight { get; init; }
    }
}
