using FluentValidation;

namespace CalorieCounter.Application.UseCases.UserCases.Commands.Validators
{
    public class UpdateUserWeightCommandValidator : AbstractValidator<UpdateUserWeightCommand>
    {
        public UpdateUserWeightCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x => x.Weight).GreaterThan(0).WithMessage("Weight must be greater than zero");
        }
    }
}
