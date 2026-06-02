using CalorieCounter.Domain.DomainErrors;

namespace CalorieCounter.Domain.DomainResults
{
    public class DomainResult
    {
        public bool HasErrors => Errors.Any();
        public List<DomainError> Errors { get; private set; } = [];

        public void AddError(DomainError error)
        {
            Errors.Add(error);
        }

        public void AddError(string message)
        {
            Errors.Add(new DomainError(message));
        }
    }
}
