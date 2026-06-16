using FluentValidation;

namespace CalorieCounter.Application.UseCases.UserCases.Commands.Validators
{
    public class ChangeUserNameCommandValidator : AbstractValidator<ChangeUserNameCommand>
    {
        public ChangeUserNameCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x => x.name).NotEmpty().WithMessage("Name cannot be empty");
        }
    }
}
