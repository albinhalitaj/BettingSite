using FluentValidation;

namespace Application.Bets.Commands.CreateBet
{
    public class CreateBetCommandValidator : AbstractValidator<CreateBetCommand>
    {
        public CreateBetCommandValidator()
        {
            RuleFor(v => v.Amount)
                .NotEmpty()
                .WithMessage("Amount is required")
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0.");
            RuleFor(v => v.Bet)
                .NotNull()
                .Must(p => p.GetType().IsEnum)
                .WithMessage("Bet Type is required! E.g. red = 0,black = 1,green = 2");
        }
    }
}