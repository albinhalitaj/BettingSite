using System.Threading;
using System.Threading.Tasks;
using Application.Bets.ViewModels;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Bets.Queries
{
    public class GetBetByIdQuery : IRequest<BetVm>
    {
        public int Id { get; set; }
    }
    
    public class GetBetByIdQueryHandler : IRequestHandler<GetBetByIdQuery, BetVm>
    {
        private readonly IApplicationDbContext _ctx;
        private readonly IMapper _mapper;

        public GetBetByIdQueryHandler(IApplicationDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        
        public async Task<BetVm> Handle(GetBetByIdQuery request, CancellationToken cancellationToken)
        {
            var bet = await _ctx.Bets.FirstOrDefaultAsync(x => x.BetId == request.Id, cancellationToken);
            if (bet == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Bets), request.Id);
            }

            var result = _mapper.Map<BetVm>(bet);
            
            return result;
        }
    }
}