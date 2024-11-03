using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Modelss.Dtos.Users.Requests;
using TodoList.Modelss.Tokens;

namespace TodoList.Service.Absracts
{
    public interface IAuthenticationService
    {
        Task<ReturnModel<TokenResponseDto>> LoginAsync(LoginRequestDto dto);
        Task<ReturnModel<TokenResponseDto>> RegisterAsync(RegisterRequestDto dto);
    }
}
