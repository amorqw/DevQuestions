namespace DevQuestions.Contracts;

public record GetQuestionsDto(string Title, Guid[] TagIds, int Page, int PageSize);