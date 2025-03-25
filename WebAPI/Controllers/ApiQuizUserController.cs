using ApplicationCore.Models.QuizAggregate;
using AutoMapper;
using BackendLab01;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/quizzes/")]
public class QuizController : Controller 
{
    private readonly IQuizUserService _service;
    private readonly IMapper _mapper;

    public QuizController(IQuizUserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
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

    [Route("{quizId}/answers/{userId}")]
    [HttpGet]
    public ActionResult<object> GetQuizFeedback(int quizId, int userId)
    {
        var feedback = _service.GetUserAnswersForQuiz(quizId, userId);

        if(feedback.Count == 0)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }

        return _mapper.Map<FeedbackDTO>(feedback);
    }
}