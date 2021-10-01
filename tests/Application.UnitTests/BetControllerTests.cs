using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Controllers;
using Application.Bets.Commands.CreateBet;
using Application.Bets.Commands.DeleteBet;
using Application.Bets.Queries;
using Application.Bets.ViewModels;
using Domain.Enums;
using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Xunit;

namespace Application.UnitTests
{
    public class BetControllerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly BetController _controller;
        
        public BetControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _controller = new BetController(_mediator.Object);
        }

        [Fact]
        public async Task CreateBet_WithRequiredFields_ReturnsCreatedBet()
        {
            // Arrange 
            var command = new CreateBetCommand
            {
                Bet = BetType.Black,
                Amount = (decimal) 14.2
            };
            var responseMockResult = new BetVm
            {
                BetId = 1,
                Bet = BetType.Black,
                Amount = (decimal) 14.2,
                CreatedAt = DateTime.Now,
                Profit = command.Amount * 2,
                UserId = Guid.NewGuid()
            };

            _mediator.Setup(x => x.Send(It.IsAny<CreateBetCommand>(), CancellationToken.None))
                .ReturnsAsync(responseMockResult);
            
            // Act
            var result = await _controller.Create(command);
            var okResult = result as OkObjectResult;

            if (okResult != null)
            {
                Assert.NotNull(okResult);
            }

            var response = okResult?.Value as BetVm;
            Assert.Equal(command.Amount, response?.Amount);
            Assert.Equal(command.Bet, response?.Bet);
        }

        [Fact]
        public async Task DeleteBet_WithExistingId_ReturnsNoContent()
        {
           _mediator.Setup(x => x.Send(It.IsAny<DeleteBetCommand>(), CancellationToken.None))
               .ReturnsAsync(Unit.Value);
           
           var res = await _controller.Delete(It.IsAny<int>());
           
           res.ShouldBeOfType<NoContentResult>();
        }

        [Fact]
        public async Task GetBets_WithExistingBets_ReturnsAllBets()
        {
            var expectedBets = new[] {CreateRandomBet(), CreateRandomBet(), CreateRandomBet()};
            _mediator.Setup(x => x.Send(It.IsAny<GetBetsQuery>(), CancellationToken.None))
                .ReturnsAsync(expectedBets);

            var bets = await _controller.Get();
            
            bets.Value.ShouldBeEquivalentTo(expectedBets.ToList());
        }

        [Fact]
        public async Task GetBetById_WithExistingBet_ReturnsSingleBet()
        {
            var bet = CreateRandomBet();

            _mediator.Setup(x => x.Send(It.IsAny<GetBetByIdQuery>(), CancellationToken.None))
                .ReturnsAsync(bet);

            var result = await _controller.GetById(It.IsAny<int>());
            
            result.Value.ShouldBeOfType<BetVm>();
            result.Value.ShouldBeEquivalentTo(bet);
        }

        [Fact]
        public async Task GetBetById_WithUnExistingBet_ReturnsNotFound()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetBetByIdQuery>(), CancellationToken.None))
                .ReturnsAsync((BetVm) null);

            var result = await _controller.GetById(It.IsAny<int>());

            result.Result.ShouldBeOfType<NotFoundResult>();
        }

        private BetVm CreateRandomBet()
        {
            Random rnd = new();
            return new BetVm
            {
                BetId = rnd.Next(1, 100),
                Amount = (decimal) rnd.NextDouble(),
                Profit = rnd.Next(1, 50),
                CreatedAt = DateTime.Now,
                Bet = BetType.Red,
                UserId = Guid.NewGuid()
            };
        }
    }
}