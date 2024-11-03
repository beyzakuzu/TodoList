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
    public  class CategoryBusinessRules
    {
        public virtual void CategoryIsNullCheck(Category category)
        {
            if (category is null)
            {
                throw new NotFoundException("İlgili kategori bulunamadı.");
            }
        }
    }
}
