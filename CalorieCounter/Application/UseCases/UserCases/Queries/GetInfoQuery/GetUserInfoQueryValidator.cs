using FluentValidation;

namespace CalorieCounter.Application.UseCases.UserCases.Queries.Validators
{
    public class GetUserInfoQueryValidator : AbstractValidator<GetUserInfoQuery>
    {
        public GetUserInfoQueryValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Id cannot be empty");
        }
    }
}
