using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Domain.AggregatesModels.UserAggregate;
using Contracts.Requests;
using Contracts.Responses;

namespace CalorieCounter.Api.Mapping
{
    public static class UserMapping
    {
        public static User MapToUser(this CreateUserRequest userRequest)
        {
            return new User(Guid.NewGuid(), userRequest.Name, userRequest.Age, (Gender)userRequest.Gender, userRequest.Height, userRequest.Weight);
        }

        public static UserResponse MapToResponse(this User user)
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
            };
        }
    }
}
