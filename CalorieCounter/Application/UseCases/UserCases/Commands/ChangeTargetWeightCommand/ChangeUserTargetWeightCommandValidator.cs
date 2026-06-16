using FluentValidation;

namespace CalorieCounter.Application.UseCases.UserCases.Commands.Validators
{
    public class ChangeUserTargetWeightCommandValidator : AbstractValidator<ChangeUserTargetWeightCommand>
    {
        public ChangeUserTargetWeightCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x => x.targetWeight).GreaterThan(0).WithMessage("Target weight must be greater than zero");
        }
    }
}
