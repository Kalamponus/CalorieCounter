using FluentValidation;

namespace CalorieCounter.Application.UseCases.UserCases.Commands.ChangeActivityLevel
{
    public class ChangeUserActivityLevelCommandValidator : AbstractValidator<ChangeUserActivityLevelCommand>
    {
        public ChangeUserActivityLevelCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x => x.PhysicalActivityLevel).IsInEnum().WithMessage(x => $"Unknown activity level index: {(int)x.PhysicalActivityLevel}");
        }
    }
}
