#nullable enable
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Identity.Commands
{
    public class LoginUserCommand : IRequest<object?>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, object?>
    {
        private readonly IIdentityService _identityService;

        public LoginUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        
        public async Task<object?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.LoginUser(request);
            return user;
        }
    }
}