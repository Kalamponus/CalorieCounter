using CalorieCounter.Application.DTO;
using Contracts.Responses;

namespace CalorieCounter.Api.Mapping
{
    public static class UserMapping
    {
        public static UserResponse MapToResponse(this UserDto user)
        {
            return new UserResponse()
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age,
                Gender = (Contracts.Common.Gender)user.Gender,
                Height = user.Height,
                Weight = user.Weight,
                TargetWeight = user.TargetWeight,
                PhysicalActivityLevel = (Contracts.Common.PhysicalActivityLevel)user.PhysicalActivityLevel,
            };
        }
    }
}
