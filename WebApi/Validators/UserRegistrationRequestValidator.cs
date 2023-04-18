using FluentValidation;
using WebApi.Dto;

namespace WebApi.Validators;

public class UserRegistrationRequestValidator : AbstractValidator<UserRegistrationRequestDto>
{
    public UserRegistrationRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_])(?=.*\d).+$")
            .WithMessage("Password should contain at least 1 uppercase, lowercase, symbol, digit");
    }
}