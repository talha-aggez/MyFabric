using Core.DBModels;
using System.Collections.Generic;

namespace Infrastructure.JWTUtility
{
    public  interface IJwtService
    {
        string GenerateJWTToken(AppUser appUser, List<AppRole> roles);
    }
}
