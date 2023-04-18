using FluentValidation;
using WebApi.Dto;

namespace WebApi.Validators;

public class UserLoginRequestValidator : AbstractValidator<UserLoginRequestDto>
{
    public UserLoginRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_])(?=.*\d).+$")
            .WithMessage("Password should contain at least 1 uppercase, lowercase, symbol, digit");;
    }
}