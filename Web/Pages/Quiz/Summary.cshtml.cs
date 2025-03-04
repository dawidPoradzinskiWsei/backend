using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01.Pages;

public class Summary : PageModel
{

    private readonly IQuizUserService _userService;

    private readonly ILogger _logger;
    public Summary(IQuizUserService userService, ILogger<QuizModel> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    public int QuizID { get; set;}
    public int CorrectAnswers { get; set; }
    public int TotalQuestions { get; set; }

    public void OnGet(int quizId, int userId)
    {
        CorrectAnswers = _userService.CountCorrectAnswersForQuizFilledByUser(quizId, userId);
        TotalQuestions = _userService.FindQuizById(quizId).Items.Count;
    }
}