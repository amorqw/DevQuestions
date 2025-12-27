using DevQuestions.Contracts;

namespace DevQuestions.Application.Questions;

public interface IQuestionService
{
    Task<Guid> Create (CreateQuestionDto questionDto, CancellationToken cancellationToken);
}