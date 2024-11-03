using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Modelss.Dtos.Users.Requests;
using TodoList.Modelss.Entities;

namespace TodoList.Service.Absracts;

public interface IUserService
{

    Task<User> RegisterAsync(RegisterRequestDto dto);
    Task<User> GetByEmailAsync(string email);

    Task<User> LoginAsync(LoginRequestDto dto);

    Task<User> UpdateAsync(string id, UserUpdateRequestDto dto);

    Task<User> ChangePasswordAsync(string id, ChangePasswordRequestDto requestDto);

    Task<string> DeleteAsync(string id);
}
