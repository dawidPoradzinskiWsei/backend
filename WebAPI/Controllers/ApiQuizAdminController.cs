using System.Net;
using System.Net.Mime;
using ApplicationCore.Models.QuizAggregate;
using BackendLab01;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/admin/quizzes/")]
public class ApiQuizAdminController : Controller
{
    private readonly IQuizAdminService _service;

    public ApiQuizAdminController(IQuizAdminService service)
    {
        _service = service;
    }

    [HttpPost]
    public ActionResult<object> AddQuiz([FromBody] NewQuizDto dto)
    {
        var quiz = _service.AddQuiz(dto.Title, null);

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
}