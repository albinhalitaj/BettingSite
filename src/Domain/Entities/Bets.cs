using System;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities
{
    public class Bets
    {
        [Key]
        public int BetId { get; set; }
        public Guid UserId { get; set; }
        public BetType Bet { get; set; }
        public decimal Amount { get; set; }
        public decimal Profit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}