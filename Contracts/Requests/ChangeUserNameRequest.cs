namespace Contracts.Requests
{
    public sealed record ChangeUserNameRequest
    {
        public required string NewName { get; init; }
    }
}
