using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Modelss.Entities;
using TodoList.Modelss.Tokens;

namespace TodoList.Service.Absracts
{
    public interface IJwtService
    {
        Task<TokenResponseDto> CreateJwtTokenAsync(User user);
    }
}
