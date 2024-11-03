using Core.Exceptions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Modelss.Entities;

namespace TodoList.Service.Rules
{
    public class TodoBusinessRules
    {
        public virtual void TodoIsNullCheck(Todo todo)
        {
            if (todo is null)
            {
                throw new NotFoundException("İlgili liste bulunamadı.");
            }
        }

        public void UserCanAccessTodo(Todo todo, string userId, string userRole)
        {
            if (userRole.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
            {
                return; 
            }

            if (todo.UserId != userId)
            {
                throw new UnauthorizedAccessException("Bu todo'ya erişim izniniz yok.");
            }
        }
    }
}
