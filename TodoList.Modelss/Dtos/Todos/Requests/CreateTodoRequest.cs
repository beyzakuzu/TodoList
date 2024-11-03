using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Modelss.Entities;

namespace TodoList.Modelss.Dtos.Todos.Requests
{
    public sealed record CreateTodoRequest(
       string Title,
       string Description,
       DateTime StartDate,
       DateTime EndDate,
       Priority Priority,
       int CategoryId);
}
