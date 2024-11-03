using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Modelss.Dtos.Users.Requests
{
    public sealed record LoginRequestDto(string Email, string Password);
}
