using FluentValidation;

namespace Application.Bets.Commands.DeleteBet
{
    public class DeleteBetCommandValidator : AbstractValidator<DeleteBetCommand>
    {
        public DeleteBetCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Id is required");
        }
    }
}