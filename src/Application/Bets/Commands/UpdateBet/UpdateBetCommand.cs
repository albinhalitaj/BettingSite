using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Bets.ViewModels;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Bets.Commands.UpdateBet
{
    public class UpdateBetCommand : IRequest<BetVm>
    {
        public int Id { get; set; }
        public BetType Bet { get; set; }
        public decimal Amount { get; set; }
    }

    public class UpdateBetCommandHandler : IRequestHandler<UpdateBetCommand, BetVm>
    {
        private readonly IApplicationDbContext _ctx;
        private readonly IMapper _mapper;

        public UpdateBetCommandHandler(IApplicationDbContext ctx,IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        
        public async Task<BetVm> Handle(UpdateBetCommand request, CancellationToken cancellationToken)
        {
            var bet = await _ctx.Bets.FirstOrDefaultAsync(x => x.BetId == request.Id, cancellationToken);
            if (bet is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Bets), request.Id);
            }

            _mapper.Map(request,bet);

            bet.UpdatedAt = DateTime.Now;
            bet.Profit = request.Bet == BetType.Green ? request.Amount * 14 : request.Amount + request.Amount;

            await _ctx.SaveChangesAsync(cancellationToken);

            return _mapper.Map<BetVm>(bet);
        }
    }
}