using CalorieCounter.Domain;

namespace CalorieCounter.Application.Services
{
    public interface IDailyCaloriesCalculatorService
    {
        float CalculateDailyCalories(float weight, float height, int age, Gender gender, PhysicalActivityLevel activityLevel);
    }
}
