using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Bets.Commands.DeleteBet
{
    public class DeleteBetCommand : IRequest
    {
        public int Id { get; set; }
    }
    
    public class DeleteBetCommandHandler : IRequestHandler<DeleteBetCommand>
    {
        private readonly IApplicationDbContext _ctx;

        public DeleteBetCommandHandler(IApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        
        public async Task<Unit> Handle(DeleteBetCommand request, CancellationToken cancellationToken)
        {
            var bet = await _ctx.Bets.FirstOrDefaultAsync(x => x.BetId == request.Id, cancellationToken);
            if (bet is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Bets), request.Id);
            }

            _ctx.Bets.Remove(bet);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}