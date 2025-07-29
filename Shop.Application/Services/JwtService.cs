using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shop.Application.IServices;
using Shop.Domain;
using Shop.Domain.Models.Users;

namespace Shop.Application.Services
{
    public class JwtService : IJwtService, IScopedDependency
    {
        private readonly SiteSetting _siteSetting;
        private readonly SignInManager<User> _signInManager;
        
        public JwtService(IOptionsSnapshot<SiteSetting> siteSetting, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _siteSetting = siteSetting.Value;
        }
        
        public async Task<string> GenerateAsync(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSetting.SecretKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSetting.EncryptKey);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
            
            var claims = await GetClaims(user);

            var descriptor = new SecurityTokenDescriptor()
            {
                Issuer = _siteSetting.JwtSetting.Issuer,
                Audience = _siteSetting.JwtSetting.Audience,
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddDays(_siteSetting.JwtSetting.ExpirationDay),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.Now.AddMinutes(_siteSetting.JwtSetting.NotBeforeMinute),
                EncryptingCredentials = encryptingCredentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(descriptor);
            
            var jwt = tokenHandler.WriteToken(securityToken);

            return jwt;
        }

        private async Task<IEnumerable<Claim>> GetClaims(User user)
        {
            var result = await _signInManager.ClaimsFactory.CreateAsync(user);
            
            var list = new List<Claim>();
            list.AddRange(result.Claims);
            list.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
            return list;
            // var securityStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;
            //
            // var claims = new List<Claim>()
            // {
            //     new Claim(ClaimTypes.Name, user.UserName),
            //     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //     new Claim(securityStampClaimType, user.SecurityStamp.ToString())
            // };
            //
            // return claims;
        }
    }
}