using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Identity.Commands
{
    public class CreateUserCommand : IRequest<string>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IIdentityService _identityService;

        public CreateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.CreateUser(request);
            return userId;
        }
    }
}