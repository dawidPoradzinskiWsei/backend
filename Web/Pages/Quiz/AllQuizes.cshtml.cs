using ApplicationCore.Commons.Repository;
using ApplicationCore.Models.QuizAggregate;
using BackendLab01;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class AllQuizes : PageModel
{
    private readonly IGenericRepository<Quiz, int> _quizRepository;

    public AllQuizes(IGenericRepository<Quiz,int> quizRepository)
    {
        _quizRepository = quizRepository;
    }

    public List<Quiz> Quizes { get; set; }

    public void OnGet()
    {
        Quizes = _quizRepository.FindAll();
    }
}