using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Bets.ViewModels;
using Application.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.Enums;
using MediatR;

namespace Application.Bets.Commands.CreateBet
{
    public class CreateBetCommand : IRequest<BetVm>
    {
        public BetType Bet { get; set; }
        public decimal Amount { get; set; }
    }

    public class CreateBetCommandHandler : IRequestHandler<CreateBetCommand, BetVm>
    {
        private readonly IApplicationDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateBetCommandHandler(IApplicationDbContext ctx,IMapper mapper,ICurrentUserService currentUserService)
        {
            _ctx = ctx;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        
        public async Task<BetVm> Handle(CreateBetCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Bets>(request);
            entity.CreatedAt = DateTime.Now;
            entity.Profit = request.Bet == BetType.Green ? request.Amount * 14 : request.Amount + request.Amount;
            entity.UserId = Guid.Parse(_currentUserService.UserId);

            await _ctx.Bets.AddAsync(entity, cancellationToken);
            await _ctx.SaveChangesAsync(cancellationToken);

            return _mapper.Map<BetVm>(entity);
        }
    }
}