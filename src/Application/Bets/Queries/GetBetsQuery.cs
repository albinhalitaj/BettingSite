using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Bets.ViewModels;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Bets.Queries
{
    public class GetBetsQuery : IRequest<IList<BetVm>>
    {
        
    }

    public class GetBetsQueryHandler : IRequestHandler<GetBetsQuery, IList<BetVm>>
    {
        private readonly IApplicationDbContext _ctx;
        private readonly IMapper _mapper;
        
        public GetBetsQueryHandler(IApplicationDbContext ctx,IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        
        public async Task<IList<BetVm>> Handle(GetBetsQuery request, CancellationToken cancellationToken)
        {
            var result = new List<BetVm>();
            var bets = await _ctx.Bets.ToListAsync(cancellationToken);
            
            if (bets != null)
            {
                result = _mapper.Map<List<BetVm>>(bets);
            }

            return result;
        }
    }
}