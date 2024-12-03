using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bookStore.App.Services.Jwt
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(string userId, string role);
    }
}