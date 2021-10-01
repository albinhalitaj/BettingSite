using FluentValidation;

namespace Application.Bets.Commands.UpdateBet
{
    public class UpdateBetCommandValidator : AbstractValidator<UpdateBetCommand>
    {
        public UpdateBetCommandValidator()
        {
            RuleFor(v => v.Amount)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Amount is required");
            RuleFor(v => v.Bet)
                .NotNull()
                .NotEmpty()
                .WithMessage("Bet Type is required! E.g. red = 0,black = 1,green = 2");
        }
    }
}