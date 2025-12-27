using DevQuestions.Application.Questions;
using DevQuestions.Contracts;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DevQuestions.Application;

public class QuestionsService : IQuestionService
{
    private readonly IQuestionRepository _questionsRepository;
    private readonly ILogger<QuestionsService> _logger;
    private readonly IValidator<CreateQuestionDto> _validator;

    public QuestionsService(IQuestionRepository questionsRepository, ILogger<QuestionsService> logger, IValidator<CreateQuestionDto> validator)
    {
        _questionsRepository = questionsRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Guid> Create(CreateQuestionDto questionDto, CancellationToken cancellationToken)
    {
        // Проверка валидности
        // Валидация входных данных
        var validationResult = await _validator.ValidateAsync(questionDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        // Валидация бизнес логики
        var openUserQuestionsCount = await _questionsRepository.GetOpenUserQuestionsAsync(questionDto.UserId, cancellationToken);
        if (openUserQuestionsCount > 3)
        {
            throw new Exception("Пользователь не может открыть больше 3 вопросов");
        }

        
        // Создание сущности Question
        Guid questionId = Guid.NewGuid();

        Question question = new Question(
            questionId,
            questionDto.Title,
            questionDto.Body,
            questionDto.UserId,
            null,
            questionDto.TagIds.ToList()
        );
        // Сохранение сущности Question в базе данных
        await _questionsRepository.AddAsync(question, cancellationToken);
        // Логирование об успешном или неуспешном сохранении
        _logger.LogInformation("Created question with id {questionId}", questionId);
        
        return questionId;
    }


    /* public async Task<IActionResult> Update(
        [FromQuery] GetQuestionsDto questionDto,
        [FromBody] UpdateQuestionDto updateQuestionDto,
        CancellationToken cancellationToken)
    {
    }

    [HttpDelete("{questionId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid questionId,
        CancellationToken cancellationToken)
    {
    }

    [HttpPut("{QuestionId:guid}/solution")]
    public async Task<IActionResult> SelectSolution(
        [FromRoute] Guid questionId,
        [FromQuery] Guid answerId,
        CancellationToken cancellationToken)
    {
    }


    public async Task<IActionResult> AddAnswer(
        [FromRoute] Guid questionId,
        [FromBody] AddAnswerDto request,
        CancellationToken cancellationToken)
    {
    }
 */
}

