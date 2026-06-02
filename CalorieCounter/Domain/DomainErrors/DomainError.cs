namespace CalorieCounter.Domain.DomainErrors
{
    public readonly record struct DomainError
    {
        public string Message { get; init; }

        public DomainError(string message = "Business rules are not satisfied by operation's data.")
        {
            Message = message;
        }
    }
}
