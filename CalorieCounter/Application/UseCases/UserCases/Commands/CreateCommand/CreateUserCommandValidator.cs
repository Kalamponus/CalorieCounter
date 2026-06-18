using FluentValidation;

namespace CalorieCounter.Application.UseCases.UserCases.Commands.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.age).GreaterThan(0).WithMessage("Age must be greater than zero");
            RuleFor(x => x.weight).GreaterThan(0).WithMessage("Weight must be greater than zero");
            RuleFor(x => x.height).GreaterThan(0).WithMessage("Height must be greater than zero");
            RuleFor(x => x.gender).IsInEnum().WithMessage(x => $"Cannot identify the gender by number {(int)x.gender}");
        }
    }
}
