using ApplicationCore.Models.QuizAggregate;
using BackendLab01;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/quizzes/")]
public class QuizController : Controller 
{
    private readonly IQuizUserService _service;

    public QuizController(IQuizUserService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<QuizDTO> FindById(int id)
    {
        Quiz? quiz = _service.FindQuizById(id);

        if(quiz is null)
        {
            return NotFound();
        }

        return Ok(QuizDTO.of(quiz));
    }

    [HttpGet]
    public IEnumerable<QuizDTO> FindAll()
    {
        IEnumerable<Quiz> quizes = _service.FindAllQuizes();

        return quizes.Select(quiz => QuizDTO.of(quiz));
    }

    [HttpPost]
    [Route("{quizId}/items/{itemId}")]
    public void SaveAnswer([FromBody] QuizItemAnswerDTO dto, int quizId, int itemId)
    {
        _service.SaveUserAnswerForQuiz(quizId, dto.UserId,itemId,dto.Answer);
    }

    [HttpGet]
    [Route("{quizId}/correct/{userId}")]
    public ActionResult<Dictionary<string,int>> FindCorrectAnswersCount(int quizId,int userId)
    {
        return Ok(new Dictionary<string,int>() { {"correct", _service.CountCorrectAnswersForQuizFilledByUser(quizId,userId)} });
    }
}