using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Modelss.Dtos.Categories.Requests;

namespace TodoList.Service.Validations.Categories
{
    public class UpdateCategoryRequestDtoValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryRequestDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Kategori ID'si boş olamaz.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .Length(2, 30).WithMessage("Kategori adı minimum 2, maksimum 30 karakterli olmalıdır.");
        }
    }
}
