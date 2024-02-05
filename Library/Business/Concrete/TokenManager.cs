using Business.Abstract;
using Core.Configuration;
using Core.Dto;
using Core.Utilities;
using Entities.Dto;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Business.Concrete
{
    public class TokenManager : ITokenService
    {
        private readonly CustomTokenOptions _tokenOption;

        public TokenManager(IOptions<CustomTokenOptions> tokenOption)
        {
            _tokenOption = tokenOption.Value;
        }

        private IEnumerable<Claim> GetClaims(UserDto userDto, List<string> audiences)
        {
            var userList = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, userDto.PkId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userDto.Mail),
                new Claim(ClaimTypes.Name, userDto.FullName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            userList.AddRange(audiences.Select(audience => new Claim(JwtRegisteredClaimNames.Aud, audience)));

            return userList;
        }

        public TokenDto CreateToken(UserDto userDto)
        {
            var accessTokenExpiration = DateTime.Now.AddYears(_tokenOption.AccessTokenExpiration);

            var securityKey = SignHelper.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaims(userDto, _tokenOption.Audience),
                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration,
                UserId = userDto.PkId.ToString(),
                UserFullName = userDto.FullName
            };

            return tokenDto;
        }
    }
}