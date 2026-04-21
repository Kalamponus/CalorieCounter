using CalorieCounter.Domain.Common;

namespace CalorieCounter.Domain.AggregatesModels
{
    public class User : Entity, IAggregateRoot
    {
        private const int MaxAge = 150;
        private const float MaxWeight = 500;
        private const float MaxHeight = 300;
        private const int MaxNameLength = 200;

        public string Name { get; private set; }
        public int Age { get; private set; }
        public float Weight { get; private set; }
        public float Height { get; private set; }

        public User(string name, int age, float weight, float height)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength)
                throw new DomainException("Name cannot be empty.");
            if (age <= 0 || age > MaxAge)
                throw new DomainException($"Age must be greater than zero and less than {MaxAge}.");
            if (weight <= 0 || weight > 500)
                throw new DomainException($"Weight must be greater than zero and less than {MaxWeight}.");
            if (height <= 0 || height < 300)
                throw new DomainException($"Height must be greater than zero and less than {MaxHeight}.");

            Name = name;
            Age = age;
            Weight = weight;
            Height = height;
        }
    }
}
