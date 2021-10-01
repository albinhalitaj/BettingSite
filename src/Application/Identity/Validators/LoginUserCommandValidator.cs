using Application.Identity.Commands;
using FluentValidation;

namespace Application.Identity.Validators
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull()
                .WithMessage("Username is required");
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Password is required");
        }
    }
}