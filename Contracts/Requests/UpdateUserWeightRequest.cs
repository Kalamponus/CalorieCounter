namespace Contracts.Requests
{
    public sealed record UpdateUserWeightRequest
    {
        public required float Weight { get; init; }
    }
}
