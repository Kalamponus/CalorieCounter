using FluentValidation;

namespace CalorieCounter.Application.UseCases.UserCases.Commands.Validators
{
    public class UpdateUserGeneralDataCommandValidator : AbstractValidator<UpdateUserGeneralDataCommand>
    {
        public UpdateUserGeneralDataCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.Age).GreaterThan(0).WithMessage("Age must be greater than zero");
            RuleFor(x => x.Weight).GreaterThan(0).WithMessage("Weight must be greater than zero");
            RuleFor(x => x.Height).GreaterThan(0).WithMessage("Height must be greater than zero");
            RuleFor(x => x.Gender).IsInEnum().WithMessage(x => $"Unknown gender index: {(int)x.Gender}");
            RuleFor(x => x.PhysicalActivityLevel).IsInEnum().WithMessage(x => $"Unknown activity level index: {(int)x.PhysicalActivityLevel}");
        }
    }
}
