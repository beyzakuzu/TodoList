using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Modelss.Dtos.Categories.Requests
{
    public sealed record UpdateCategoryRequest(int Id, string Name);

}
