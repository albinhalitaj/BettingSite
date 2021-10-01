using System;
using System.Security.Claims;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _accessor;
        
        public CurrentUserService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string UserId => _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
    }
}