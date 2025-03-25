using ApplicationCore.Models;

public class FeedbackDTO
{
    public int QuizId {get;set;}
    public int UserId {get;set;}
    public int TotalQuestions {get;set;}
    public List<object> Details {get;set;}
}