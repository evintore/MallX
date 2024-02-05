using Business.Abstract;
using Business.ModelMapping.AutoMapper;
using Core.Dto;
using Core.Results;
using Core.Utilities;
using Entities.Dto;
using Microsoft.AspNetCore.Http;
using Repository.Abstract;
using System.Net;

namespace Business.Concrete
{
    internal class AuthManager : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContext;

        public AuthManager(IUnitOfWork unitOfWork, IUserService userService, ITokenService tokenService, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _tokenService = tokenService;
            _httpContext = httpContext;
        }

        public async Task<Response<TokenDto>> Login(UserLoginDto userLoginDto)
        {
            var dbUser = await _unitOfWork.User.GetAsync(x => x.Mail == userLoginDto.Email && x.IsActive);

            if (dbUser is null)
                return Response<TokenDto>.Fail("Email veya şifre yanlış", (int)HttpStatusCode.NotFound, true);

            if (!HashingHelper.VerifyHashedPassword(dbUser.Password, userLoginDto.Password))
                return Response<TokenDto>.Fail("Email veya şifre yanlış", (int)HttpStatusCode.NotFound, true);

            var token = _tokenService.CreateToken(ObjectMapper.Mapper.Map<UserDto>(dbUser));

            _httpContext.HttpContext.Response.Cookies.Append("access_token", token.AccessToken, new CookieOptions
            {
                Expires = token.AccessTokenExpiration,
                HttpOnly = true
            });

            return Response<TokenDto>.Success(token, (int)HttpStatusCode.OK);
        }

        public async Task<Response<TokenDto>> CheckLoggedIn(int userId)
        {
            var dbUser = await _userService.GetUserByIdAsync(userId);

            if (dbUser.Data is null)
                return Response<TokenDto>.Fail("Kullanıcı Doğrulanamadı", (int)HttpStatusCode.BadRequest, true);

            TokenDto tokenDto = new() { UserId = userId.ToString(), UserFullName = dbUser.Data.FullName };

            return Response<TokenDto>.Success(tokenDto, (int)HttpStatusCode.OK);
        }
    }
}
