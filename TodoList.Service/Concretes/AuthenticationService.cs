using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Modelss.Dtos.Users.Requests;
using TodoList.Modelss.Tokens;
using TodoList.Service.Absracts;

namespace TodoList.Service.Concretes
{
    public class AuthenticationService(IUserService _userService, IJwtService _jwtService) : IAuthenticationService
    {


        public async Task<ReturnModel<TokenResponseDto>> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userService.LoginAsync(dto);
            var tokenResponse = await _jwtService.CreateJwtTokenAsync(user);

            return new ReturnModel<TokenResponseDto>
            {
                Data = tokenResponse,
                Message = "Giriş Başarılı",
                Status = 200,
                Success = true
            };
        }

        public async Task<ReturnModel<TokenResponseDto>> RegisterAsync(RegisterRequestDto dto)
        {
            var user = await _userService.RegisterAsync(dto);
            var tokenResponse = await _jwtService.CreateJwtTokenAsync(user);

            return new ReturnModel<TokenResponseDto>
            {
                Data = tokenResponse,
                Message = "Kayıt işlemi Başarılı",
                Status = 200,
                Success = true
            };
        }
    }
}
