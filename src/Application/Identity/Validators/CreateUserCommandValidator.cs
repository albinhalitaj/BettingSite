using Application.Bets.Commands;
using Application.Identity.Commands;
using FluentValidation;

namespace Application.Identity.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(15)
                .WithMessage("Username is required");
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("Email Address is required");
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(5)
                .WithMessage("Password is required");
        }
    }
}