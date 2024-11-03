using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Modelss.Dtos.Todos.Requests;

namespace TodoList.Service.Validations.Todos;

public class CreateTodoRequestDtoValidator : AbstractValidator<CreateTodoRequest>
{
    public CreateTodoRequestDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .Length(2, 100).WithMessage("Başlık minimum 2, maksimum 100 karakterli olmalıdır.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .Length(5, 500).WithMessage("Açıklama minimum 5, maksimum 500 karakterli olmalıdır.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Başlangıç tarihi boş olamaz.")
            .Must(date => date >= DateTime.Now).WithMessage("Başlangıç tarihi şu anki tarihten sonraki bir tarih olmalıdır.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("Bitiş tarihi boş olamaz.")
            .GreaterThan(x => x.StartDate).WithMessage("Bitiş tarihi, başlangıç tarihinden sonra olmalıdır.");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Geçersiz öncelik .");
    }
}

