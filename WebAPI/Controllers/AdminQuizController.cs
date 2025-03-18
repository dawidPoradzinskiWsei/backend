using System.Net.Mime;
using BackendLab01;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/admin/quizzes")]
public class AdminQuizController : Controller
{
    private readonly IQuizAdminService _service;

    public AdminQuizController(IQuizAdminService service)
    {
        _service = service;
    }


    [HttpGet("{id:int}")]
    public ActionResult<object> Get(int id)
    {
        var quiz = _service.FindAllQuizzes().FirstOrDefault(x => x.Id == id);

        return quiz is null ? NotFound() : Ok(quiz);
    }

    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<object> Post([FromBody] NewQuizDto dto)
    {
        var quiz = _service.AddQuiz(dto.Title, null);

        return CreatedAtAction(nameof(Get), new { id = quiz.Id }, quiz);

    }

    [HttpPatch("{id:int}")]
    [Consumes(MediaTypeNames.Application.JsonPatch)]
    public ActionResult<object> Patch(int id)
    {
        var quiz = _service.FindAllQuizzes().FirstOrDefault(x => x.Id == id);

        if( quiz is null)
        // or doc is null
        {
            return NotFound();
        }

        int prevCount = quiz.Items.Count;
        //doc.ApplyTo(quiz.ModelState);
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
}