using CalorieCounter.Domain.Common;

namespace CalorieCounter.Domain.AggregatesModels
{
    public class User : Entity, IAggregateRoot
    {
        public const int MaxAge = 150;
        public const float MaxWeight = 500;
        public const float MaxHeight = 300;
        public const int MaxNameLength = 200;
        public const float MinHealthyBodyMassIndex = 18.5f;

        public string Name { get; private set; }
        public int Age { get; private set; }
        public Gender Gender { get; private set; }
        public float Weight { get; private set; }
        public float Height { get; private set; }
        public float TargetWeight { get; private set; }

        public User(string name, int age, Gender gender, float height, float weight)
        {
            SetName(name);
            SetAge(age);
            SetHeight(height);
            SetWeight(weight);
            Gender = gender;
        }

        public void SetName(string name)
        {
            name = name.Trim();

            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name cannot be empty.");

            if (name.Length > MaxNameLength)
                throw new DomainException($"Name cannot exceed {MaxNameLength} characters.");

            Name = name;
        }

        public void SetAge(int age)
        {
            if (age <= 0 || age > MaxAge)
                throw new DomainException($"Age must be greater than zero and less than or equal to {MaxAge}.");

            Age = age;
        }

        public void SetGender(Gender gender)
        {
            Gender = gender;
        }

        public void SetHeight(float height)
        {
            if (height <= 0 || height > MaxHeight)
                throw new DomainException($"Height must be greater than zero and less than or equal to {MaxHeight}.");

            Height = height;
        }

        public void SetWeight(float weight)
        {
            ValidateWeight(weight);
            Weight = weight;
        }

        public void SetTargetWeight(float targetWeight)
        {
            ValidateWeight(targetWeight);

            float heightInMeters = Height / 100f;
            float bmi = targetWeight / (heightInMeters * heightInMeters);

            if (bmi < MinHealthyBodyMassIndex)
                throw new DomainException("Target weight must be healthy.");

            TargetWeight = targetWeight;
        }

        private void ValidateWeight(float weight)
        {
            if (weight <= 0 || weight > MaxWeight)
                throw new DomainException($"Weight must be greater than zero and less than or equal to {MaxWeight}.");
        }
    }
}
