using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Identity.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationSettings _applicationSettings;
        
        public IdentityService(UserManager<ApplicationUser> userManager,IOptions<ApplicationSettings> applicationSettings)
        {
            _userManager = userManager;
            _applicationSettings = applicationSettings.Value;
        }
        
        public async Task<string> CreateUser(CreateUserCommand request)
        {
            var appUser = new ApplicationUser()
            {
                UserName = request.Username,
                Email = request.Email
            };
            var user = await _userManager.CreateAsync(appUser, request.Password);
            if (!user.Succeeded) return string.Empty;
            var id = await _userManager.GetUserIdAsync(appUser);
            return id;

        }

        public async Task<object> LoginUser(LoginUserCommand request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return string.Empty;
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                return string.Empty;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_applicationSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new
            {
                token = tokenHandler.WriteToken(token),
                userId = user.Id
            };
        }
    }
}