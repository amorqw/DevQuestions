using DevQuestions.Application.Questions;
using DevQuestions.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DevQuestions.Presenters.Questions;

[ApiController]
[Route("[controller]")]
public class QuestionsController: ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuestionsController(IQuestionService questionService)
    {
        _questionService = questionService;
    }
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateQuestionDto questionDto,
        CancellationToken cancellationToken)
    {
        var questionId= await _questionService.Create(questionDto, cancellationToken);
        return Ok(questionId);
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] GetQuestionsDto questionDto,
        CancellationToken cancellationToken)
    {
        return Ok("Questions get");
    }
    [HttpGet("{QuestionId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid QuestionId )
    {
        return Ok("Questions get");
    }
    
    [HttpPut("{QuestionId:guid}")]
    public async Task<IActionResult> Update(
        [FromQuery] GetQuestionsDto questionDto,
        [FromBody]UpdateQuestionDto updateQuestionDto,
        CancellationToken cancellationToken)
    {
        return Ok("Questions get");
    }

    [HttpDelete("{questionId:guid}")]

    public async Task<IActionResult> Delete(
        [FromRoute] Guid questionId,
        CancellationToken cancellationToken)
    {
        return Ok("Questions delete");
    }

    [HttpPut("{QuestionId:guid}/solution")]
    public async Task<IActionResult> SelectSolution(
        [FromRoute] Guid questionId,
        [FromQuery] Guid answerId,
        CancellationToken cancellationToken)
    {
        return Ok("solution selected");
    }
    
    [HttpPost("{QuestionId:guid}/answers")]
    public async Task<IActionResult> AddAnswer(
        [FromRoute] Guid questionId,
        [FromBody] AddAnswerDto request,
        CancellationToken cancellationToken)
    {
        return Ok("Answer added");
    }
}