using System.Threading.Tasks;
using Application.Identity.Commands;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> CreateUser(CreateUserCommand request);
        Task<object> LoginUser(LoginUserCommand request);
    }
}