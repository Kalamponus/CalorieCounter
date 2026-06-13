using CalorieCounter.Application.DTO;
using FluentValidation;

namespace CalorieCounter.Application.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(user => user.Id).NotEqual(Guid.Empty);
            RuleFor(user => user.Name).NotNull().NotEmpty();
            RuleFor(user => user.Age).NotEmpty();
            RuleFor(user => user.Height).NotEmpty();
            RuleFor(user => user.Weight).NotEmpty();
            RuleFor(user => user.TargetWeight).NotEmpty();
        }
    }
}
