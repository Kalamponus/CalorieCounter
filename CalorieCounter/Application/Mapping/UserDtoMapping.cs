using CalorieCounter.Application.DTO;
using CalorieCounter.Domain.AggregatesModels;

namespace CalorieCounter.Application.Mapping
{
    public static class UserDtoMapping
    {
        public static UserDto MapToDto(this User user)
        {
            return new UserDto(
                user.Id,
                user.Name,
                user.Age,
                user.Gender,
                user.Weight,
                user.Height,
                user.TargetWeight);
        }
    }
}
