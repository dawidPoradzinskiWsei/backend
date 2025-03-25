using System.Net;
using System.Net.Mime;
using ApplicationCore.Models.QuizAggregate;
using AutoMapper;
using BackendLab01;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/admin/quizzes/")]
public class ApiQuizAdminController : Controller
{
    private readonly IQuizAdminService _service;
    private readonly IMapper _mapper;
    private readonly IValidator<QuizItem> _validator;

    public ApiQuizAdminController(IQuizAdminService service, IMapper mapper, IValidator<QuizItem> validator)
    {
        _service = service;
        _mapper = mapper;
        _validator = validator;
    }

    [HttpPost]
    public ActionResult<object> AddQuiz([FromBody] NewQuizDto dto)
    {
        var quiz = _service.AddQuiz(_mapper.Map<Quiz>(dto));

        return CreatedAtAction(nameof(Get), new { id = quiz.Id }, quiz);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Quiz> Get(int id)
    {
        var quiz = _service.FindAllQuizzes().FirstOrDefault(x => x.Id == id);

        return quiz is null ? NotFound() : Ok(quiz);
    }

    [HttpPatch("{id:int}")]
    [Consumes(MediaTypeNames.Application.JsonPatch)]
    public ActionResult<object> Patch(int id, JsonPatchDocument<Quiz>? patchDoc)
    {
        var quiz = _service.FindAllQuizzes().FirstOrDefault(x => x.Id == id);

        if( quiz is null || patchDoc is null)
        {
            return NotFound();
        }

        int prevCount = quiz.Items.Count;
        patchDoc.ApplyTo(quiz, ModelState);

        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        if(prevCount < quiz.Items.Count)
        {
            QuizItem item = quiz.Items[^1];

            var validationResult = _validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            _service.AddQuizItem(item.Question,item.IncorrectAnswers,item.CorrectAnswer,1);
        }

        return quiz;
    }

    [HttpDelete("{id:int}")]
    public ActionResult<object> Delete(int id)
    {
        var quiz = _service.FindAllQuizzes().FirstOrDefault(x => x.Id == id);

        if(quiz is null)
        {
            return NotFound();
        }

        if(quiz.Items is not null && quiz.Items.Count != 0)
        {
            return Conflict();
        }

        _service.RemoveQuiz(id);

        return StatusCode(StatusCodes.Status410Gone);

    }

    [HttpGet("{id:int}/question/{nr:int}")]
    public ActionResult<QuizItemDto> GetItem(int id, int nr)
    {

        var quiz = _service.FindAllQuizzes().FirstOrDefault(x => x.Id == id);

        if(quiz is null)
        {
            return NotFound();
        }

        if(quiz.Items is null || quiz.Items.Count == 0)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }

        var question = quiz.Items[nr];

        if(question is null)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }

        return Ok(question);
    }
}