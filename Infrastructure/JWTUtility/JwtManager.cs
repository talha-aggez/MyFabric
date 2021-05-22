using Core.DBModels;
using Infrastructure.StringInfos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.JWTUtility
{
    public class JwtManager : IJwtService
    {

        public string GenerateJWTToken(AppUser appUser, List<AppRole> roles)
        {
         

            var bytes = Encoding.UTF8.GetBytes(JWTInfo.SecurityKey); //startup la aynı olmalı 
            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



            JwtSecurityToken token = new JwtSecurityToken(issuer: JWTInfo.Issuer, audience: JWTInfo.Audience,
                notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(JWTInfo.TokenExpiration), signingCredentials: credentials, claims: GetClaims(appUser, roles));


            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
        private List<Claim> GetClaims(AppUser appUser, List<AppRole> roles)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.ID.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, appUser.Name));

            if (roles != null)
            {
                if (roles.Count > 0)
                {
                    foreach (var item in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item.Name));
                    }
                }
            }

            return claims;

        }
    }
}
