using CalorieCounter.Domain.AggregatesModels.UserAggregate;
using CalorieCounter.Domain.Common;
using FluentResults;

namespace CalorieCounter.Domain.AggregatesModels
{
    public class User : Entity, IAggregateRoot
    {
        public const int MaxAge = 150;
        public const float MaxWeight = 500;
        public const float MaxHeight = 300;
        public const float MinHealthyBodyMassIndex = 18.5f;

        public string Name { get; private set; }
        public int Age { get; private set; }
        public Gender Gender { get; private set; }
        public float Weight { get; private set; }
        public float Height { get; private set; }
        public PhysicalActivityLevel PhysicalActivityLevel { get; private set; }
        public float TargetWeight { get; private set; }

        public static Result<User> RegisterNewUserData(Guid id, string name, int age, Gender gender, float height, float weight)
        {
            List<IError> errors = ValidateData(name, age, gender, height, weight);

            if (errors.Count > 0)
                return Result.Fail(errors);

            User user = new(id, name, age, gender, height, weight);

            return Result.Ok(user);
        }

        private User(Guid id, string name, int age, Gender gender, float height, float weight, PhysicalActivityLevel physicalActivityLevel = PhysicalActivityLevel.None) : base(id)
        {
            Name = name;
            Age = age;
            Gender = gender;
            Height = height;
            Weight = weight;
        }

        public Result UpdateGeneralUserData(string name, int age, Gender gender, float height, float weight, PhysicalActivityLevel physicalActivityLevel)
        {
            List<IError> errors = ValidateData(name, age, gender, height, weight);

            if (errors.Count > 0)
                return Result.Fail(errors);

            Name = name;
            Age = age;
            Gender = gender;
            Height = height;
            Weight = weight;
            PhysicalActivityLevel = physicalActivityLevel;

            return Result.Ok();
        }

        public Result ChangeName(string newName)
        {
            if (ValidateName(newName, out string errorMessage))
            {
                Name = newName;
                return Result.Ok();
            }

            return Result.Fail(errorMessage);
        }

        public Result UpdateCurrentWeight(float weight)
        {
            if (ValidateWeight(weight, out string errorMessage))
            {
                Weight = weight;
                return Result.Ok();
            }

            return Result.Fail(errorMessage);
        }

        public Result SetTargetWeight(float targetWeight)
        {
            List<IError> errors = [];

            if (!ValidateWeight(targetWeight, out string errorMessage))
                errors.Add(new Error(errorMessage));

            float heightInMeters = Height / 100f;
            float bmi = targetWeight / (heightInMeters * heightInMeters);

            if (bmi < MinHealthyBodyMassIndex)
                errors.Add(new Error("Target weight must be healthy."));

            if (errors.Count > 0)
                return Result.Fail(errors);

            TargetWeight = targetWeight;

            return Result.Ok();
        }

        public Result ChangePhysicalActivity(PhysicalActivityLevel physicalActivityLevel)
        {
            PhysicalActivityLevel = physicalActivityLevel;
            return Result.Ok();
        }

        private static List<IError> ValidateData(string name, int age, Gender gender, float height, float weight)
        {
            List<IError> errors = [];

            if (!ValidateName(name, out string errorMessage))
                errors.Add(new Error(errorMessage));

            if (!ValidateAge(age, out errorMessage))
                errors.Add(new Error(errorMessage));

            if (!ValidateHeight(height, out errorMessage))
                errors.Add(new Error(errorMessage));

            if (!ValidateWeight(weight, out errorMessage))
                errors.Add(new Error(errorMessage));

            return errors;
        }

        private static bool ValidateName(string name, out string errorMessage)
        {
            name = name.Trim();
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(name))
            {
                errorMessage = "Name cannot be empty.";
                return false;
            }
            
            return true;
        }

        private static bool ValidateAge(int age, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (age <= 0 || age > MaxAge)
            {
                errorMessage = $"Age must be greater than zero and less than or equal to {MaxAge}.";
                return false;
            }

            return true;
        }

        private static bool ValidateHeight(float height, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (height <= 0 || height > MaxHeight)
            {
                errorMessage = $"Height must be greater than zero and less than or equal to {MaxHeight}.";
                return false;
            }

            return true;
        }

        private static bool ValidateWeight(float weight, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (weight <= 0 || weight > MaxWeight)
            {
                errorMessage = $"Weight must be greater than zero and less than or equal to {MaxWeight}.";
                return false;
            }

            return true;
        }
    }
}
