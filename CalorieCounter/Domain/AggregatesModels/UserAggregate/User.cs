using CalorieCounter.Domain.AggregatesModels.UserAggregate;
using CalorieCounter.Domain.Common;
using ErrorOr;
using System.ComponentModel;

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

        public User(Guid id, string name, int age, Gender gender, float height, float weight) : base(id)
        {
            ChangeName(name);
            ChangeAge(age);
            ChangeHeight(height);
            ChangeWeight(weight);
            Gender = gender;
        }

        public ErrorOr<Updated> ChangeName(string name)
        {
            name = name.Trim();
            List<Error> errors = [];

            if (string.IsNullOrWhiteSpace(name))
                errors.Add(Error.Validation(description: "Name cannot be empty."));
                
            if (name.Length > MaxNameLength)
                errors.Add(Error.Validation(description: $"Name cannot exceed {MaxNameLength} characters."));

            if (errors.Count > 0)
                return errors;

            Name = name;

            return Result.Updated;
        }

        public ErrorOr<Updated> ChangeAge(int age)
        {
            if (age <= 0 || age > MaxAge)
                return Error.Validation(description: $"Age must be greater than zero and less than or equal to {MaxAge}.");

            Age = age;

            return Result.Updated;
        }

        public ErrorOr<Updated> ChangeGender(Gender gender)
        {
            Gender = gender;

            return Result.Updated;
        }

        public ErrorOr<Updated> ChangeHeight(float height)
        {
            if (height <= 0 || height > MaxHeight)
                return Error.Validation(description: $"Height must be greater than zero and less than or equal to {MaxHeight}.");

            Height = height;

            return Result.Updated;
        }

        public ErrorOr<Updated> ChangeWeight(float weight)
        {
            ErrorOr<Updated> result = ValidateWeight(weight);

            if (!result.IsError)
                Weight = weight;

            return result;
        }

        public ErrorOr<Updated> SetTargetWeight(float targetWeight)
        {
            List<Error> errors = ValidateWeight(targetWeight);

            float heightInMeters = Height / 100f;
            float bmi = targetWeight / (heightInMeters * heightInMeters);

            if (bmi < MinHealthyBodyMassIndex)
                throw new DomainException("Target weight must be healthy.");

            TargetWeight = targetWeight;

            return Result.Updated;
        }

        private List<Error> ValidateWeight(float weight)
        {
            List<Error> errors = [];

            if (weight <= 0 || weight > MaxWeight)
                errors.Add(Error.Validation($"Weight must be greater than zero and less than or equal to {MaxWeight}."));

            return errors;
        }
    }
}
