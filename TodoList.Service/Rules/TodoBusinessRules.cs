using Core.Exceptions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

      

        public void ValidateStartDate(Todo todo)
        {
            if (todo.StartDate < DateTime.Now)
            {
                throw new ValidationException("Başlangıç tarihi bugünden eski olamaz.");
            }
        }

        public void ValidatePriority(Todo todo, Priority minPriority)
        {
            if (todo.Priority < minPriority)
            {
                throw new ValidationException($"Todo'nun öncelik seviyesi {minPriority} veya daha yüksek olmalıdır.");
            }
        }
    }
}
