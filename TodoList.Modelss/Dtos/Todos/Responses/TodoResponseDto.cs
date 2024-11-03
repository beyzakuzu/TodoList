using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Modelss.Entities;

namespace TodoList.Modelss.Dtos.Todos.Responses
{
    public sealed record TodoResponseDto(
    Guid Id,
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    DateTime CreatedDate,
    Priority Priority,
    int CategoryId,
    bool Completed,
    string UserName,
    string UserId,
    string Category);


}
