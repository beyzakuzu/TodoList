using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Modelss.Dtos.Categories.Requests;

namespace TodoList.Service.Validations.Categories
{
    public class CreateCategoryRequestDtoValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .Length(2, 30).WithMessage("Kategori adı minimum 2, maksimum 30 karakterli olmalıdır.");
        }
    }
}
