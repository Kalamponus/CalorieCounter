using FluentValidation;

namespace CalorieCounter.Application.UseCases.UserCases.Commands.Validators
{
    public class ChangeUserTargetWeightCommandValidator : AbstractValidator<ChangeUserTargetWeightCommand>
    {
        public ChangeUserTargetWeightCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x => x.TargetWeight).GreaterThan(0).WithMessage("Target weight must be greater than zero");
        }
    }
}
