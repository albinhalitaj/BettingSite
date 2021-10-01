using System;
using Domain;
using Domain.Enums;

namespace Application.Bets.ViewModels
{
    public class BetVm
    {
        public int BetId { get; set; }
        public Guid UserId { get; set; }
        public BetType Bet { get; set; }
        public decimal Amount { get; set; }
        public decimal Profit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}