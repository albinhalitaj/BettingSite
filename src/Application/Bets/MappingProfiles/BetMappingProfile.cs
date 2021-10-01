using Application.Bets.Commands.CreateBet;
using Application.Bets.Commands.UpdateBet;
using Application.Bets.ViewModels;
using AutoMapper;

namespace Application.Bets.MappingProfiles
{
    public class BetMappingProfile : Profile
    {
        public BetMappingProfile()
        {
            CreateMap<CreateBetCommand, Domain.Entities.Bets>();
            CreateMap<UpdateBetCommand, Domain.Entities.Bets>();

            CreateMap<Domain.Entities.Bets, BetVm>();
            CreateMap<BetVm, Domain.Entities.Bets>();
        }
    }
}