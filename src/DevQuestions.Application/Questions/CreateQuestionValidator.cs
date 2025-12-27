using DevQuestions.Contracts;
using FluentValidation;

namespace DevQuestions.Application.Questions;

public class CreateQuestionValidator: AbstractValidator<CreateQuestionDto>
{
    public CreateQuestionValidator()
    {
        
        RuleFor(x => x.Title).NotEmpty().MaximumLength(500).WithMessage("Заголовок не валидный");
        RuleFor(x=> x.Body).NotEmpty().MaximumLength(5000).WithMessage("Текст невалидный");
        RuleFor(x => x.UserId).NotEmpty();
        RuleForEach(x => x.TagIds).NotEmpty();
    }
    
}