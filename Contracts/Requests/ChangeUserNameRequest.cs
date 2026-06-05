namespace Contracts.Requests
{
    public sealed record ChangeUserNameRequest
    {
        public required Guid Id { get; init; }
        public required string NewName { get; init; }
    }
}
