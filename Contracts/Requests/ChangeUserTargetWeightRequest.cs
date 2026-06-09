namespace Contracts.Requests
{
    public sealed record ChangeUserTargetWeightRequest
    {
        public required float TargetWeight { get; init; }
    }
}
